using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {


    public static float player_level = 1f;
    public static float player_currentXp = 0f;
    private float player_maxXp = 20f;
    private float player_savedXp;
    private float dmgReceivedFromEnemy;

    [Header("GUI")]
    [SerializeField] private Transform hp_fill;
    [SerializeField] private Transform energy_fill;
    [SerializeField] private SpriteRenderer xp_fill;

    [Header("Stats Visualization")]
    [SerializeField] private TextMesh player_level_txt;
    [SerializeField] private TextMesh player_xp_txt;
    [SerializeField] private TextMesh player_hp_txt;
    [SerializeField] private TextMesh player_strength_txt;
    [SerializeField] private TextMesh player_damage_txt;
    [SerializeField] private TextMesh player_critDamage_txt;
    [SerializeField] private TextMesh player_energy_txt;
    [SerializeField] private TextMesh player_healthReg_txt;
    [SerializeField] private TextMesh player_energyReg_txt;

    [Header("Stats")]
    public float player_weaponDamage;
    private float player_maxHealth = 20f;
    private float player_healthRegen;
    private float player_strength;
    private float player_critDamage;
    private float player_maxEnergy = 20f;
    private float player_energyRegen;



    public float player_currentHealth;
    public float player_currentEnergy;

    [Header("Heart heal")]
    private GameObject[] hearts;
    private float heartHealing;
    private float heartHeal_currentTime = 0f;
    private float heartHeal_maxTime = 3f;


    private perks _perks;
    private float player_finalDamage;


    void Awake()
    {
        DontDestroyOnLoad(this);
        transform.position = Vector3.zero;

        _perks = GameObject.FindGameObjectWithTag("Player").GetComponent<perks>();
    }


    IEnumerator Start()
    {
        player_currentHealth = player_maxHealth;
        player_currentEnergy = player_maxEnergy; ;


        while (true)
        {
            hearts = GameObject.FindGameObjectsWithTag("heart");
            yield return new WaitForSeconds(0.5f);
        }
    }


    void Update()
    {
        Stats_scale();
        Heart_heal();
        MainStats_Regen();
        GUI_sts();
        LevelUp();
        Death();
        Stats_text();
    }

    void Stats_scale()
    {
        player_maxHealth = 20f + (player_level * 0.5f);
        player_healthRegen = 0.1f + (player_level * 0.01f) + _perks.HealthRegeneration()+_perks.HpRegenPerMaxHp(player_maxHealth);
        player_strength = 2f + (player_level * 0.1f);
        player_critDamage = 0.2f + (player_level * 0.01f);
        player_maxEnergy = 20f + (player_level * 1f);
        player_energyRegen = 6f + (player_level * 0.02f) + _perks.EnergyRegen() + _perks.EnergyRegenPerMaxEnergy(player_maxEnergy);
    }

    void Heart_heal()
    {
        foreach (GameObject heart in hearts)
        {
            if (heart != null)
            {
                if (Vector3.Distance(transform.position, heart.transform.position) < 3f)
                {
                    heartHealing++;
                    heartHeal_currentTime = 0f;
                    Destroy(heart.gameObject);
                }
            }
        }

        if (heartHealing > 0)
        {
            heartHeal_currentTime += Time.deltaTime;
            player_currentHealth += (player_maxHealth * 0.0004f) * heartHealing;
            player_currentEnergy += (player_maxEnergy * 0.0002f) * heartHealing;

            if (heartHeal_currentTime > heartHeal_maxTime)
            {
                heartHealing = 0;
                heartHeal_currentTime = 0f;
            }
        }
    }

    void MainStats_Regen()
    {
        if (player_currentHealth < player_maxHealth)
        {
            player_currentHealth += player_healthRegen * Time.deltaTime ;
        }

        if (player_currentEnergy < player_maxEnergy)
        {
            player_currentEnergy += player_energyRegen * Time.deltaTime;
        }

        player_currentHealth = Mathf.Clamp(player_currentHealth, 0f, player_maxHealth);
        player_currentEnergy = Mathf.Clamp(player_currentEnergy, 0f, player_maxEnergy);
    }

    void GUI_sts()
    {
        hp_fill.localScale = new Vector3(player_currentHealth / player_maxHealth, 1f, 1f);
        energy_fill.localScale = new Vector3(player_currentEnergy / player_maxEnergy, 1f, 1f);
        xp_fill.transform.localScale = new Vector3(player_currentXp / player_maxXp, 1f, 1f);
    }

    void Stats_text()
    {
        player_level_txt.text = "Level: " + player_level.ToString("N0");
        player_xp_txt.text = "Experience: " + player_currentXp.ToString("N1") + "/" + player_maxXp.ToString("N1");
        player_hp_txt.text = "Health: " + player_currentHealth.ToString("N1") +"/"+player_maxHealth.ToString("N1");
        player_strength_txt.text = "Strength: " + player_strength.ToString("N0");
        player_damage_txt.text = "Damage: " + (player_weaponDamage+player_strength).ToString("N0");
        player_critDamage_txt.text = "Critical Damage: " + (player_critDamage*100f).ToString("N0") +"%";
        player_energy_txt.text = "Energy: " + player_currentEnergy.ToString("N1") +"/"+ player_maxEnergy.ToString("N1");
        player_healthReg_txt.text = "Health Regeneration: " + player_healthRegen.ToString("N1");
        player_energyReg_txt.text = "Energy Regeneration: " + player_energyRegen.ToString("N1");
    }

    void LevelUp()
    {
        //gain the xp
        if (player_savedXp > 0)
        {
            player_savedXp -= Time.deltaTime * 1f;
            player_currentXp += Time.deltaTime * 1f;

            xp_fill.color = Color.Lerp(xp_fill.color, new Color32(255, 49, 62, 255), Time.deltaTime * 15f);
        }
        else
        {
            xp_fill.color = Color.Lerp(xp_fill.color, new Color32(0, 49, 62, 255), Time.deltaTime * 3f);
        }

        //level up
        if (player_currentXp > player_maxXp)
        {
            player_level++;

            player_maxXp = 15f + (player_level * 3f);
            player_currentXp = 0f;

            player_level++;
        }
    }


    void Death()
    {
        if (player_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    //here we calculate the total dmg the player will deal
    public float CalculatedDmg()
    {
        float critRoll = 0f;
        player_finalDamage = player_strength + player_weaponDamage;

        critRoll = Random.Range(1f, 100f);
        if (critRoll <= _perks.CritChance())
        {
            player_finalDamage = player_finalDamage + player_finalDamage * player_critDamage;
        }

        player_finalDamage = Random.Range(player_finalDamage - (0.1f * player_finalDamage), player_finalDamage + (0.1f * player_finalDamage));

        return player_finalDamage;
    }


    public float DamageReduction(float enemyDamage)
    {
        dmgReceivedFromEnemy = enemyDamage - (enemyDamage * _perks.Armour());
        return dmgReceivedFromEnemy;
    }



}
