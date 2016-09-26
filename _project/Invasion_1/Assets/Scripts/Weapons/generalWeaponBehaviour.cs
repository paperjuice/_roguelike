using UnityEngine;
using System.Collections;

public class generalWeaponBehaviour : MonoBehaviour {

    private Rigidbody rigid;
    private GameObject player;
    private playerStats _playerStats;
    private GameObject hand;
    private GameObject[] weapons;

    //if it is equppied
    public bool isEquipped;

    [Header("Misc")]
    [SerializeField] private GameObject rotateAroundObj;
    [SerializeField] private GameObject statsPanel;


    [Header("Stats Text")]
    [SerializeField] private TextMesh text_dmg;


    [Header("Quality")]
    [SerializeField] private TextMesh textQuality;
    private int qualityLevel;
    private float randomQuality;


    [Header("1-LongSword | 2-Revolver | 3-Staff | 4-Shield | 5-Autogun")]
    [SerializeField][Range(1,5)] private int weaponId;

    [Header("Stats Rolled")]
    public float weapon_energyCost;
    float weapon_weaponDamage = 0;
    

    private float random;





    void Awake()
    {
        rigid = GetComponent<Rigidbody>();


        weapons = GameObject.FindGameObjectsWithTag("weapon");
        player = GameObject.FindGameObjectWithTag("Player");
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        hand = GameObject.FindGameObjectWithTag("hand");
    }



    void Start()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        rigid.AddForce(new Vector3(0, 1f, 1f) * 8000f);

        RollTheDamageBasedOnID();
    }

    void Update()
    {
        Rotate();
        Equip_weapon();
        Show_stats_as_text();
        Distance_between_weapons();
    }




    void Rotate()
    {
        transform.RotateAround(rotateAroundObj.transform.position, Vector3.up, Time.deltaTime * 20f);
    }
    
    void Equip_weapon()
    {
        if (isEquipped)
        {
            rigid.isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            transform.parent = hand.transform;
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            statsPanel.gameObject.SetActive(false);
            _playerStats.player_weaponDamage = weapon_weaponDamage;
        }
        else
        {
            rigid.isKinematic = false;
            GetComponent<BoxCollider>().enabled = true;
            transform.parent = null;

            if (Vector3.Distance(transform.position, player.transform.position) < 5)
            {
                statsPanel.gameObject.SetActive(true);
            }
            else
            {
                statsPanel.gameObject.SetActive(false);
            }
        }

    }


    void Show_stats_as_text()
    {
        if (_playerStats.player_weaponDamage - weapon_weaponDamage < 0)
        {
            text_dmg.text = "+ Damage: " + (-_playerStats.player_weaponDamage + weapon_weaponDamage).ToString("N1");
            text_dmg.color = Color.green;
        }
        else if (_playerStats.player_weaponDamage - weapon_weaponDamage > 0)
        {
            text_dmg.text = "- Damage: " + (_playerStats.player_weaponDamage - weapon_weaponDamage).ToString("N1");
            text_dmg.color = Color.red;
        }
        else if (_playerStats.player_weaponDamage - weapon_weaponDamage == 0)
        {
            text_dmg.text = "Damage: " + (_playerStats.player_weaponDamage - weapon_weaponDamage).ToString("N1");
            text_dmg.color = Color.gray;
        }
    }


    void Distance_between_weapons()
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon != null)
            {
                if (Vector3.Distance(transform.position, weapon.transform.position) < 10 && !weapon.GetComponent<generalWeaponBehaviour>().isEquipped)
                {
                    rigid.AddForce((transform.position - weapon.transform.position) * 100f);
                }
            }
        }
    }

    void RollTheDamageBasedOnID()
    {
        switch (weaponId)
        {
            case 1:
                LongSword_RolledWeaponDamage();
                break;
            case 2:
                Revolver_RolledWeaponDamage();
                break;
            case 3:
                Staff_RolledWeaponDamage();
                break;
            case 4:
                Shield_RolledWeaponDamage();
                break;
            case 5:
                Autogun_RolledWeaponDamage();
                break;
        }
    }


    void LongSword_RolledWeaponDamage()
    {
        randomQuality = Random.Range(1, 1000f);
        

        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;

            weapon_weaponDamage = Random.Range(5f,10f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(10f, 12f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);

            //+4
            weapon_weaponDamage = Random.Range(9f, 14f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(9f, 11f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);

            //+5
            weapon_weaponDamage = Random.Range(14f, 19f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(8f, 10f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);

            //+6
            weapon_weaponDamage = Random.Range(20f, 25f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(7f, 9f) + (0.5f * controller.dungeonLevel);
        }

    }

    void Revolver_RolledWeaponDamage()
    {
        randomQuality = Random.Range(1, 1000f);


        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;

            weapon_weaponDamage = Random.Range(5f, 10f) + (3f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(3f, 4f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);

            //+4
            weapon_weaponDamage = Random.Range(9f, 14f) + (3f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(2.5f, 3.5f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);

            //+5
            weapon_weaponDamage = Random.Range(14f, 19f) + (3f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(2f, 3f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);

            //+6
            weapon_weaponDamage = Random.Range(20f, 25f) + (3f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(1.5f, 2.5f) + (0.2f * controller.dungeonLevel);
        }

    }

    void Staff_RolledWeaponDamage()
    {
        randomQuality = Random.Range(1, 1000f);


        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;

            weapon_weaponDamage = Random.Range(12f, 15f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(10f, 12f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);

            //+4
            weapon_weaponDamage = Random.Range(16f, 19f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(9f, 11f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);

            //+5
            weapon_weaponDamage = Random.Range(21f, 24f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(8f, 10f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);

            //+6
            weapon_weaponDamage = Random.Range(26f, 29f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(7f, 9f) + (0.5f * controller.dungeonLevel);
        }

    }

    void Shield_RolledWeaponDamage()
    {
        randomQuality = Random.Range(1, 1000f);


        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;

            weapon_weaponDamage = Random.Range(5f, 10f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(8f, 10f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);

            //+4
            weapon_weaponDamage = Random.Range(9f, 14f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(7f, 9f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);

            //+5
            weapon_weaponDamage = Random.Range(14f, 19f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(6f, 8f) + (0.5f * controller.dungeonLevel);
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);

            //+6
            weapon_weaponDamage = Random.Range(20f, 25f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(5f, 7f) + (0.5f * controller.dungeonLevel);
        }

    }

    void Autogun_RolledWeaponDamage()
    {
        randomQuality = Random.Range(1, 1000f);


        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;

            weapon_weaponDamage = Random.Range(2f, 4f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(2f, 3f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);

            //+4
            weapon_weaponDamage = Random.Range(5f, 7f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(1.5f, 2.5f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);

            //+5
            weapon_weaponDamage = Random.Range(8f, 10f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(1f, 2f) + (0.2f * controller.dungeonLevel);
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);

            //+6
            weapon_weaponDamage = Random.Range(11f, 13f) + (0.5f * controller.dungeonLevel);
            weapon_energyCost = Random.Range(0.5f, 1.5f) + (0.2f * controller.dungeonLevel);
        }

    }

}
