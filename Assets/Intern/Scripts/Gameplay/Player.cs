using System;
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
	private UnityEvent on_follow = new UnityEvent();
	[SerializeField]
	private UnityEvent on_lead = new UnityEvent();
	[SerializeField]
	private UnityEvent on_enable = new UnityEvent();
	[SerializeField]
	private UnityEvent on_disable = new UnityEvent();

	private int joy;
	private int index;
	private float last_planet_switch;
	private Collider last_planet;

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
		car.enabled = false;
		on_disable.Invoke();
	}

	/// <summary>
	/// Enable the player ( user input )
	/// </summary>
	public void Enable()
	{
		car.enabled = true;
		on_enable.Invoke();
	}


	/// <summary>
	/// Switch to loader states
	/// </summary>
	public void Leader()
	{
		on_lead.Invoke();
		body.material = skin_leader[ index ];
		track.gameObject.SetActive( true );
		alive_creator.enabled = true;
		leader_trigger.gameObject.SetActive( true );
		alive_trigger.gameObject.SetActive( true );
		car.SetPower( 36.6f , 15 , 50 , 0 );
	}

	/// <summary>
	/// Switch to follower state
	/// </summary>
	public void Follower()
	{
		body.material = skin[ index ];
		on_follow.Invoke();
		track.gameObject.SetActive( false );
		alive_creator.enabled = false;
		car.SetPower( 40 , 0 , 130 , 200 );
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
				game.RequestLeaderSwitch( this );
			}

			return;
		}

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