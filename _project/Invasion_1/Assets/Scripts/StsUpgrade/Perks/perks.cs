using UnityEngine;
using System.Collections;

public class perks : MonoBehaviour {

    [Header("Magic Find")]
    public static float magicFind_level;
    [SerializeField] private TextMesh magicFind_txt;
    private float magicFind_calculatedOutcome;


    [Header("Crit Chance")]
    public static float critChance_level;
    [SerializeField] private TextMesh critChance_txt;
    private float critChance_calculatedOutcome;



    void Update()
    {
        magicFind_txt.text = "Magic Find: " + MagicFind().ToString("N1") + "%";
        critChance_txt.text = "Crit Chance: " + CritChance().ToString("N1") + "%";


    }


    public float MagicFind()
    {
        magicFind_calculatedOutcome = 5 + (magicFind_level * 0.1f);
        return magicFind_calculatedOutcome;
    }
    

    public float CritChance()
    {
        critChance_calculatedOutcome = 10f + (critChance_level * 0.2f);
        return critChance_calculatedOutcome;
    }

}
