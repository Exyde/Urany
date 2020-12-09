using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBall : MonoBehaviour
{
    [Header("Multiple Ball Attack")]

    public GameObject spherePrefab;
    public float spawnRadius = 1.2f;

    [Header ("Datas")]
    public int sphereNumber;
    public float sphereSpeed;
    public float attackDuration;


    private Uranie uranie;

    void Start()
    {
        uranie = GetComponent<Uranie>();
    }

    public void SetPhase1()
    {
        sphereSpeed = 2.3f;
        sphereNumber = 6;
        attackDuration = 2f;
    }
    public void SetPhase2()
    {
        sphereSpeed = 2.8f;
        sphereNumber = 8;
        attackDuration = 1.5f;
    }

    public void SetPhase3()
    {
        sphereSpeed = 3.5f;
        sphereNumber = 12;
        attackDuration = 1f;
    }

    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

    IEnumerator DoAttack()
    {
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();

        for (int i = 0; i < sphereNumber; i++)
        {
            Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            GameObject sphere = Instantiate(spherePrefab, spherePos, Quaternion.identity);
            sphere.GetComponent<MultipleSphereBehavior>().speed = sphereSpeed;

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
        }

        yield return new WaitForSeconds(.2f);


       foreach (GameObject sphere in currentSpheres)
	   {
            if (sphere != null)
			{
                sphere.GetComponent<MultipleSphereBehavior>().SetFire();
            }
        }

        EndAttack();
    }

    public void BeginAttack()
    {
        uranie.isAttacking = true;
        uranie.isMoving = false;
        uranie.isWaiting = false;
    }

    public void EndAttack()
    {
        uranie.state = Uranie.State.Move;
        uranie.isAttacking = false;
        uranie.isMoving = true;
    }

    private void OnDrawGizmos()
    {
        //Attack Range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
