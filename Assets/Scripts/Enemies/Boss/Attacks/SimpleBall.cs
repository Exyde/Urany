using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    [Header("Simple Ball attack")]

    public GameObject sphereAttackPrefab;
    public float spawnRadius = 1.2f;
    public Transform SphereHolder;


    [Header("Datas")]
    public float sphereSpeed;
    public int sphereNumber;
    public float attackDuration;
    public float timeUntilFire;


    private Uranie uranie;

	void Start()
	{
        uranie = GetComponent<Uranie>();
	}

    public void Attack()
	{
        StartCoroutine(DoAttack());
	}

    public void SetPhase1()
	{
        sphereSpeed = 3f;
        sphereNumber = 3;
        attackDuration = 3f;
        timeUntilFire = .4f;
        
	}
    public void SetPhase2()
	{
        sphereSpeed = 3.2f;
        sphereNumber = 4;
        attackDuration = 2.5f;
        timeUntilFire = .3f;
	}

    public void SetPhase3()
    {
        sphereSpeed = 3.5f;
        sphereNumber = 5;
        attackDuration = 2f;
        timeUntilFire = .2f;
    }

    IEnumerator DoAttack()
    {
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();

        for (int i = 0; i < sphereNumber; i++)
        {
            Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            GameObject sphere = Instantiate(sphereAttackPrefab, spherePos, Quaternion.identity);
            sphere.GetComponent<SimpleSphereBehavior>().SetData(sphereSpeed, timeUntilFire);
            sphere.transform.parent = SphereHolder;

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
        }

        //Wait a Brief time.
        yield return new WaitForSeconds(1f);
        
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
