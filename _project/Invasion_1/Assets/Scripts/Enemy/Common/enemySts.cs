using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySts : MonoBehaviour {
    [SerializeField] private float chanceToGetWeapon;
    [SerializeField] private GameObject[] bloodSplatter;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject[] weaponDrops;
    [SerializeField] private GameObject bloodSplash;
    [SerializeField] private GameObject deadBody;

    private GameObject[] spawnPoints;
    private List<GameObject> listOfSpawnPoints;
    private int randomSpawnPoint;

    private playerSts player;
    private enemyMeleeGeneralBehaviour enemyMeleeGeneralBehaviour;

    private int randomCollectibleChance;
    private float randomWeaponChance;
    public bool isHit;
    public float enemyHP;
    private float saved_enemyHp;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        enemyMeleeGeneralBehaviour = GetComponent<enemyMeleeGeneralBehaviour>();
    }

    IEnumerator Start()
    {
        saved_enemyHp = enemyHP;

        yield return new WaitForSeconds(0.2f);
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

    }

    void Update()
    {
        Death();
        EnemyHit();
    }

    void Death()
    {
        if (enemyHP <= 0)
        {
            CollectibleDrop();
            WeaponDrop();
            Instantiate(deadBody, transform.position += Vector3.up, transform.rotation);
            Destroy(gameObject);
            player.savedXp += 1 * controller.dungeonLevel;
        }
    }

    void EnemyHit()
    {
        if (isHit)
        {
            Instantiate(bloodSplash, transform.position, transform.rotation);
            Instantiate(bloodSplatter[Random.Range(0, bloodSplatter.Length)], bloodSplatter[Random.Range(0, bloodSplatter.Length)].transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z), bloodSplatter[Random.Range(0, bloodSplatter.Length)].transform.rotation = Quaternion.Euler(90f, Random.Range(0f,360f), 0f));

            isHit = false;
        }
    }

    void CollectibleDrop()
    {
        randomCollectibleChance = Random.Range(1,10);
        if (randomCollectibleChance == 1)
        {
            Instantiate(heart, transform.position, transform.rotation);
        }
    }

    void WeaponDrop()
    {
        randomWeaponChance = Random.Range(1f, 100f);
        if (randomWeaponChance <= chanceToGetWeapon)
        {
            Instantiate(weaponDrops[Random.Range(0, weaponDrops.Length)], transform.position, transform.rotation);
        }
    }
    
}
