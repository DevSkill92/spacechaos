using UnityEngine;
using System.Collections;

/// <summary>
/// AI for deathmatch track
/// </summary>
public class AICar : MonoBehaviour
{
	[SerializeField]
	private Transform left_anchor;
	[SerializeField]
	private Transform right_anchor;
	[SerializeField]
	private Vector2 steer = new Vector2( 20 , 180 );
	[SerializeField]
	private Vector2 curve_length = new Vector2( 0.15f , 1f );
	[SerializeField]
	private Vector2 even_length = new Vector2( 1f , 5f );
	[SerializeField]
	private float speed = 38;
	[SerializeField]
	private float start_delay = 4;

	private float current_steer = 0;
	private float next_change;
	private float start_date;
	private float rotation;

	/// <summary>
	/// Change target / move
	/// </summary>
	public void Update()
	{
		if ( start_date > Time.time )
		{
			return;
		}

		// change target after timeout
		if ( next_change < Time.time )
		{
			change_target();
		}

		// move
		rotation += current_steer * Time.deltaTime;
		transform.rotation = Quaternion.Euler( new Vector3( 0 , rotation , 0 ) );
		transform.position += transform.forward * speed * Time.deltaTime;

		// show track
		Root.I.Get<TrackCreator>().UpdateTrack( left_anchor , right_anchor , Color.white , current_steer );
	}

	/// <summary>
	/// Reset current values
	/// </summary>
	/// <param name="postion"></param>
	public void ResetCar( Vector3 postion )
	{
		transform.position = postion;
		transform.rotation = Quaternion.LookRotation( Vector3.forward );
		rotation = 0;
		start_date = Time.time + start_delay;
	}

	/// <summary>
	/// Find next target values
	/// </summary>
	private void change_target()
	{
		if ( 1 == Random.Range( 0 , 2 ) )
		{
			// curve
			current_steer = Random.Range( steer.x , steer.y ) * ( 1 == Random.Range( 0 , 1 ) ? 1 : -1 );
			next_change = Time.time + Random.Range( curve_length.x , curve_length.y );
		}
		else
		{
			// event
			current_steer = 0;
			next_change = Time.time + Random.Range( even_length.x , even_length.y );
		}
	}

}
