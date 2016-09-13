using UnityEngine;
using System.Collections;

public class upgradePrimaryStsButtonBehaviour : MonoBehaviour {

    [SerializeField] GameObject button;
	[SerializeField][Range(1,3)] int _case;

    private Vector3 buttonLocalScale;


    void Start()
    {
        buttonLocalScale = button.transform.localScale;
    }

    void OnMouseDown()
    {
        button.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);

        if (primarySts.statPoints > 0)
        {
            switch (_case)
            {
                case 1:
                    primarySts.playerHP_level++;
                    primarySts.statPoints--;
                    break;
                case 2:
                    primarySts.playerDMG_level++;
                    primarySts.statPoints--;
                    break;
                case 3:
                    primarySts.playerENERGY_level++;
                    primarySts.statPoints--;
                    break;
            }
        }
    }

    void OnMouseUp()
    {
        button.transform.localScale = buttonLocalScale;
    }
}
