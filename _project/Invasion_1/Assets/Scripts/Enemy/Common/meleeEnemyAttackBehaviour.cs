using UnityEngine;
using System.Collections;

public class meleeEnemyAttackBehaviour : MonoBehaviour {

    private playerStats _playerStats;

    [Header("attack")]
    [SerializeField]
    private GameObject hitRange;
    private float timeToWaitUntilAttack = 0.4f;
    private float startingAttackTime = 0f;
    private bool isAttacking;
    private float dmg;

    void Awake()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, _playerStats.transform.position) < 3)
        {
            isAttacking = true;
            dmg = 1f + controller.dungeonLevel * 2;
            //print(dmg.ToString());
        }


        if (isAttacking)
        {
            startingAttackTime += Time.deltaTime;
            if (startingAttackTime >= timeToWaitUntilAttack)
            {
                if (Vector3.Distance(hitRange.transform.position, _playerStats.transform.position) < 3)
                {
                    _playerStats.player_currentHealth -= _playerStats.DamageReduction(dmg);
                    print(_playerStats.DamageReduction(dmg).ToString());
                }

                startingAttackTime = 0f;
                isAttacking = false;
            }

        }
    }

}
