using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

public class RequireAliveTrigger : MonoBehaviour
{
	[SerializeField]
	private float timeout;
	[SerializeField]
	private float die_timeout;
	[SerializeField]
	private float fall_range = 2;
	[SerializeField]
	private Component[] disable_collection;
	[SerializeField]
	private float force_alive_factor = 1;
	[SerializeField]
	private Player player;
	[SerializeField]
	private GameObject die_effect;
	[SerializeField]
	private GameObject spawn_effect;
	[SerializeField]
	private float max_health = 10;

	[SerializeField]
	private UnityEvent on_die = new UnityEvent();
	public UnityEvent OnDie { get { return on_die; } }
	[ SerializeField]
	private UnityEvent on_dead = new UnityEvent();
	public UnityEvent OnDead { get { return on_dead; } }
	[SerializeField]
	private UnityEvent on_respawn = new UnityEvent();
	public UnityEvent OnRespawn { get { return on_respawn; } }

	private bool alive = true;
	private bool dead = false;
	public float health;
	private float last_alive;
	private Vector3 fall_direction;
	private Vector3 fall_rotation;

	public bool Alive
	{
		get
		{
			return alive;
		}
	}

	private void Start()
	{
		respawn();
	}

	/// <summary>
	/// Keep last trigger time
	/// </summary>
	/// <param name="collider"></param>
	public void OnTriggerStay( Collider collider )
	{
		if (
			alive
			&& collider.gameObject.layer == LayerMask.NameToLayer( "trigger_alive" )
			&& last_alive < Time.time
		)
		{
			last_alive = Time.time;
		}
	}

	/// <summary>
	/// Force the alive state for given time
	/// </summary>
	public void ForceAlive()
	{
		last_alive = Time.time + force_alive_factor;
	}

	/// <summary>
	/// Update player state
	/// </summary>
	private void Update()
	{
		if ( alive )
		{
			if ( Time.time - timeout > last_alive )
			{
				Die();
			}
		}
		else if ( Time.time - die_timeout < last_alive )
		{
			die_transition();
		} 
		else if ( !dead ) {
			if ( Root.I.Get<GameModeManager>().AllowRespawn )
			{
				respawn();
			}
			else
			{
				kill();
			}
		}
	}

	/// <summary>
	/// Set player to die state
	/// </summary>
	public void Die()
	{
		alive = false;
		OnDie.Invoke();
		fall_direction = new Vector3( fall_range , -20 , fall_range );
		fall_rotation = transform.rotation.eulerAngles;
		set_component_status( false );

		player.AttachWeapon();
		player.ShowEffect( die_effect );
	}

	/// <summary>
	/// Sets the active / enabled of disable_collection
	/// </summary>
	/// <param name="enabled"></param>
	private void set_component_status( bool enabled )
	{
		foreach ( Component component in disable_collection )
		{
			if ( component.gameObject == gameObject )
			{
				( component as MonoBehaviour ).enabled = enabled;
			}
			else
			{
				component.gameObject.SetActive( enabled );
			}
		}
	}

	/// <summary>
	/// Set player to dead state
	/// </summary>
	private void kill()
	{
		dead = true;
		OnDead.Invoke();
		Destroy( gameObject );
	}

	/// <summary>
	/// Respawns the player
	/// </summary>
	private void respawn()
	{
		last_alive = Time.time + 1;
		alive = true;
		health = max_health;

		if ( !player.Enabled )
		{
			return;
		}

		if ( !Root.I.Get<GameModeManager>().AllowItem )
		{
			Planet last_planet = Root.I.Get<PlanetManager>().LastPlayerPlanet( player );
			if ( null != last_planet )
			{
				last_planet.Owner = null;
			}
		}

		if ( !Root.I.Get<TrackCreator>().SetRespawnTransform( transform ) )
		{
			transform.position = Vector3.zero;
			transform.LookAt( Vector3.forward );
		}

		set_component_status( true );
		player.Follower();

		if  (
			Root.I.Get<GameModeManager>().AllowLeader
			&& null == Root.I.Get<PlayerManager>().Leader
		)
		{
			Root.I.Get<PlayerManager>().RequestLeaderSwitch( player );
		}

		player.ShowEffect( spawn_effect );
		on_respawn.Invoke();
	}

	/// <summary>
	/// Handle die transition
	/// </summary>
	private void die_transition()
	{
		transform.position += fall_direction * Time.deltaTime;
		fall_rotation += fall_direction * 10 * Time.deltaTime;

		transform.eulerAngles = fall_rotation;
	}

	/// <summary>
	/// Receive damage from another player
	/// </summary>
	/// <returns>Kill</returns>
	public bool ReceiveDamage( float damage )
	{
		if ( !Alive )
		{
			return false;
		}

		health = Mathf.Max( 0 , health - damage );
		if ( 0 >= health )
		{
			return true;
		}

		return false;
	}

	/// <summary>
	/// Give health to player
	/// </summary>
	/// <param name="health"></param>
	public void GiveHealth( float health )
	{
		this.health = Mathf.Max( this.health + health , max_health );
	}
}