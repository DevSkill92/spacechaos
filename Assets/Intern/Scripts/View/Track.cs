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

	private void Update()
	{
		Root.I.Get<ScreenManager>().Get<Game>().TrackCreator.UpdateTrack( left_anchor , right_anchor , player.Color );
	}
}