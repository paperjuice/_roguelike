using UnityEngine;
using System.Collections;

public class collectKey : MonoBehaviour {


    [SerializeField] private TextMesh text;
    [SerializeField] private GameObject keyPng;
    public static int keys = 0;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "key")
        {
            keys++;
            Destroy(col.gameObject);
        }
    }

    void Update()
    {
        text.text = "Keys: " + keys;

        if (keys > 0)
        {
            keyPng.gameObject.SetActive(true);
        }
    }

	
}
