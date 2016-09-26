using UnityEngine;
using System.Collections;

public class shieldGeneralBehaviour : MonoBehaviour {

    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerStats _playerStats;
    private Animator _camera;

    [SerializeField]private Animator anim; 
    [SerializeField] private TextMesh DmgText;

    [SerializeField] private float cooldown;
    private float currentCooldown;
    private bool isOnCooldown = false;

    private float energyCost;
    private float dmg;


    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        generalWeaponBehaviour = GetComponent<generalWeaponBehaviour>();
    }

    void Start()
    {
        energyCost = generalWeaponBehaviour.weapon_energyCost;
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
            col.gameObject.GetComponent<enemySts>().enemyHP -= dmg;
            _camera.SetTrigger("shake");

            col.gameObject.GetComponent<enemySts>().isHit = true;

            DmgText.text = dmg.ToString("N1");
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
        if (!isOnCooldown && _playerStats.player_currentEnergy>energyCost)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                _playerStats.player_currentEnergy -= energyCost;
                isOnCooldown = true;
                dmg = _playerStats.CalculatedDmg();
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
