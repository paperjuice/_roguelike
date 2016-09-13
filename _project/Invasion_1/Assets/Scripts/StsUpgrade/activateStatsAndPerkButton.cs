using UnityEngine;
using System.Collections;

public class activateStatsAndPerkButton : MonoBehaviour {

	[SerializeField] private GameObject statsButton;
	[SerializeField] private GameObject perksButton;





    void Update()
    {
        if (primarySts.statPoints > 0)
        {
            statsButton.gameObject.SetActive(true);
        }
        else
        {
            statsButton.gameObject.SetActive(false);
        }

        if (primarySts.perkPoints > 0)
        {
            perksButton.gameObject.SetActive(true);
        }
        else
        {
            perksButton.gameObject.SetActive(false);
        }
    }
}
