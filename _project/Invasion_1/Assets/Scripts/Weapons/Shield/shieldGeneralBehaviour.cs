using UnityEngine;
using System.Collections;

public class shieldGeneralBehaviour : MonoBehaviour {

    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerSts playerSts;
    private Animator _camera;

    [SerializeField]private Animator anim; 
    [SerializeField] private TextMesh DmgText;

    [SerializeField] private float cooldown;
    private float currentCooldown;
    private bool isOnCooldown = false;

    [SerializeField] private float energyCost;


    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "enemy")
        {
            col.gameObject.GetComponent<enemySts>().enemyHP -= playerSts.CalculatedDmg();
            _camera.SetTrigger("shake");

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = playerSts.CalculatedDmg().ToString("N1");
            Instantiate(DmgText, col.gameObject.transform.position, DmgText.transform.rotation);
        }

        if (col.gameObject.tag == "prop")
        {
            col.gameObject.GetComponent<propBehaviour>().propIsHit = true;
            _camera.SetTrigger("shake");
        }
    }

    void Attack()
    {
        if (!isOnCooldown && playerSts.currentPlayerENERGY>energyCost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                playerSts.currentPlayerENERGY -= energyCost;
                isOnCooldown = true;
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
