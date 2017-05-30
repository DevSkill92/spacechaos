using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
	[SerializeField]
	protected float accuracy;
	[SerializeField]
	protected int bullet_per_shot;
	[SerializeField]
	protected GameObject bullet_prefab;
	[SerializeField]
	protected float cooldown;
	[SerializeField]
	protected int max_ammunition;

	[SerializeField]
	private UnityEvent on_shoot = new UnityEvent();
	public UnityEvent OnShoot { get { return on_shoot; } }

	private int ammunition;
	private float last_shoot;
	private Player owner;

	/// <summary>
	/// Gets the current ammunition
	/// </summary>
	public virtual int Ammunition
	{
		get
		{
			return ammunition;
		}
	}

	/// <summary>
	/// Gets the weapon is empty
	/// </summary>
	public virtual bool Empty
	{
		get
		{
			return 0 >= ammunition;
		}
	}

	/// <summary>
	/// Bind player data
	/// </summary>
	/// <param name="owner"></param>
	public void Bind( Player owner )
	{
		ammunition = max_ammunition;
		this.owner = owner;
	}

	/// <summary>
	/// Shoot the weapon
	/// </summary>
	public void Shoot()
	{
		if (
			!Empty
			&& last_shoot < Time.time - cooldown
			&& handle_shoot( transform.position )
		)
		{
			ammunition--;
			last_shoot = Time.time;
		}
	}

	/// <summary>
	/// Shoot the weapon
	/// </summary>
	public void ShootLeader( Vector3 start_position )
	{
		if (
			last_shoot < Time.time - ( cooldown * ( 2f / Root.I.Get<PlayerManager>().All.Length ) )
			&& handle_shoot( start_position )
		)
		{
			last_shoot = Time.time;
		}
	}

	/// <summary>
	/// Handles the shooting logic
	/// </summary>
	/// <returns></returns>
	protected virtual bool handle_shoot( Vector3 start_position )
	{
		for( int i = 0 ; i < bullet_per_shot ; i++ )
		{
			create_bullet( bullet_prefab , start_position , shoot_direction( accuracy ) );
		}

		return true;
	}

	/// <summary>
	/// Creates a bullet by given prefab
	/// </summary>
	/// <param name="prefab"></param>
	protected void create_bullet( GameObject prefab , Vector3 position , Vector3 direction )
	{
		GameObject container = Instantiate( prefab );
		Bullet bullet = container.GetComponent<Bullet>();
		bullet.transform.position = position;
		bullet.Bind( owner , direction );

		on_shoot.Invoke();
	}

	/// <summary>
	/// Gets the shooting direction with an accuracy
	/// </summary>
	/// <param name="accuracy"></param>
	/// <returns></returns>
	protected Vector3 shoot_direction( float accuracy )
	{
		Vector3 forward = transform.forward;
		return Vector3.Normalize( new Vector3(
			forward.x + Random.Range( -accuracy , accuracy ),
			forward.y,
			forward.z + Random.Range( -accuracy , accuracy )
		));
	}
}
