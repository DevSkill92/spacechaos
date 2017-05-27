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
	/// Limit round to 5min
	/// </summary>
	public override float Timeout
	{
		get
		{
			return 300;
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

	/// <summary>
	/// Get score by kills
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public override int GetScore( Player player )
	{
		return player.KillCount;
	}
}
