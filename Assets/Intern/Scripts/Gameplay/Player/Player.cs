﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Handle player game logic
/// </summary>
public class Player : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer body;
	[SerializeField]
	private Material[] skin;
	[SerializeField]
	private Material[] skin_leader;

	[SerializeField]
	private SimpleCar car;
	[SerializeField]
	private CaptureLine capture_line;
	[SerializeField]
	private RequireAliveTrigger alive;
	[SerializeField]
	private AliveTriggerCreator alive_creator;
	[SerializeField]
	private Track track;
	[SerializeField]
	private GameObject leader_trigger = null;
	[SerializeField]
	private GameObject alive_trigger = null;
	[SerializeField]
	private Color[] color;
	[SerializeField]
	private Transform weapon_container;

	[SerializeField]
	private UnityEvent on_follow = new UnityEvent();
	public UnityEvent OnFollow { get { return on_follow; } }
	[SerializeField]
	private UnityEvent on_lead = new UnityEvent();
	public UnityEvent OnLead { get { return on_lead; } }
	[SerializeField]
	private UnityEvent on_attach_weapon = new UnityEvent();
	public UnityEvent OnAttachWeapon { get { return on_attach_weapon; } }
	[SerializeField]
	private UnityEvent on_enable = new UnityEvent();
	public UnityEvent OnEnable { get { return on_enable; } }
	[SerializeField]
	private UnityEvent on_disable = new UnityEvent();
	public UnityEvent OnDisable { get { return on_disable; } }

	private int joy;
	private bool enabled;
	private int index;
	private int kill_count;
	private float health;
	private float last_planet_switch;
	private Collider last_planet;
	private Weapon weapon;

	/// <summary>
	/// Gets the current weapon
	/// </summary>
	public virtual Weapon Weapon
	{
		get
		{
			return weapon;
		}
	}

	/// <summary>
	/// Gets that the player is alive
	/// </summary>
	public bool Alive
	{
		get
		{
			return alive.Alive;
		}
	}

	/// <summary>
	/// Gets the amount of kills on another player
	/// </summary>
	public int KillCount
	{
		get
		{
			return kill_count;
		}
	}

	/// <summary>
	/// Gets the index of the player
	/// </summary>
	public int Index
	{
		get
		{
			return index;
		}
	}

	/// <summary>
	/// Gets the color of the player
	/// </summary>
	public Color Color
	{
		get
		{
			return color[ index ];
		}
	}

	/// <summary>
	/// Gets the player is currently capturing
	/// </summary>
	public bool Capturing
	{
		get
		{
			return capture_line.gameObject.activeInHierarchy;
		}
	}


	/// <summary>
	/// Bind user index
	/// </summary>
	/// <param name="joy"></param>
	/// <param name="index"></param>
	public void BindIndex( int joy , int index )
	{
		this.joy = joy;
		this.index = index;
	}

	/// <summary>
	/// Initialize user skin
	/// </summary>
	private void Start()
	{
		Follower();
		car.BindJoyindex( joy );
		alive_creator.enabled = false;
	}

	/// <summary>
	/// Disable the player ( user input )
	/// </summary>
	public void Disable()
	{
		enabled = false;
		car.enabled = enabled;
		on_disable.Invoke();
	}

	/// <summary>
	/// Enable the player ( user input )
	/// </summary>
	public void Enable()
	{
		enabled = true;
		car.enabled = enabled;
		on_enable.Invoke();
	}

	/// <summary>
	/// Gets that the player is enabled
	/// </summary>
	public bool Enabled
	{
		get
		{
			return enabled;
		}
	}


	/// <summary>
	/// Switch to loader states
	/// </summary>
	public void Leader()
	{
		on_lead.Invoke();
		body.material = skin_leader[ index ];

		if ( !Root.I.Get<GameModeManager>().AITrack )
		{
			track.Enable();
		}

		alive_creator.enabled = true;
		leader_trigger.gameObject.SetActive( true );
		alive_trigger.gameObject.SetActive( true );
		car.SetPower( 37 , 15 , 95 , 0 );
	}

	/// <summary>
	/// Switch to follower state
	/// </summary>
	public void Follower( bool enable_speedbreaker = false )
	{
		body.material = skin[ index ];
		on_follow.Invoke();
		track.Disable();
		alive_creator.enabled = false;
		car.SetPower( 40 , 0 , 130 , enable_speedbreaker ? 200 : 0 );
		alive.ForceAlive();
		leader_trigger.gameObject.SetActive( false );
		alive_trigger.gameObject.SetActive( false );
	}

	/// <summary>
	/// Keep last trigger time
	/// </summary>
	/// <param name="collider"></param>
	private void OnTriggerStay( Collider collider )
	{
		if ( !Alive )
		{
			return;
		}

		Game game = Root.I.Get<ScreenManager>().Get<Game>();
		GameModeManager mode = Root.I.Get<GameModeManager>();

		// Receive bullet
		Bullet bullet = collider.GetComponent<Bullet>();
		if ( null != bullet )
		{
			bullet.Receive( this );
			return;
		}

		// Leader change
		if ( mode.AllowLeader )
		{
			if (
				collider.gameObject.layer == LayerMask.NameToLayer( "trigger_leader" )
				&& collider.gameObject != leader_trigger
			)
			{
				if (
					null == last_planet
					|| collider != last_planet
					|| last_planet_switch < Time.time - 1
				)
				{
					last_planet_switch = Time.time;
					last_planet = collider;
					Root.I.Get<PlayerManager>().RequestLeaderSwitch( this );
				}

				return;
			}
		}

		// Capture planets
		if ( mode.AllowCapture )
		{
			Planet planet = collider.GetComponent<Planet>();
			if (
				null != planet
				&& planet.Capture( this )
			)
			{
				capture_line.Show( planet );
			}
		}
	}

	private void Update()
	{
		if (
			null != weapon
			&& Input.GetButton( "Shoot" + joy )
			&& alive.Alive
			&& enabled
		)
		{
			weapon.Shoot();
		}
	}

	/// <summary>
	/// Attach the given weapon
	/// </summary>
	/// <param name="prefab"></param>
	public void AttachWeapon( Weapon prefab )
	{
		foreach( Transform child in weapon_container )
		{
			Destroy( child.gameObject );
		}

		GameObject obj = Instantiate( prefab.gameObject );
		Weapon weapon = obj.GetComponent<Weapon>();
		weapon.Bind( this );
		weapon.transform.SetParent( weapon_container , true );
		weapon.transform.localPosition = Vector3.forward;
		weapon.transform.rotation = Quaternion.LookRotation( transform.forward );

		on_attach_weapon.Invoke();

		this.weapon = weapon;
	}

	/// <summary>
	/// Receive damage from another player
	/// </summary>
	/// <returns></returns>
	public void ReceiveDamage( float damage , Player opponent )
	{
		if ( 
			!alive.Alive
			|| this == opponent
		)
		{
			return;
		}

		health = Mathf.Max( 0 , health - damage );
		if ( 0 >= health )
		{
			alive.Die();
			opponent.kill_count++;
		}
	}
}