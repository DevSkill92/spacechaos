using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class the access planets
/// </summary>
public class PlanetManager : RootGameComponent
{
	private Planet[] planet_list;

	/// <summary>
	/// Find planets in scene
	/// </summary>
	public override void Enter()
	{
		planet_list = FindObjectsOfType<Planet>();
	}

	/// <summary>
	/// Gets all planets in scene
	/// </summary>
	public Planet[] All
	{
		get
		{
			return planet_list;
		}
	}

	/// <summary>
	/// Gets all planets of a user
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public Planet[] PlayerPlanet( Player player )
	{
		if ( null == player )
		{
			return new Planet[ 0 ];
		}

		List<Planet> result = new List<Planet>();

		foreach ( Planet planet in All )
		{
			if ( planet.Owner == player )
			{
				if ( player.Alive )
				{
					result.Add( planet );
				}
				else
				{
					planet.Owner = null;
				}
			}
		}

		return result.ToArray();
	}

	/// <summary>
	/// Gets the last captured planet of a player
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public Planet LastPlayerPlanet( Player player )
	{
		Planet result = null;
		float min_date = float.MaxValue;
		foreach( Planet p in PlayerPlanet( player ) )
		{
			if ( p.LastCapture < min_date )
			{
				min_date = p.LastCapture;
				result = p;
			}
		}

		return result;
	}
}
