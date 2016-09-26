using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySts : MonoBehaviour {

    [SerializeField] private GameObject hpFill;

    [SerializeField] private float magicFind;
    [SerializeField] private GameObject[] bloodSplatter;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject[] perkTokens;
    [SerializeField] private GameObject[] weaponDrops;
    [SerializeField] private GameObject bloodSplash;
    [SerializeField] private GameObject deadBody;

    private perks _perks;
    private GameObject[] spawnPoints;
    private List<GameObject> listOfSpawnPoints;
    private int randomSpawnPoint;
    

    private enemyMeleeGeneralBehaviour enemyMeleeGeneralBehaviour;

    private float randomCollectibleChance;
    private float randomWeaponChance;
    private float randomPerkChance;

    public bool isHit;
    public float enemyHP;
    private float saved_enemyHp;

    private float randomBloodSize=0f;

    

    void Awake()
    {
        _perks = GameObject.FindGameObjectWithTag("Player").GetComponent<perks>();
        enemyMeleeGeneralBehaviour = GetComponent<enemyMeleeGeneralBehaviour>();
    }

    IEnumerator Start()
    {
        enemyHP = EnemyHp_scale();
        saved_enemyHp = enemyHP;

        yield return new WaitForSeconds(0.2f);
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnPoint");

    }

    void Update()
    {
        Death();
        EnemyHit();
        GUI_sts();
    }

    void Death()
    {
        if (enemyHP <= 0)
        {
            HearthDrop();
            WeaponDrop();
            PerkDrop();
            Instantiate(deadBody, transform.position += Vector3.up, transform.rotation);
            Destroy(gameObject);
            playerStats.player_currentXp += 1 * controller.dungeonLevel;
        }
    }

    float EnemyHp_scale()
    {
        enemyHP = 20f + (controller.dungeonLevel * 8f);
        return enemyHP;
    }

    void EnemyHit()
    {

        if (isHit)
        {
            randomBloodSize = Random.Range(0.5f, 3f);
            Instantiate(bloodSplash, transform.position, transform.rotation);
            
            Instantiate(bloodSplatter[Random.Range(0, bloodSplatter.Length)], bloodSplatter[Random.Range(0, bloodSplatter.Length)].transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z), bloodSplatter[Random.Range(0, bloodSplatter.Length)].transform.rotation = Quaternion.Euler(90f, Random.Range(0f,360f), 0f));
            bloodSplatter[Random.Range(0, bloodSplatter.Length)].transform.localScale = new Vector3(randomBloodSize, randomBloodSize, 1 );

            isHit = false;
        }
    }

    void HearthDrop()
    {
        randomCollectibleChance = Random.Range(1f,100f);
        if (randomCollectibleChance <= _perks.MagicFind())
        {
            Instantiate(heart, transform.position += new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
    }

    void WeaponDrop()
    {
        randomWeaponChance = Random.Range(1f, 100f);
        if (randomWeaponChance <= magicFind + _perks.MagicFind())
        {
            Instantiate(weaponDrops[Random.Range(0, weaponDrops.Length)], transform.position+= new Vector3(0f,1.5f,0f), transform.rotation);
        }
    }

    void PerkDrop()
    {
        randomPerkChance = Random.Range(1f, 100f);
        if (randomPerkChance <= _perks.MagicFind())
        {
            Instantiate(perkTokens[Random.Range(0, perkTokens.Length)], transform.position += new Vector3(0f, 0.5f, 0f), transform.rotation);
        }
    }
    
    void GUI_sts()
    {
        hpFill.transform.localScale = new Vector3((enemyHP / saved_enemyHp), 1f, 1f);
    }


    




}
