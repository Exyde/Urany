using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBall : MonoBehaviour
{
    [Header("Gravity Ball Attack")]

    public GameObject spherePrefab;
    public int sphereNumber;
    public float sphereSpeed;
    public float attackDuration;
    public float spawnRadius = 1.2f;

    private Uranie uranie;

    public Transform SphereSpawnerArea;
    Vector3 _center;
    Vector3 _size;

    void Start()
    {
        uranie = GetComponent<Uranie>();

        Collider2D coll = SphereSpawnerArea.GetComponent<Collider2D>();
        Bounds bounds = coll.bounds;

        _center = bounds.center;
        _size = bounds.size;
    }

    public void Attack()
    {
        StartCoroutine(DoAttack());
    }

	private void Update()
	{
        SphereSpawnerArea.position = new Vector3( SphereSpawnerArea.position.x, transform.position.y, 0);
    }

	IEnumerator DoAttack()
    {
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();

        for (int i = 0; i < sphereNumber; i++)
        {
            //Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            Vector2 spherePos = RandomPointInBox(_center, _size);
            
            GameObject sphere = Instantiate(spherePrefab, spherePos, Quaternion.identity);

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
        }


        yield return new WaitForSeconds(.2f);

        foreach (GameObject sphere in currentSpheres)
        {
            sphere.GetComponent<GravitySphereBehavior>().Explode();
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

    public Vector2 RandomPointInBox(Vector3 center, Vector3 size)
    {
        return center + new Vector3(
            (Random.value - 0.5f) * size.x,
            (Random.value - 0.5f) * size.y,
            (Random.value - 0.5f) * size.z);
    }
}
