using UnityEngine;
using System.Collections;

public class autogunGeneralBehaviour : MonoBehaviour {

    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerSts playerSts;
    private Animator _camera;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bullet;


    [SerializeField]
    private float cooldown;
    private float currentCooldown;
    private bool isOnCooldown = false;

    [SerializeField]
    private float energyCost;




    void Awake()
    {
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        generalWeaponBehaviour = GetComponent<generalWeaponBehaviour>();
    }


    void Update()
    {
        if (generalWeaponBehaviour.isEquipped)
        {
            Attack();
        }
    }

    
    void Attack()
    {
        if (!isOnCooldown && playerSts.currentPlayerENERGY > energyCost)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                playerSts.currentPlayerENERGY -= energyCost;
                isOnCooldown = true;
                Instantiate(bullet, anim.transform.position += transform.forward *1.1f, anim.transform.rotation);
            }
        }

        if (isOnCooldown)
        {
            currentCooldown += Time.deltaTime;

            if (currentCooldown > cooldown)
            {
                isOnCooldown = false;
                currentCooldown = 0f;
            }
        }
    }
}
