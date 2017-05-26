﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Capture the flag mode
/// </summary>
public class Capture : GameMode
{
	public override bool AllowCapture
	{
		get
		{
			return true;
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
		throw new NotImplementedException();
	}
}
