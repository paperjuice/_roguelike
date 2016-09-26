using UnityEngine;
using System.Collections;

public class perks : MonoBehaviour {




    [Header("Magic Find")]
    [SerializeField] private TextMesh magicFind_txt;
    [SerializeField] private TextMesh magicFindLevel_txt;
    public static float magicFind_level=0f;
    private float magicFind_calculatedOutcome;


    [Header("Crit Chance")]
    [SerializeField] private TextMesh critChance_txt;
    [SerializeField] private TextMesh critChanceLevel_txt;
    public static float critChance_level=0f;
    private float critChance_calculatedOutcome;


    [Header("Health Regeneration")]
    [SerializeField] private TextMesh healthRegenLevel_txt;
    public static float healthRegen_level=0f;
    private float healthRegen_calculatedOutcome;


    [Header("hpRegPerMaxHp")]
    [SerializeField] private TextMesh hpRegPerMaxHpLevel_txt;
    public static float hpRegPerMaxHp_level=0f;
    private float hpRegPerMaxHp_calculatedOutcome;


    [Header("energyRegen")]
    [SerializeField] private TextMesh energyRegenLevel_txt;
    public static float energyRegen_level = 0f;
    private float energyRegen_calculatedOutcome;


    [Header("energyRegenPerMaxEnergy")]
    [SerializeField] private TextMesh energyRegenPerMaxEnergyLevel_txt;
    public static float energyRegenPerMaxEnergy_level = 0f;
    private float energyRegenPerMaxEnergy_calculatedOutcome;


    [Header("armour")]
    [SerializeField] private TextMesh armourLevel_txt;
    [SerializeField] private TextMesh armour_txt;
    public static float armour_level = 0f;
    private float armour_calculatedOutcome;



    void Update()
    {
        //magicFind
        magicFind_txt.text = "Magic Find: " + MagicFind().ToString("N1") + "%";
        magicFindLevel_txt.text = "X" + magicFind_level.ToString();

        //critChance
        critChance_txt.text = "Crit Chance: " + CritChance().ToString("N1") + "%";
        critChanceLevel_txt.text = "X" + critChance_level.ToString();

        //healthRegen
        healthRegenLevel_txt.text = "X" + healthRegen_level.ToString();

        //hpRegPerMaxLevel
        hpRegPerMaxHpLevel_txt.text = "X" + hpRegPerMaxHp_level.ToString();

        //energyRegen
        energyRegenLevel_txt.text = "X" + energyRegen_level.ToString();

        //energyRegenPerMaxEnergy
        energyRegenPerMaxEnergyLevel_txt.text = "X" + energyRegenPerMaxEnergy_level.ToString();

        //armour
        armourLevel_txt.text = "X" + armour_level.ToString();
        armour_txt.text = "Damage Reduction: " + (Armour() * 100).ToString("N0") + "%";
    }


    public float MagicFind()
    {
        magicFind_calculatedOutcome = 5f + (magicFind_level * 0.1f)+controller.dungeonLevel / 5f;
        return magicFind_calculatedOutcome;
    }
    

    public float CritChance()
    {
        critChance_calculatedOutcome = 5f + (critChance_level * 1f);
        return critChance_calculatedOutcome;
    }


    public float HealthRegeneration()
    {
        healthRegen_calculatedOutcome = healthRegen_level * 0.1f;
        return healthRegen_calculatedOutcome; 
    }


    public float HpRegenPerMaxHp(float maxPlayerHp)
    {
        hpRegPerMaxHp_calculatedOutcome = hpRegPerMaxHp_level * (0.2f * maxPlayerHp);
        return healthRegen_calculatedOutcome;
    }


    public float EnergyRegen()
    {
        energyRegen_calculatedOutcome = energyRegen_level * 0.3f;
        return energyRegen_calculatedOutcome;
    }


    public float EnergyRegenPerMaxEnergy(float maxPlayerEnergy)
    {
        energyRegenPerMaxEnergy_calculatedOutcome = energyRegenPerMaxEnergy_level * (0.01f * maxPlayerEnergy);
        return energyRegenPerMaxEnergy_calculatedOutcome;
    }


    public float Armour()
    {
        armour_calculatedOutcome = (1+armour_level) / (20 + armour_level);
        return armour_calculatedOutcome;
    }

















}
