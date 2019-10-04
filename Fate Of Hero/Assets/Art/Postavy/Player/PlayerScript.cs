
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
	public static PlayerScript Instance { get; private set; }
	public bool CanSwapEquipment { get; set; }
	public Animator Anim { get { return anim; } }
	public int XP, Gold, Silver, Copper;
	private bool isInCombat;


	//
	float range = 1.8f;
	float attackInterval = 0.7f;
	float meleeDamage = 30;
	private float nextAttack = 0;
	[SerializeField]
	private float attackAngle = 45;
	//
	[SerializeField]
	private int maxAttackValue;
	[SerializeField]
	private Transform rh, lh;
	protected override void Awake()
	{
		Instance = FindObjectOfType<PlayerScript>();
		CanSwapEquipment = true;
		base.Awake();
	}
	public override void Attack()
	{
		if (isInCombat||!CharacterPanel.Instance.WeaponSlot.Filled) return;
		DisableAgent();
		isInCombat = true;
		SetAnimatorBool("isInCombat", true);
		SetAnimatorTrigger("drawSword");
		SetAnimatorBool("equipedSword", true);
		RestoreAgent();
	}

	public void SetOutOfCombat()
	{

		if (!isInCombat||!CharacterPanel.Instance.WeaponSlot.Filled) return;
		DisableAgent();
		isInCombat = false;
		SetAnimatorBool("isInCombat", false);
		SetAnimatorTrigger("hideSword");
		SetAnimatorBool("equipedSword", false);
		RestoreAgent();
	}
	public void RollJump()
	{
		DisableAgent();
		bool winc = isInCombat;
		SetOutOfCombat();
		SetAnimatorTrigger("rollJump");
		isInCombat = winc;

	}
	public void Gathering()
	{
		bool winc = isInCombat;
		DisableAgent();
		SetOutOfCombat();
		SetAnimatorTrigger("gathering");
		isInCombat = winc;

	}

	public override void Defend()
	{
	}
	protected override void Update()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			RollJump();

		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			Gathering();

		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			Attack();
		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			SetOutOfCombat();
		}


			if (Input.GetKeyDown(KeyCode.Alpha0))
			{
				Attacking(0);
			}
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				Attacking(1);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				Attacking(2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				Attacking(3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				Attacking(4);
			}

		


		if (AgentAvailable)
		{

			if (AgentIsOnPosition)
			{

				Idle();
			}
			else
			{
				if ((stats.TargetVector.Target != null || stats.TargetVector.Destination != Vector3.zero))
				{

					if (Input.GetKey(KeyCode.LeftShift))
						Run();
					else
						Walk();

				}

			}

			base.Update();
		}
	}


	void DrawParentAndResetPose()
	{
		Agent.Warp(transform.position);
		RestoreAgent();
		if (isInCombat)
		{
			Attack();
		}
	}


	void SetAnimatorTrigger(string trigger)
	{
		anim.SetTrigger(trigger);

	}
	void SetAnimatorBool(string val, bool b)
	{
		anim.SetBool(val, b);
	}
	void SetAnimatorInt(string val, int num)
	{
		anim.SetInteger(val, num);
	}
	public void Attacking(int attack)
	{
		if (!CharacterPanel.Instance.WeaponSlot.Filled) return;
		if (!isInCombat)
		{
			Attack();
		}
		SetAnimatorInt("attackIndex", attack);
		MeleeAttack();

	}
	void ResetAnimatorAttack()
	{
		SetAnimatorInt("attackIndex", maxAttackValue + 1);
	}
	void MeleeAttack()
	{
		if (Time.time > nextAttack)
		{ 
			nextAttack = Time.time + attackInterval;
			
			Collider[] colls = Physics.OverlapSphere(transform.position, range);
			foreach (Collider hit in colls)
			{
				if (hit && hit.tag == "Enemy")
				{
					float angle = Vector3.Angle(transform.position, hit.transform.position);
					//within angle?
				
					var dist = Vector3.Distance(hit.transform.position, transform.position);
					if (dist <= range&& angle <= attackAngle)
					{ 
						hit.SendMessage("ApplyDamage", meleeDamage);
					}
				}
			}
		}
	}
	public void DrawSword()
	{
		if (CharacterPanel.Instance.WeaponSlot.Filled)
		{
			Weapon w = CharacterPanel.Instance.WeaponSlot.CurrentItem as Weapon;
			w.DrawWeapon(CharacterPanel.Instance.WeaponSlot.FyzicPlacementInNotUse.gameObject, CharacterPanel.Instance.WeaponSlot.FyzicPlacementInUse.gameObject);
		}
	}
	public void HideSword()
	{
		if (CharacterPanel.Instance.WeaponSlot.Filled)
		{
			Weapon w = CharacterPanel.Instance.WeaponSlot.CurrentItem as Weapon;
			w.DrawWeapon(CharacterPanel.Instance.WeaponSlot.FyzicPlacementInUse.gameObject, CharacterPanel.Instance.WeaponSlot.FyzicPlacementInNotUse.gameObject);
		}
	}
}
