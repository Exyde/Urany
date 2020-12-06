using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    PostProcessController pp;
    public bool drawGizmos;

    [Header("All Attack Stats")]
    private AnimationScript anim;
    public LayerMask enemyLayer;
    //How many attack per second
    public float attackRate = 2f;
    float nextAttackTime = 0;

    [Header("Side Attack Data")]
    public Transform sideAttackBottomLeft;
    public Transform sideAttackTopRight;
    public float sideAttackRange = .5f;
    public int sideAttackDamage = 50;
    public float sideAttackForce;
    public float verticalPushForce = 3f;

    [Header("Down Attack Data")]
    public Transform downAttackBottomLeft;
    public Transform downAttackTopRight;
    public float downAttackRange = .5f;
    public int downAttackDamage = 50;

    void Start()
    {
        anim = GetComponentInChildren<AnimationScript>();
        pp = FindObjectOfType<PostProcessController>();
    }

    void Update()
    {
        Vector2 direction;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Attack"))
            {
                SideAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    private void HandleAttack(Vector2 direction)
    {

        if (direction.y == 1)
        {
            print("Attack Up !");

        }
        else if (direction.y == -1)
        {
            //anim.SetTrigger("downAttack");
            print("Attack Down !");

            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(downAttackPoint.position, downAttackRange, enemyLayer);
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(downAttackBottomLeft.position, downAttackTopRight.position, enemyLayer);

            if (hitEnemies.Length > 0)
			{
                foreach (Collider2D enemy in hitEnemies)
                {
                    print(enemy.name);
                    enemy.GetComponent<Enemy>().TakeDamage(downAttackDamage);
                }
            }
        }

        else
        {
            anim.SetTrigger("sideAttack");

            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(sideAttackPoint.position, sideAttackRange, enemyLayer);
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(sideAttackBottomLeft.position, sideAttackTopRight.position, enemyLayer);

            if (hitEnemies.Length > 0)
			{
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(sideAttackDamage);
                }
            }
 

        }
    }

    public void SideAttack()
	{
        anim.SetTrigger("sideAttack");
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(sideAttackBottomLeft.position, sideAttackTopRight.position, enemyLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(sideAttackDamage);
                PushEnemy(enemy.transform);

                pp.SetAttackPostProcess();
            }
        }
    }

    public void PushEnemy(Transform enemy)
	{
        float verticalForce = verticalPushForce;
        float horizontalForce = -1 * (transform.position - enemy.transform.position).normalized.x * sideAttackForce;

        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalForce, verticalForce), ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            DrawSideAttackGizmos();
           // DrawDonwnAttackGizmos();
        }

    }

    private void DrawSideAttackGizmos()
    {
        Vector3 bottomLeft = sideAttackBottomLeft.position;
        Vector3 topRight = sideAttackTopRight.position;
        Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
        Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);


        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(sideAttackBottomLeft.position, sideAttackTopRight.position);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
    }

    private void DrawDonwnAttackGizmos()
    {
        Vector3 bottomLeft = downAttackBottomLeft.position;
        Vector3 topRight = downAttackTopRight.position;
        Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);
        Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(downAttackBottomLeft.position, downAttackTopRight.position);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
    }
}