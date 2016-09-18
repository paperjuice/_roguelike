using UnityEngine;
using System.Collections;

public class perksController : MonoBehaviour {

    private float i = -0.4f;
    private float j = 0.3f;
    private int randomPerk;
    [SerializeField] private GameObject[] perkIcons;

    public static float perkPoints;

    public bool isActivating;



    void Update()
    {
        if (isActivating)
        {
            randomPerk = Random.Range(0, perkIcons.Length);

            if (perkIcons[randomPerk].gameObject.activeInHierarchy)
            {
                perkPoints++;
            }
            else
            {
                perkIcons[randomPerk].gameObject.SetActive(true);
                perkIcons[randomPerk].transform.localPosition = new Vector3(i, j, 0);
                i += 0.25f;
                if (i > 0.35f)
                {
                    j -= 0.2f;
                    i = -0.4f;
                }
            }


            isActivating = false;
        }
    }
    







}
