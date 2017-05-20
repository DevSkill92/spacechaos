using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles the countdown on enter the game
/// </summary>
public class Countdown : MonoBehaviour
{
	[SerializeField]
	private GameObject[] tick_prefab;

	private float start;
	private int last_tick;

	/// <summary>
	/// Starts the countdown
	/// </summary>
	public void Show()
	{
		// Clean up
		foreach( Transform child in transform )
		{
			Destroy( child );
		}

		foreach( Player player in Root.I.Get<ScreenManager>().Get<Game>().PlayerList )
		{
			player.Disable();
		}

		start = Time.time;

		gameObject.SetActive( true );
	}
	
	/// <summary>
	/// Handle ticks
	/// </summary>
	private void Update()
	{
		int tick = Mathf.FloorToInt( Time.time - start );

		// handle only full secounds
		if ( tick == last_tick )
		{
			return;
		}
		last_tick = tick;

		// Handle ready
		if ( tick >= tick_prefab.Length )
		{
			gameObject.SetActive( false );

			foreach ( Player player in Root.I.Get<ScreenManager>().Get<Game>().PlayerList )
			{
				player.Enable();
			}

			return;
		}

		// Show tick
		Instantiate( tick_prefab[ tick ] ).transform.SetParent( transform , true );
	}
}