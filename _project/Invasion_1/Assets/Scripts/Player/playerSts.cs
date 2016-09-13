using UnityEngine;
using System.Collections;

public class playerSts : MonoBehaviour {


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
    private float maxXp = 20f;
    
    private static float level;

    //we get the tag of the gameObj that we want to scale for hp and energy representation
    private Transform hp_fill;
    private Transform energy_fill;
    private SpriteRenderer xp_fill;

    private primarySts primarySts;


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
        primarySts = GameObject.FindGameObjectWithTag("primaryStsUpgrade").GetComponent<primarySts>();
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
            currentPlayerHP += primarySts.savedHPReg * Time.deltaTime;
        }

        if (currentPlayerENERGY < playerENERGY)
        {
            currentPlayerENERGY += primarySts.savedENERGYReg * Time.deltaTime;
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
            regFromHeart =  (primarySts.savedHP+primarySts.savedENERGY)/2 * 0.1f * heartHealing;

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
        if (savedXp > 0)
        {
            savedXp -= Time.deltaTime * 2f;
            currentXp += Time.deltaTime * 2f;

            xp_fill.color = Color.Lerp(xp_fill.color, Color.white, Time.deltaTime * 15f);
        }
        else
        {
            xp_fill.color = Color.Lerp(xp_fill.color, Color.gray, Time.deltaTime*3f);
        }


        if (currentXp > maxXp)
        {
            maxXp = 20f +(level * 2f);
            currentXp = 0f;
            //currentXp += savedXp;

            primarySts.statPoints += 3f;
            primarySts.perkPoints += 1f;

            print(primarySts.statPoints + " | " + primarySts.perkPoints);
        }
    }
    
    
    void Death()
    {
        if (currentPlayerHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    
}
