using UnityEngine;
using System.Collections;

public class activateStatsAndPerkButton : MonoBehaviour {

	[SerializeField] private GameObject statsButton;
	[SerializeField] private GameObject perksButton;





    void Update()
    {
        if (primarySts.statPoints > 0)
        {
           // statsButton.gameObject.SetActive(true);
            statsButton.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
           // statsButton.gameObject.SetActive(false);
            statsButton.GetComponent<SpriteRenderer>().color = Color.gray;
        }

        if (primarySts.perkPoints > 0)
        {
           // perksButton.gameObject.SetActive(true);
            perksButton.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
           // perksButton.gameObject.SetActive(false);
            perksButton.GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
