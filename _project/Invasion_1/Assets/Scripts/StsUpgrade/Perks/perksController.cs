using UnityEngine;
using System.Collections;

public class perksController : MonoBehaviour {

    private static float i = -0.3f;
    private static float j = 0.3f;
    private int randomPerk;

    private GameObject[] _perks;

    [SerializeField] private GameObject[] perkIcons;

    public bool isActivating;
    private int id;


    IEnumerator Start()
    {
        while (true)
        {
            _perks = GameObject.FindGameObjectsWithTag("perk");
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {
        CollectPerk();
    }

    void CollectPerk()
    {
        foreach (GameObject perk in _perks)
        {
            if (perk != null)
            {
                if (Vector3.Distance(transform.position, perk.transform.position) < 3)
                {
                    isActivating = true;
                    Destroy(perk.gameObject);
                    id = perk.GetComponent<perkId>()._perkId;
                }
            }
        }

        if (isActivating)
        {

            switch (id)
            {
                //magicFind
                case 11:
                    if (!perkIcons[0].gameObject.activeInHierarchy)
                    {
                        perks.magicFind_level++;
                        perkIcons[0].gameObject.SetActive(true);
                        perkIcons[0].transform.localPosition = new Vector3(i, j, 0);
                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }
                       
                    }
                    else
                    {
                        perks.magicFind_level++;
                    }
                    break;

                //critChance
                case 12:
                    if (!perkIcons[1].gameObject.activeInHierarchy)
                    {
                        perks.critChance_level++;
                        perkIcons[1].gameObject.SetActive(true);
                        perkIcons[1].transform.localPosition = new Vector3(i, j, 0);
                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }
                        
                    }
                    else
                    {
                        perks.critChance_level++;
                    }
                    break;

                //healthRegen
                case 16:
                    if (!perkIcons[2].gameObject.activeInHierarchy)
                    {
                        perks.healthRegen_level++;
                        perkIcons[2].gameObject.SetActive(true);
                        perkIcons[2].transform.localPosition = new Vector3(i, j, 0);

                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }
                        
                    }
                    else
                    {
                        perks.healthRegen_level++;
                    }
                    break;

                //hpRegenPerMaxHp
                case 17:
                    if (!perkIcons[3].gameObject.activeInHierarchy)
                    {
                        perks.hpRegPerMaxHp_level++;
                        perkIcons[3].gameObject.SetActive(true);
                        perkIcons[3].transform.localPosition = new Vector3(i, j, 0);

                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }
                        
                    }
                    else
                    {
                        perks.hpRegPerMaxHp_level++;
                    }
                    break;

                //energyRegen
                case 18:
                    if (!perkIcons[4].gameObject.activeInHierarchy)
                    {
                        perks.energyRegen_level++;
                        perkIcons[4].gameObject.SetActive(true);
                        perkIcons[4].transform.localPosition = new Vector3(i, j, 0);

                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }

                    }
                    else
                    {
                        perks.energyRegen_level++; 
                    }
                    break;

                //energyRegenPerMaxEnergy
                case 19:
                    if (!perkIcons[5].gameObject.activeInHierarchy)
                    {
                        perks.energyRegenPerMaxEnergy_level++;
                        perkIcons[5].gameObject.SetActive(true);
                        perkIcons[5].transform.localPosition = new Vector3(i, j, 0);

                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }

                    }
                    else
                    {
                        perks.energyRegenPerMaxEnergy_level++;
                    }
                    break;

                case 20:
                    if (!perkIcons[6].gameObject.activeInHierarchy)
                    {
                        perks.armour_level++;
                        perkIcons[6].gameObject.SetActive(true);
                        perkIcons[6].transform.localPosition = new Vector3(i, j, 0);

                        i += 0.3f;
                        if (i > 0.35)
                        {
                            j -= 0.3f;
                            i = -0.3f;
                        }

                    }
                    else
                    {
                        perks.armour_level++;
                    }
                    break;



            }

            isActivating = false;
        }
    }
    







}
