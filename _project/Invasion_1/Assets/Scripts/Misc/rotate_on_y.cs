using UnityEngine;
using System.Collections;

public class rotate_on_y : MonoBehaviour {

	
	[SerializeField] private float rs = 20;

    void Update()
    {
        transform.Rotate(Vector3.up * rs * Time.deltaTime);
    }






}
