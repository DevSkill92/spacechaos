using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Capture the flag mode
/// </summary>
public class Capture : GameMode
{
	private const int VICTORY_SCORE = 3;

	/// <summary>
	/// Returns true to allow capture
	/// </summary>
	public override bool AllowCapture
	{
		get
		{
			return true;
		}
	}

	/// <summary>
	/// Use planet display for score
	/// </summary>
	protected override ScoreSet.Type ScoreType
	{
		get
		{
			return ScoreSet.Type.Planet;
		}
	}

	/// <summary>
	/// Get score by captured planets
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
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

	/// <summary>
	/// Override to handle victory check
	/// </summary>
	/// <returns></returns>
	public override ScoreSet[] Update()
	{
		ScoreSet[] score = GetScore();

		foreach( ScoreSet team in score )
		{
			if ( VICTORY_SCORE <= team.Score / team.Player.Length )
			{
				// victory so exit to show game result
				Root.I.Get<ScreenManager>().Get<Game>().Exit();
			}
		}

		return score;
	}
}
