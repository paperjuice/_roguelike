using UnityEngine;
using System.Collections;

public class activateObjectOnClick : MonoBehaviour {
    [SerializeField] private Color onHoverColor;
    [SerializeField] private GameObject panelToOpen;
	[SerializeField] private string _tag;

    private GameObject[] panels;
    private bool isOpen;
    private Color startingColor;
    private SpriteRenderer sprite;
    private Vector3 initialScale;


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        startingColor = sprite.color;
        initialScale = transform.localScale;
    }

    void OnMouseEnter()
    {
        sprite.color = onHoverColor;
    }

    void OnMouseExit()
    {
        sprite.color = startingColor;
    }

    void OnMouseDown()
    {
        if (_tag != null)
        {
            panels = GameObject.FindGameObjectsWithTag("panel");
        }

        foreach (GameObject panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        if (panelToOpen.gameObject.activeInHierarchy)
        {
            panelToOpen.gameObject.SetActive(false);
           // Time.timeScale = 1f;
        }
        else
        {
            panelToOpen.gameObject.SetActive(true);
           // Time.timeScale = 0f;
        }

        transform.localScale -= new Vector3(0.002f, 0.002f, 0.002f);
    }


    void OnMouseUp()
    {
        transform.localScale = initialScale;
    }








}
