using UnityEngine;
using System.Collections;

public class autogunGeneralBehaviour : MonoBehaviour {

    private generalWeaponBehaviour generalWeaponBehaviour;
    private playerStats _playerStats;
    private Animator _camera;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject bullet;


    [SerializeField]
    private float cooldown;
    private float currentCooldown;
    private bool isOnCooldown = false;
    
    private float energyCost;




    void Awake()
    {
        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>();
        generalWeaponBehaviour = GetComponent<generalWeaponBehaviour>();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        energyCost = generalWeaponBehaviour.weapon_energyCost;
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
        if (!isOnCooldown && _playerStats.player_currentEnergy > energyCost)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                anim.SetTrigger("attack");
                _playerStats.player_currentEnergy -= energyCost;
                print(energyCost.ToString());
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
