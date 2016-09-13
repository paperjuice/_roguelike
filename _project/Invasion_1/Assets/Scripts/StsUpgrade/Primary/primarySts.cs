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
    [SerializeField] TextMesh hp_txt;
    [SerializeField] TextMesh dmg_txt;
    [SerializeField] TextMesh energy_txt;
    [SerializeField] TextMesh statPoint_txt;

    [Header("Saved Sts")]
    private playerSts playerSts;
    public float savedHP;
    public float savedHPReg;

    public float savedDMG;
    public float savedCritDMG;
    
    public float savedENERGY;
    public float savedENERGYReg;


    void Awake()
    {
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
    }
    

    void Update()
    {
        GUI_sts();


        savedHP = 21 + (playerHP_level * upgradePotency_HP);
        savedHPReg = 0.1f + (playerHP_level * upgradePotency_HPReg) + playerSts.Heart_heal();

        savedDMG = 21 + (playerDMG_level * upgradePotency_DMG);
        savedCritDMG = 2 + (playerDMG_level * upgradePotency_CritDMG);
        
        savedENERGY = 21 + (playerENERGY_level * upgradePotency_ENERGY);
        savedENERGYReg = 5f + (playerENERGY_level * upgradePotency_ENERGYReg) + playerSts.Heart_heal(); 
    }

    void GUI_sts()
    {
        hp_txt.text = playerSts.playerHP.ToString("N1");
        dmg_txt.text = playerSts.playerDMG.ToString("N1");
        energy_txt.text = playerSts.playerENERGY.ToString("N1");

        statPoint_txt.text = "Points left to spend:\n" + statPoints;
    }



}
