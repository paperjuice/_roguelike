using UnityEngine;
using System.Collections;

public class cameraFollowPlayer : MonoBehaviour {


    [SerializeField]
    private float y;
    [SerializeField]
    private float z;

    private GameObject player;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + y, player.transform.position.z + z);
        }
    }
}
