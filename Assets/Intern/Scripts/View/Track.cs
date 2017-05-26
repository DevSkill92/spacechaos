using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Generates a track
/// </summary>
public class Track : MonoBehaviour
{
	[SerializeField]
	private Transform left_anchor;
	[SerializeField]
	private Transform right_anchor;
	[SerializeField]
	private Player player;
	[SerializeField]
	private SimpleCar car;
	[SerializeField]
	private float start_delay = 0.15f;

	private bool enabled = false;
	private float enabled_date;

	/// <summary>
	/// Enables the track
	/// </summary>
	public void Enable()
	{
		enabled = true;
		gameObject.SetActive( enabled );
		enabled_date = Time.time;
	}

	/// <summary>
	/// Disables the track
	/// </summary>
	public void Disable()
	{
		enabled = false;
		gameObject.SetActive( enabled );
	}

	private void Update()
	{
		if ( 
			enabled
			&& Time.time > enabled_date + start_delay
		)
		{
			Root.I.Get<TrackCreator>().UpdateTrack( left_anchor , right_anchor , player.Color , car.Steer );
		}
	}
}