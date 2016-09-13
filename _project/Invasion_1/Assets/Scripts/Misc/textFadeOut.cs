using UnityEngine;
using System.Collections;

public class textFadeOut : MonoBehaviour {

	
    [SerializeField] float ms;

    private bool isFading;
    private TextMesh textMesh;
    private float a=1;


    void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        isFading = true;
    }

    void Update()
    {
        FadeOut();
        transform.position += Vector3.up * Time.deltaTime * ms;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, a);
    }

    void FadeOut()
    {
        if (isFading)
        {
            a -= Time.deltaTime;
        }

        if (a <= 0)
        {
            Destroy(gameObject);
        }
    }




}
