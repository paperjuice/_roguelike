using UnityEngine;
using System.Collections;

public class activateStatsAndPerkButton : MonoBehaviour {
    
	[SerializeField] private GameObject perksButton;
    private Animator anim;
    private bool isOpened;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        ActivatePanel();

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

    void ActivatePanel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isOpened)
            {
                isOpened = false;
                anim.SetBool("activate", true);
            }
            else
            {
                isOpened = true;
                anim.SetBool("activate", false);
            }
        }
        
    }
}
