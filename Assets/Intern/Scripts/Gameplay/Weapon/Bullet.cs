using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// An abstract bullet spawned by weapon
/// </summary>
public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float speed;
	[SerializeField]
	private float timeout;
	[SerializeField]
	protected float damage;

	[SerializeField]
	private UnityEvent on_apply = new UnityEvent();
	public UnityEvent OnApply { get { return on_apply; } }

	private Player owner;
	private bool applyed;
	private float start_date;

	/// <summary>
	/// Bind creation config
	/// </summary>
	/// <param name="owner"></param>
	/// <param name="direction"></param>
	public virtual void Bind( Player owner , Vector3 direction )
	{
		transform.rotation = Quaternion.LookRotation( direction );
		start_date = Time.time;
		this.owner = owner;
	}

	/// <summary>
	/// Called by player on receive the bullet
	/// </summary>
	/// <param name="player"></param>
	public void Receive( Player player )
	{
		if ( 
			!applyed
			&& player != owner
			&& apply_damage( player )
		)
		{
			applyed = true;
			on_apply.Invoke();
			Destroy( gameObject );
		}
	}

	/// <summary>
	/// Apply damage to target
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	protected virtual bool apply_damage( Player player )
	{
		player.ReceiveDamage( damage , owner );
		return true;
	}

	/// <summary>
	/// Handle move logic
	/// </summary>
	private void Update()
	{
		if ( !applyed )
		{
			// Destroy after timeout
			if ( start_date < Time.time - timeout )
			{
				applyed = true;
				Destroy( gameObject );
				return;
			}

			transform.position += transform.forward * speed * Time.deltaTime;
		}
	}
}
