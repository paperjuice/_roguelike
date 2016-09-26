using UnityEngine;
using System.Collections;

public class activateStatsAndPerkButton : MonoBehaviour {
    
	[SerializeField] private GameObject statsPanel;
	[SerializeField] private GameObject perksPanel;
    bool isOpened = false;


    void Update()
    {
        ActivatePanel();
    }

    void ActivatePanel()
    {
        if (!isOpened)
        {
            statsPanel.transform.localPosition = new Vector3(-5f, 1.36f, 0f);
            perksPanel.transform.localPosition = new Vector3(5f, 1.36f, 0f);
        }
        else
        {
            statsPanel.transform.localPosition = new Vector3(-1.53f, 1.36f, 0f);
            perksPanel.transform.localPosition = new Vector3(1.61f, 1.276f, 0f);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isOpened)
            {
                isOpened = false;
            }
            else
            {
                isOpened = true;
            }
        }
    }
}
