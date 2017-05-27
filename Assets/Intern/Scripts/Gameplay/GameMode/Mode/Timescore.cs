using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Capture the flag mode
/// </summary>
public class Timescore : GameMode
{
	public override bool AllowCapture
	{
		get
		{
			return false;
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
	/// Get score by leader time
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public override int GetScore( Player player )
	{
		return Mathf.FloorToInt( player.LeaderTime );
	}
}
