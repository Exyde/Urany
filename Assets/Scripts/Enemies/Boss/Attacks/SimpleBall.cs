using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBall : MonoBehaviour
{
    [Header("Simple Ball attack")]

    [Header ("Phase 1")]
    public GameObject sphereAttackPrefab;

    public float sphereSpeed;
    public int sphereNumber;
    public float attackDuration;
    public float timeUntilFire;

    public float spawnRadius = 1.2f;

    //Phase 2
    // Duration : 2
    // Sphere Number : 4
    // 
    //Phase 3

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
        sphereSpeed += 1f;
        sphereNumber = 4;
        attackDuration = 2f;
        timeUntilFire = .35f;
	}

    public void SetPhase3()
    {
        sphereSpeed += .2f;
        sphereNumber = 6;
        attackDuration = 1.5f;
        timeUntilFire = .3f;
    }

    IEnumerator DoAttack()
    {
        BeginAttack();

        List<GameObject> currentSpheres = new List<GameObject>();

        for (int i = 0; i < sphereNumber; i++)
        {
            Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle.normalized * spawnRadius;
            GameObject sphere = Instantiate(sphereAttackPrefab, spherePos, Quaternion.identity);
            sphere.GetComponent<SimpleSphereBehavior>().speed = sphereSpeed;

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
