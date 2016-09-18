using UnityEngine;
using System.Collections;

public class primarySts : MonoBehaviour {

    public static float statPoints;
    public static float perkPoints;
    
    public static float playerHP_level;
    public static float playerDMG_level;
    public static float playerENERGY_level;

    [Header("Upgrade Potency")]
    [Header("HP")]
    [SerializeField] float upgradePotency_HP = 1f;
    [SerializeField] float upgradePotency_HPReg = 0.1f;

    [Header("DMG")]
    [SerializeField] float upgradePotency_DMG = 1f;
    [SerializeField] float upgradePotency_CritDMG = 1f;
    
    [Header("Energy")]
    [SerializeField] float upgradePotency_ENERGY = 1f;
    [SerializeField] float upgradePotency_ENERGYReg = 0.3f;


    [Header("GUI")]
    [SerializeField] TextMesh level_txt;
    [SerializeField] TextMesh xp_txt;
    [SerializeField] TextMesh hp_txt;
    [SerializeField] TextMesh dmg_txt;
    [SerializeField] TextMesh energy_txt;
    [SerializeField] TextMesh hpReg_txt;
    [SerializeField] TextMesh crtDmg_txt;
    [SerializeField] TextMesh energyReg_txt;
    
    
    [Header("Saved Sts")]
    private playerSts _playerSts;
    public float savedHP;
    public float savedHPReg;

    public float savedDMG;
    public float savedCritDMG;
    
    public float savedENERGY;
    public float savedENERGYReg;


    void Awake()
    {
        _playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
    }
    

    void Update()
    {
        GUI_sts();


        savedHP = 21 + (playerHP_level * upgradePotency_HP);
        savedHPReg = 0.1f + (playerHP_level * upgradePotency_HPReg) + _playerSts.Heart_heal();

        savedDMG = 21 + (playerDMG_level * upgradePotency_DMG);
        savedCritDMG = 0.5f + (playerDMG_level * upgradePotency_CritDMG);
        
        savedENERGY = 21 + (playerENERGY_level * upgradePotency_ENERGY);
        savedENERGYReg = 5f + (playerENERGY_level * upgradePotency_ENERGYReg) + _playerSts.Heart_heal(); 
    }

    void GUI_sts()
    {
        level_txt.text = "Level: " + playerSts.level.ToString("N0");
        xp_txt.text = "Experience: " + playerSts.currentXp.ToString("N0") + "/" + _playerSts.maxXp.ToString("N0");

        hp_txt.text = "Health: " + _playerSts.playerHP.ToString("N1");
        dmg_txt.text = "Damage:" + _playerSts.playerDMG.ToString("N1");
        energy_txt.text = "Energy: " + _playerSts.playerENERGY.ToString("N1");

        hpReg_txt.text = "Health Regen: " + savedHPReg.ToString("N1")+ "/s";
        energyReg_txt.text = "Energy Regen: " + savedENERGYReg.ToString("N1") + "/s";

        crtDmg_txt.text = "Crit Damage: " + (savedCritDMG*100 ).ToString("N1")+"%";
    }



}
