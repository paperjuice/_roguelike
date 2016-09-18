using UnityEngine;
using System.Collections;

public class playerSts : MonoBehaviour {


    private float finalDmg;

    public static float saved_weaponDMG;
    public static float saved_weaponENERGY;
    public static float saved_weaponHP;


    public float playerHP = 21f;
    public float playerDMG= 21f;
    public float playerENERGY = 21f;
    
    //sts that are going to be manipulate
    [Header("Main Stats")]
    public float currentPlayerHP;
    public float currentPlayerENERGY;

    [Header("Level")]
    public static float currentXp;
    public float savedXp;
    public float maxXp = 20f;
    
    public static float level = 1f;

    //we get the tag of the gameObj that we want to scale for hp and energy representation
    private Transform hp_fill;
    private Transform energy_fill;
    private SpriteRenderer xp_fill;

    private primarySts _primarySts;
    private perks _perks;


    //heart heal
    private float heartHeal_maxTime=3f;
    private float heartHeal_currentTime=0;
    private int heartHealing = 0;
    private GameObject[] hearts;
    private float heartCollectRange = 2f;




    void Awake()
    {
        DontDestroyOnLoad(this);
        transform.position = Vector3.zero;

        hp_fill = GameObject.FindGameObjectWithTag("hp_fill").transform;
        energy_fill = GameObject.FindGameObjectWithTag("energy_fill").transform;
        xp_fill = GameObject.FindGameObjectWithTag("xp_fill").GetComponent<SpriteRenderer>();
        _primarySts = GameObject.FindGameObjectWithTag("primaryStsUpgrade").GetComponent<primarySts>();
        _perks = GameObject.FindGameObjectWithTag("primaryStsUpgrade").GetComponent<perks>();
    }

    IEnumerator Start()
    {
        currentPlayerHP = playerHP;
        currentPlayerENERGY = playerENERGY;
        

        while (true)
        {
            hearts = GameObject.FindGameObjectsWithTag("heart");
            yield return new WaitForSeconds(0.5f);
        }
    }


    void Update()
    {
        GUI_sts();
        MainStats_Regen();
        LevelUp();

        Death();
        Heart_heal();
        CollectHeart();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "heart")
        {
            heartHealing ++;
            Destroy(col.gameObject);
        }
    }




    //regenerate hp and energy based on hpReg and energyReg
    void MainStats_Regen()
    {
        if (currentPlayerHP < playerHP)
        {
            currentPlayerHP += _primarySts.savedHPReg * Time.deltaTime;
        }

        if (currentPlayerENERGY < playerENERGY)
        {
            currentPlayerENERGY += _primarySts.savedENERGYReg * Time.deltaTime;
        }

        currentPlayerHP = Mathf.Clamp(currentPlayerHP,0f , playerHP);
        currentPlayerENERGY = Mathf.Clamp(currentPlayerENERGY,0f , playerENERGY);
    }

    //show on gui the sts
    void GUI_sts()
    {
        hp_fill.localScale = new Vector3(currentPlayerHP / playerHP, 1f, 1f);
        energy_fill.localScale = new Vector3(currentPlayerENERGY/playerENERGY, 1f, 1f);
        xp_fill.transform.localScale = new Vector3(currentXp / maxXp, 1f, 1f);
    }


    public float Heart_heal()
    {
        float regFromHeart;

        if (heartHealing>0)
        {
            heartHeal_currentTime += Time.deltaTime;
            regFromHeart =  (_primarySts.savedHP+ _primarySts.savedENERGY)/2 * 0.1f * heartHealing;

            if (heartHeal_currentTime > heartHeal_maxTime)
            {
                heartHealing = 0;
                heartHeal_currentTime = 0f;
            }
            return regFromHeart;
        }
        else
        {
            return 0f;
        }
    }

    void CollectHeart()
    {
        foreach (GameObject heart in hearts)
        {
            if (heart != null)
            {
                if (Vector3.Distance(transform.position, heart.transform.position) < heartCollectRange)
                {
                    heartHealing++;
                    heartHeal_currentTime = 0;
                    Destroy(heart.gameObject);
                }
            }
        }
    }

    void LevelUp()
    {
        //gain the xp
        if (savedXp > 0)
        {
            savedXp -= Time.deltaTime * 2f;
            currentXp += Time.deltaTime * 2f;

            xp_fill.color = Color.Lerp(xp_fill.color, new Color32(255,49,62,255), Time.deltaTime * 15f);
        }
        else
        {
            xp_fill.color = Color.Lerp(xp_fill.color, new Color32(0, 49, 62, 255), Time.deltaTime*3f);
        }

        //level up
        if (currentXp > maxXp)
        {
            level++;

            maxXp = 20f +(level * 2f);
            currentXp = 0f;
            //currentXp += savedXp;

            primarySts.playerDMG_level += 1f;
            primarySts.playerENERGY_level += 1f;
            primarySts.playerHP_level += 1f;

           // print(primarySts.statPoints + " | " + primarySts.perkPoints);
        }
    }
    
    
    void Death()
    {
        if (currentPlayerHP <= 0)
        {
            Destroy(gameObject);
        }
    }


    //here we calculate the total dmg the player will deal
    public float CalculatedDmg()
    {
        float chanceToCrit = 0f;
        finalDmg = playerDMG;

        chanceToCrit = Random.Range(1f,100f);
        if (chanceToCrit<=_perks.CritChance())
        {
            finalDmg = finalDmg + finalDmg * _primarySts.savedCritDMG;
        }

        finalDmg = Random.Range(finalDmg - (0.1f * finalDmg), finalDmg + (0.1f * finalDmg));

        return finalDmg;
    }
    
}
