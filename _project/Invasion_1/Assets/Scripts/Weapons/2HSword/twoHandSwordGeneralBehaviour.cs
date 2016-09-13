using UnityEngine;
using System.Collections;

public class twoHandSwordGeneralBehaviour : MonoBehaviour {

    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerSts playerSts;
    private int alternativeAttack =1;
    private Animator _camera;

    [SerializeField] private Animator anim;
    [SerializeField] private TextMesh DmgText;


    [SerializeField] private float cooldown;
    private float currentCooldown;
    private bool isOnCooldown = false;

    [SerializeField] private float energyCost;


    void Awake()
    {
        playerSts = GameObject.FindGameObjectWithTag("Player").GetComponent<playerSts>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
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
            col.gameObject.GetComponent<enemySts>().enemyHP -= playerSts.playerDMG;
            _camera.SetTrigger("shake");

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = playerSts.playerDMG.ToString("N1");
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
        if (!isOnCooldown && playerSts.currentPlayerENERGY > energyCost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (alternativeAttack == 1)
                {
                    anim.SetTrigger("attack_1");
                    alternativeAttack = 2;
                }
                else if (alternativeAttack == 2)
                {
                    anim.SetTrigger("attack_2");
                    alternativeAttack = 1;
                }
                
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
