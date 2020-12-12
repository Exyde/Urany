using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBall : MonoBehaviour
{
    [Header("Gravity Ball Attack")]

    public GameObject spherePrefab;
    public Transform SphereSpawnerArea;
    public Transform SphereHolder;

    [Header ("Datas")]
    public int sphereNumber;
    public float sphereSpeed;
    public float attackDuration;

    private Uranie uranie;

    Vector3 _center;
    Vector3 _size;

    public GameObject explosionPrefab;


    void Start()
    {
        uranie = GetComponent<Uranie>();

        Collider2D coll = SphereSpawnerArea.GetComponent<Collider2D>();
        Bounds bounds = coll.bounds;

        _center = bounds.center;
        _size = bounds.size;
    }
    public void SetPhase1()
    {
        sphereSpeed = 3f;
        sphereNumber = 5;
        attackDuration = 1f;
    }
    public void SetPhase2()
    {
        sphereSpeed = 3.5f;
        sphereNumber = 8;
        attackDuration = .8f;
    }

    public void SetPhase3()
    {
        sphereSpeed = 4f;
        sphereNumber = 12;
        attackDuration = .6f;
    }


    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

	private void Update()
	{
        SphereSpawnerArea.position = new Vector3( SphereSpawnerArea.position.x, transform.position.y, 0);
        Collider2D coll = SphereSpawnerArea.GetComponent<Collider2D>();
        Bounds bounds = coll.bounds;

        _center = bounds.center;
        _size = bounds.size;
    }

	IEnumerator DoAttack()
    {
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();
        AudioManager.instance.UranieAttackCast();

        for (int i = 0; i < sphereNumber; i++)
        {
            //Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Vector2 spherePos = RandomPointInBox(_center, _size);
            
            GameObject sphere = Instantiate(spherePrefab, spherePos, Quaternion.identity);
            sphere.GetComponent<GravitySphereBehavior>().speed = sphereSpeed;
            sphere.transform.parent = SphereHolder;

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
            CameraShake.Shake(.05f, .05f);

        }

        uranie.GetComponent<Animator>().SetTrigger("release");
        yield return new WaitForSeconds(.35f);


        foreach (GameObject sphere in currentSpheres)
        {
            if (sphere != null)
			{
                sphere.GetComponent<GravitySphereBehavior>().SetFire();
            }
        }

        yield return new WaitForSeconds(.8f);


        EndAttack();
    }

    public void BeginAttack()
    {
        uranie.isAttacking = true;
        uranie.isMoving = false;
        uranie.isWaiting = false;

        uranie.GetComponent<Animator>().SetTrigger("cast");

    }

    public void EndAttack()
    {
        uranie.state = Uranie.State.Move;
        uranie.isAttacking = false;
        uranie.isMoving = true;
    }

    private void OnDrawGizmos()
    {
        //Spawn Box
        Gizmos.color = Color.red;
        Gizmos.DrawCube(_center, _size);
    }

    public Vector2 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
            (Random.value - 0.5f) * size.x,
            (Random.value - 0.5f) * size.y,
            (Random.value - 0.5f) * size.z);
    }
}
