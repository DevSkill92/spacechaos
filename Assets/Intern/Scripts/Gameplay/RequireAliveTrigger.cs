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
	private UnityEvent OnDie = new UnityEvent();
	[SerializeField]
	private UnityEvent OnDead = new UnityEvent();

	private bool alive = true;
	private bool dead = false;
	private float last_alive;
	private Vector3 fall_direction;
	private Vector3 fall_rotation;

	private void Start()
	{
		last_alive = Time.time + 2;
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
		)
		{
			last_alive = Time.time;
		}
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
				die();
			}
		}
		else if ( Time.time - die_timeout < last_alive )
		{
			die_transition();
		} 
		else if ( !dead ) {
			kill();
		}
	}

	/// <summary>
	/// Set player to die state
	/// </summary>
	private void die()
	{
		alive = false;
		OnDie.Invoke();
		fall_direction = new Vector3( fall_range , -2 , fall_range );
		fall_rotation = transform.rotation.eulerAngles;

		foreach ( Component component in disable_collection )
		{
			if ( component.gameObject == gameObject )
			{
				( component as MonoBehaviour ).enabled = false;
			}
			else
			{
				component.gameObject.SetActive( false );
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
	/// Handle die transition
	/// </summary>
	private void die_transition()
	{
		transform.position += fall_direction * Time.deltaTime;
		fall_rotation += fall_direction * 10 * Time.deltaTime;

		transform.eulerAngles = fall_rotation;
	}
}