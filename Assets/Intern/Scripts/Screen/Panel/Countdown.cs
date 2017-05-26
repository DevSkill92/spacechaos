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

		foreach( Player player in Root.I.Get<PlayerManager>().All )
		{
			player.Disable();
		}

		start = Time.time;
		instantiate_tick( 0 );

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

			foreach ( Player player in Root.I.Get<PlayerManager>().All )
			{
				player.Enable();
			}

			return;
		}

		// Show tick
		instantiate_tick( tick );
	}

	/// <summary>
	/// Instantiate tick prefab
	/// </summary>
	/// <param name="tick"></param>
	private void instantiate_tick( int tick )
	{
		Instantiate( tick_prefab[ tick ] ).transform.SetParent( transform , true );
	}
}