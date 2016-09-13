using UnityEngine;
using System.Collections;

public class lifetime : MonoBehaviour {

	[SerializeField] private float _lifetime;

    void Start()
    {
        Destroy(gameObject, _lifetime);
    }
}
