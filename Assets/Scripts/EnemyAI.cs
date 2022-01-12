using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    List<GameObject> currentPlayers;
    public float health = 100f;
    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    PlayerList pl;
    PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        //player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        pl = GameObject.FindObjectOfType<PlayerList>();
    }

    void Start()
    {
        currentPlayers = pl.GetList();
    }

    void Update()
    {

        if (photonView.IsMine)
        {
            currentPlayers = pl.GetList();
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                if (!walkPointSet) SearchWalkPoint();

                if (walkPointSet)
                    agent.SetDestination(walkPoint);

                Vector3 distanceToWalkPoint = transform.position - walkPoint;

                //Walkpoint reached
                if (distanceToWalkPoint.magnitude < 1f)
                    walkPointSet = false;
            }

            if (playerInSightRange && !playerInAttackRange)
            {
                agent.SetDestination(FindClosestPlayer(currentPlayers).transform.position);
            }

            if (!playerInSightRange && playerInAttackRange)
            {
                // Make sure enemy doesnt move
                agent.SetDestination(transform.position);

                Debug.Log(FindClosestPlayer(currentPlayers).transform);
                transform.LookAt(FindClosestPlayer(currentPlayers).transform);
                if (!alreadyAttacked)
                {
                    // Attack code here
                    Rigidbody rb = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                    rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                    rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                    alreadyAttacked = true;
                    Invoke(nameof(ResetAttack), timeBetweenAttacks);
                }

            }
        }
    }

    GameObject[] FindGameObjectsWithLayer(int layer)
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer && goArray[i].gameObject.GetComponent<ObjectStatsManager>())
            {

                    goList.Add(goArray[i]);
                    Debug.Log(goList[i].gameObject.name);
            }
        }
        if (goList.Count == 0) 
        { 
            return null;
        }

        return goList.ToArray();

    }

    GameObject FindClosestPlayer(List<GameObject> goArray)
    {
        GameObject go = null;
        for (int i = 0; i < goArray.Count; i++)
        {
            if(go == null)
            {
                go = goArray[i];

            }
            else
            {
                if (Vector3.Distance(gameObject.transform.position, goArray[i].transform.position) > Vector3.Distance(gameObject.transform.position , go.transform.position))
                    go = goArray[i];
            }
        }

        if (go != null)
            return go;
        else
            return null;
    }

    [PunRPC]
    void Patrolling()
    {
                        if (!walkPointSet) SearchWalkPoint();

                if (walkPointSet)
                    agent.SetDestination(walkPoint);

                Vector3 distanceToWalkPoint = transform.position - walkPoint;

                //Walkpoint reached
                if (distanceToWalkPoint.magnitude < 1f)
                    walkPointSet = false;
    }

    void SearchWalkPoint()
    { 
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    [PunRPC]
    void ChasePlayer()
    {
        agent.SetDestination(FindClosestPlayer(currentPlayers).transform.position);
    }

    [PunRPC]
    void AttackPlayer()
    {
        // Make sure enemy doesnt move
        agent.SetDestination(transform.position);

        Debug.Log(FindClosestPlayer(currentPlayers).transform);
        transform.LookAt(FindClosestPlayer(currentPlayers).transform);
        if(!alreadyAttacked)
        {
            // Attack code here
            Rigidbody rb = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
       
    }

    void TryAttack()
    {

    }

    void MeleeAttack()
    {

    }

    void RangedAttack()
    {

    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
