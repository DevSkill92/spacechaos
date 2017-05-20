using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Handle planet game logic
/// </summary>
public class Planet : MonoBehaviour
{
	[SerializeField]
	private float increase_speed;
	[SerializeField]
	private float decrease_speed;
	[SerializeField]
	private float max_amount;
	[SerializeField]
	private float cooldown;
	[SerializeField]
	private UnityEvent captured = new UnityEvent();

	private Dictionary<Player , float> player_progress = new Dictionary<Player , float>();
	private float last_capture = 0;
	private Player owner;

	/// <summary>
	/// Gets the current owner
	/// </summary>
	public Player Owner
	{
		get
		{
			return owner;
		}
	}

	// Decrease player progress
	private void Update()
	{
		List<Player> remove_player = new List<Player>();

		// decrease progress
		foreach( Player player in player_progress.Keys )
		{
			player_progress[ player ] -= decrease_speed * Time.deltaTime;
			if ( 0 >= player_progress[ player ] ) {
				remove_player.Add( player );
			}
		}

		// remove outdated players
		foreach( Player player in remove_player )
		{
			player_progress.Remove( player );
		}
	}

	/// <summary>
	/// Capturing progress
	/// </summary>
	/// <param name="player"></param>
	public void Capture( Player player )
	{
		if ( last_capture > Time.time - cooldown )
		{
			return;
		}

		if ( !player_progress.ContainsKey( player ) )
		{
			player_progress.Add( player , 0 );
		}

		player_progress[ player ] = Time.deltaTime * increase_speed;

		if ( player_progress[ player ] > max_amount )
		{
			capture_complete( player );
		}
	}

	/// <summary>
	/// Take over the planet
	/// </summary>
	/// <param name="owner"></param>
	private void capture_complete( Player owner )
	{
		player_progress = new Dictionary<Player , float>();
		this.owner = owner;
		captured.Invoke();
		last_capture = Time.time;

		Root.I.Get<ScreenManager>().Get<Game>().Capture( this );
	}
}