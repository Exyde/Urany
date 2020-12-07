using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1 : MonoBehaviour
{
    [Header("Attack 1")]

    public GameObject sphereAttackPrefab;
    public float sphereSpeed;
    public int sphereNumber;
    public float attackDuration;
    public float spawnRadius = 1.2f;

    private Uranie uranie;

	void Start()
	{
        uranie = GetComponent<Uranie>();
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
            Vector2 spherePos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
            GameObject sphere = Instantiate(sphereAttackPrefab, spherePos, Quaternion.identity);

            currentSpheres.Add(sphere);

            yield return new WaitForSeconds(attackDuration / sphereNumber);
        }


        yield return new WaitForSeconds(.2f);

        for (int i = 0; i < sphereNumber; i++)
        {
            Destroy(currentSpheres[i].gameObject);
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
