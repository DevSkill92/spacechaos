using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/// <summary>
/// A UI arrow which shows the next planet
/// </summary>
public class PlanetArrow : RootGameComponent
{
	[SerializeField]
	private Image color_image;

	private Player last_leader;
	private bool allow_capture;

	/// <summary>
	/// Store game instance
	/// </summary>
	public override void Enter()
	{
		base.Enter();
		allow_capture = Root.I.Get<GameModeManager>().AllowCapture;
	}

	/// <summary>
	/// Recolor and rotate
	/// </summary>
	private void Update()
	{
		PlayerManager player_manager = Root.I.Get<PlayerManager>();
		if ( null == player_manager )
		{
			return;
		}

		Player leader = player_manager.Leader;
		if (
			!allow_capture
			|| null == leader
		)
		{
			color_image.enabled = false;
			return;
		}


		if ( last_leader != leader )
		{
			last_leader = leader;
			color_image.color = leader.Color;
			color_image.enabled = true;
		}

		Planet nearest = null;
		float nearst_dst = float.MaxValue;
		foreach( Planet planet in Root.I.Get<PlanetManager>().All )
		{
			if ( planet.Owner == leader )
			{
				continue;
			}

			float dst = Vector3.Distance( leader.transform.position , planet.transform.position );
			if ( dst < nearst_dst )
			{
				nearst_dst = dst;
				nearest = planet;
			}
		}

		transform.rotation = Quaternion.Euler(new Vector3(
			0,
			0,
			( ( Mathf.Atan2( nearest.transform.position.z - leader.transform.position.z , nearest.transform.position.x - leader.transform.position.x ) / ( Mathf.PI * 2 ) ) * 360 ) - 90
		));
	}
}
