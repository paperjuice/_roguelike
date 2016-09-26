using UnityEngine;
using System.Collections;

public class staffGeneralBehaviour : MonoBehaviour {

	private generalWeaponBehaviour generalWeaponBehaviour;
	private playerStats _playerStats;
	[SerializeField] private Animator anim;


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

    void Attack()
	{
		if (!isOnCooldown && _playerStats.player_currentEnergy > energyCost)
		{
			if (Input.GetKey(KeyCode.Mouse0))
			{
				anim.SetTrigger("attack");
                _playerStats.player_currentEnergy -= energyCost;
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
