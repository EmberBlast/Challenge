using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyProfile enemyProfile;
    [SerializeField] private Gun enemyGun;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform gunExitPoint;

    private bool canAttack = false;
    private float actionTime = 0.2f;
    private NavMeshAgent agent;
    private float fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Init();
    }

    private void Init()
    {
        gameObject.name = enemyProfile.name;
        agent.speed = enemyProfile.EnemySpeed;
        agent.stoppingDistance = enemyProfile.EnemyRange/2;
        StartCoroutine(ChecktDistance());
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

        if (canAttack)
        {

            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time - fireTimer > enemyGun.GunFiringTime / enemyGun.GunFiringRate)
        {
            fireTimer = Time.time;
            enemyGun.Shoot(gunExitPoint);
        }
    }

    private IEnumerator ChecktDistance()
    {
        WaitForSeconds seconds = new WaitForSeconds(actionTime);
        float distance = 0;

        while (true)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
            canAttack = distance <= enemyProfile.EnemyRange;
            yield return seconds;
        }
    }
}
