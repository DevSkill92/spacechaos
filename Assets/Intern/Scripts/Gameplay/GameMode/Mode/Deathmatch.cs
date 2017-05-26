using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Capture the flag mode
/// </summary>
public class Deathmatch : GameMode
{

	public override bool AITrack
	{
		get
		{
			return true;
		}
	}

	public override bool AllowLeader
	{
		get
		{
			return false;
		}
	}

	public override bool AllowItem
	{
		get
		{
			return true;
		}
	}

	/// <summary>
	/// Gets all allowed options of this mode
	/// </summary>
	public override GameConfig AllowedConfig
	{
		get
		{
			return new GameConfig()
			{
				Teamplay = true ,
				AllowItem = false ,
				GamepadOnly = true ,
				AllowRespawn = true ,
				ModeName = Name ,
				PlayerAmount = 2
			};
		}
	}

	public override int GetScore( Player player )
	{
		int result = 0;

		foreach ( Planet planet in Root.I.Get<PlanetManager>().All )
		{
			if ( planet.Owner == player )
			{
				if ( player.Alive )
				{
					result++;
				}
				else
				{
					planet.Owner = null;
				}
			}
		}

		return result;
	}

	public override void ShowGameResult()
	{
		//Root.I.Get<ScreenManager>().Switch<Result>().BindScore( score );
	}

	public override void Update()
	{
	}
}
