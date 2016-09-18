using UnityEngine;
using System.Collections;

public class generalWeaponBehaviour : MonoBehaviour {

    private Rigidbody rigid;
    private GameObject player;
    private playerSts _playerSts;
    private primarySts primarySts;
    private GameObject hand;
    private GameObject[] weapons;

    //if it is equppied
    public bool isEquipped;

    [Header("Misc")]
    [SerializeField] private GameObject rotateAroundObj;
    [SerializeField] private GameObject statsPanel;


    [Header("Stats Text")]
    [SerializeField] private TextMesh text_hp;
    [SerializeField] private TextMesh text_dmg;
    [SerializeField] private TextMesh text_energy;

    
    [Header("Quality")]
    [SerializeField] private TextMesh textQuality;
    private int qualityLevel;
    private float randomQuality;

    [Header("Roll chances")]
    [SerializeField] private float numberOfStatPointsAvailable;
    [SerializeField] private float HpRollChance;
    [SerializeField] private float DmgRollChance;
    [SerializeField] private float EnergyRollChance;


    [Header("Stats Rolled")]
    public float weaponHP;
    public float weaponDMG;
    public float weaponENERGY;

    private float random;





    void Awake()
    {
        rigid = GetComponent<Rigidbody>();


        weapons = GameObject.FindGameObjectsWithTag("weapon");
        player = GameObject.FindGameObjectWithTag("Player");
        _playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        primarySts = GameObject.FindGameObjectWithTag("primaryStsUpgrade").GetComponent<primarySts>();
        hand = GameObject.FindGameObjectWithTag("hand");
    }

          

    void Start()
    {
        transform.rotation = Quaternion.Euler(0,Random.Range(0f,360f),0);
        rigid.AddForce(new Vector3(0,1f,1f) * 8000f);

        Roll_stats();
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
        transform.RotateAround(rotateAroundObj.transform.position,Vector3.up, Time.deltaTime * 20f);

    }

    void Roll_stats()
    {
        numberOfStatPointsAvailable = LevelOFQuality();

        while (numberOfStatPointsAvailable > 0)
        {
            random = Random.Range(1, HpRollChance + DmgRollChance + EnergyRollChance);
            if (random >= 1 && random < HpRollChance)
            {
                weaponHP++;
                numberOfStatPointsAvailable--;
            }
            else if (random >= HpRollChance && random < (HpRollChance + DmgRollChance))
            {
                weaponDMG++;
                numberOfStatPointsAvailable--;
            }
            else if (random >= (HpRollChance + DmgRollChance) && random < (HpRollChance + DmgRollChance + EnergyRollChance)) 
            {
                weaponENERGY++;
                numberOfStatPointsAvailable--;

            }
        }
    }


    void Equip_weapon()
    {
        if (isEquipped)
        {
            rigid.isKinematic = true;
            GetComponent<BoxCollider>().enabled = false;
            transform.parent = hand.transform;
            transform.localPosition = new Vector3(0f,0f,0f);
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            statsPanel.gameObject.SetActive(false);

            _playerSts.playerHP = primarySts.savedHP + weaponHP;
            _playerSts.playerDMG = primarySts.savedDMG + weaponDMG;
            _playerSts.playerENERGY = primarySts.savedENERGY + weaponENERGY;

            playerSts.saved_weaponHP = weaponHP;
            playerSts.saved_weaponDMG = weaponDMG;
            playerSts.saved_weaponENERGY = weaponENERGY;
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
        if (playerSts.saved_weaponHP - weaponHP < 0)
        {
            text_hp.text = "+" + (-playerSts.saved_weaponHP + weaponHP).ToString("N0");
            text_hp.color = Color.green;
        }
        else if (playerSts.saved_weaponHP - weaponHP > 0)
        {
            text_hp.text = "-" + (playerSts.saved_weaponHP - weaponHP).ToString("N0");
            text_hp.color = Color.red;
        }
        else if (playerSts.saved_weaponHP - weaponHP == 0)
        {
            text_hp.text =  (playerSts.saved_weaponHP - weaponHP).ToString("N0");
            text_hp.color = Color.gray;
        }



        if (playerSts.saved_weaponDMG - weaponDMG < 0)
        {
            text_dmg.text = "+" + (-playerSts.saved_weaponDMG + weaponDMG).ToString("N0");
            text_dmg.color = Color.green;
        }
        else if (playerSts.saved_weaponDMG - weaponDMG > 0)
        {
            text_dmg.text = "-" + (playerSts.saved_weaponDMG - weaponDMG).ToString("N0");
            text_dmg.color = Color.red;
        }
        else if (playerSts.saved_weaponDMG - weaponDMG == 0)
        {
            text_dmg.text = (playerSts.saved_weaponDMG - weaponDMG).ToString("N0");
            text_dmg.color = Color.gray;
        }


        if (playerSts.saved_weaponENERGY - weaponENERGY < 0)
        {
            text_energy.text = "+" + (-playerSts.saved_weaponENERGY + weaponENERGY).ToString("N0");
            text_energy.color = Color.green;
        }
        else if (playerSts.saved_weaponENERGY - weaponENERGY > 0)
        {
            text_energy.text = "-" + (playerSts.saved_weaponENERGY - weaponENERGY).ToString("N0");
            text_energy.color = Color.red;
        }
        else if (playerSts.saved_weaponENERGY - weaponENERGY == 0)
        {
            text_energy.text = (playerSts.saved_weaponENERGY - weaponENERGY).ToString("N0");
            text_energy.color = Color.gray;
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


    float LevelOFQuality()
    {
        randomQuality = Random.Range(1, 1000f);
        float statsPoint = 0;

        if (randomQuality >= 1 && randomQuality < 650)
        {
            qualityLevel = 1;
            textQuality.text = "Trivial Quality";
            textQuality.color = Color.gray;
            statsPoint = 5f;
        }
        else if (randomQuality >= 650 && randomQuality < 900)
        {
            qualityLevel = 2;
            textQuality.text = "Average Quality";
            textQuality.color = new Color32(46, 172, 189, 255);
            statsPoint = 7f;
        }
        else if (randomQuality >= 900 && randomQuality < 996)
        {
            qualityLevel = 3;
            textQuality.text = "Exquisite Quality";
            textQuality.color = new Color32(29, 221, 46, 255);
            statsPoint = 9f;
        }
        else if (randomQuality >= 996 && randomQuality <= 1000)
        {
            qualityLevel = 4;
            textQuality.text = "Luxurious Quality";
            textQuality.color = new Color32(236, 46, 2, 255);
            statsPoint = 13f;
        }
        //print(statsPoint.ToString());
        return statsPoint;
        
    }
}
