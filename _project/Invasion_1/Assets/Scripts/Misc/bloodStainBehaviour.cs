using UnityEngine;
using System.Collections;

public class bloodStainBehaviour : MonoBehaviour {

    private GameObject[] bloodStains;


    IEnumerator Start()
    {
        while (true)
        {
            bloodStains = GameObject.FindGameObjectsWithTag("bloodStain");
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        foreach (GameObject bloodStain in bloodStains)
        {
            if (bloodStain != null)
            {
                if (bloodStain.transform.localScale.y <  1.5f)
                {
                    bloodStain.transform.localScale += Time.deltaTime * new Vector3(1, 1, 1) * 0.1f;
                }
            }
        }
    }
}
