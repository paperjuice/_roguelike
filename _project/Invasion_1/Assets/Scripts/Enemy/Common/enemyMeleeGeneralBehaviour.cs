using UnityEngine;
using System.Collections;

public class enemyMeleeGeneralBehaviour : MonoBehaviour {
    
    private playerSts player;
    private GameObject[] enemies;
    private GameObject[] walls;
    private bool isActive;


    //movement
    public Vector3 lastRememberedPoistion;
    private bool isMovingToLastPlayerKnownLocation;
    private bool isDodgingEnemyPosition;
    private float timeToMoveAwayFromEnemy = 1f;

    //raycasting
    private RaycastHit raycast;
    private Ray ray;
    private float dist = 30f;


    [SerializeField] private float enemyMs;
    private float saved_enemyMs;
    [SerializeField] private float enemyRs;
    private float saved_enemyRs;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
    }


    IEnumerator Start()
    {
        saved_enemyMs = enemyMs;
        saved_enemyRs = enemyRs;

        yield return new WaitForSeconds(1f);
        walls = GameObject.FindGameObjectsWithTag("wall");
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        isActive = true;
    }

    void Update()
    {
        if (isActive)
        {
            RotateTowardsPlayer();
            Movement_v2();
            MoveToLastPlayerKnownPosition();
            //DodgeEnemyPosition();

            Attack();
        }
    }


    void RotateTowardsPlayer()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(enemy.transform.position, player.transform.position) < dist)
                {
                    Vector3 rotate = player.transform.position - enemy.transform.position;
                    rotate.y = 0f;

                    Quaternion rotateTowards = Quaternion.LookRotation(rotate);
                    enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, rotateTowards, enemyRs * Time.deltaTime);
                }
            }
        }
    }

    void Movement_v2()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(enemy.transform.position, player.transform.position) < 20f)
                {
                    //enemy.GetComponent<Animator>().SetTrigger("move");
                    enemy.transform.position += Time.deltaTime * enemy.transform.forward * saved_enemyMs;
                    lastRememberedPoistion = player.transform.position;
                }
                else
                {
                    if (lastRememberedPoistion != Vector3.zero)
                    {
                        isMovingToLastPlayerKnownLocation = true;
                    }
                }
            }
        }

    }

    void MoveToLastPlayerKnownPosition()
    {
        if (isMovingToLastPlayerKnownLocation)
        {
            foreach (GameObject enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, lastRememberedPoistion, Time.deltaTime * saved_enemyMs);
                    if (lastRememberedPoistion == enemy.transform.position)
                    {
                        isMovingToLastPlayerKnownLocation = false;
                    }
                }
            }
        }
    }
    void DodgeEnemyPosition()
    {
        if (isDodgingEnemyPosition)
        {
            timeToMoveAwayFromEnemy -= Time.deltaTime;
            transform.position += Time.deltaTime * transform.right * saved_enemyMs;

            if (timeToMoveAwayFromEnemy <= 0f)
            {
                timeToMoveAwayFromEnemy = 1f;
                isDodgingEnemyPosition = false;
            }
        }
    }


    void Attack()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                if (Vector3.Distance(enemy.transform.position, player.transform.position) < 2f)
                {
                    enemy.GetComponent<Animator>().SetTrigger("attack");
                }
            }
        }
    }
}
