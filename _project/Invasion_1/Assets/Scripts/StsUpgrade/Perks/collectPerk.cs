using UnityEngine;
using System.Collections;

public class collectPerk : MonoBehaviour {

    private GameObject[] perksCollectibles;
    [SerializeField]private perksController _perkController;


    IEnumerator Start()
    {
        while (true)
        {
            perksCollectibles = GameObject.FindGameObjectsWithTag("perk");
            yield return new WaitForSeconds(1f);
        }
    }

    void Update()
    {
        foreach (GameObject perk in perksCollectibles)
        {
            if (perk != null)
            {
                if (Vector3.Distance(transform.position, perk.transform.position) < 2)
                {
                    _perkController.isActivating = true;
                    Destroy(perk.gameObject);
                }
            }
        }
    }
}
