using System.Collections.Generic;
using System.Linq;
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
	private GameObject capture_effect;

	[SerializeField]
	private UnityEvent on_captured = new UnityEvent();
	public UnityEvent OnCaptured { get { return on_captured; } }

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
		set
		{
			owner = value;
			last_capture = Time.time;
		}
	}
	
	/// <summary>
	/// Gets the date of the last capture
	/// </summary>
	public float LastCapture
	{
		get
		{
			return last_capture;
		}
	}

	// Decrease player progress
	private void Update()
	{
		// decrease progress
		foreach ( Player player in player_progress.Keys.ToArray() )
		{
			player_progress[ player ] -= decrease_speed * Time.deltaTime;
			if ( 0 >= player_progress[ player ] ) {
				player_progress[ player ] = 0;
			}
		}
	}

	/// <summary>
	/// Capturing progress
	/// </summary>
	/// <param name="player"></param>
	public bool Capture( Player player )
	{
		if ( 
			last_capture > Time.time - cooldown
			|| Owner == player
			|| player != Root.I.Get<PlayerManager>().Leader
		)
		{
			return false;
		}

		if ( !player_progress.ContainsKey( player ) )
		{
			player_progress.Add( player , 0 );
		}

		player_progress[ player ] += Time.deltaTime * increase_speed;

		if ( player_progress[ player ] > max_amount )
		{
			capture_complete( player );
			Root.I.Get<ScreenManager>().Get<Game>().CapturePanel.gameObject.SetActive( false );
		}
		else
		{
			Root.I.Get<ScreenManager>().Get<Game>().CapturePanel.Show( player.Color , player_progress[ player ] / max_amount );
		}

		return true;
	}

	/// <summary>
	/// Take over the planet
	/// </summary>
	/// <param name="owner"></param>
	private void capture_complete( Player owner )
	{
		player_progress = new Dictionary<Player , float>();
		this.owner = owner;
		on_captured.Invoke();
		last_capture = Time.time;

		owner.ShowEffect( capture_effect , transform.position );
	}
}