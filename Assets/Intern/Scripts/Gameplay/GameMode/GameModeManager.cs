using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Porvide mode specific logic
/// </summary>
public class GameModeManager : IRootObject
{
	private GameMode[] all = new GameMode[] {
		new Capture(),
		new Deathmatch(),
		new Timescore()
	};
	private GameMode mode = null;

	/// <summary>
	/// Gets that the current player result is empty
	/// </summary>
	public bool EmptyResult
	{
		get
		{
			return !Root.I.Get<PlayerManager>().All.Any( a => 0 < mode.GetScore( a ) );
		}
	}

	/// <summary>
	/// Gets that the current mode allows respawn
	/// </summary>
	public bool AllowRespawn
	{
		get
		{
			return mode.AllowRespawn;
		}
	}

	/// <summary>
	///  Gets that the mode allows capturing planets
	/// </summary>
	public bool AllowCapture
	{
		get
		{
			return mode.AllowCapture;
		}
	}

	/// <summary>
	///  Gets that the mode allows leader change
	/// </summary>
	public bool AllowLeader
	{
		get
		{
			return mode.AllowLeader;
		}
	}

	/// <summary>
	/// Gets that the mode allows item spawn
	/// </summary>
	public bool AllowItem
	{
		get
		{
			return mode.AllowItem;
		}
	}

	/// <summary>
	/// Gets that the mode uses teamplay
	/// </summary>
	public bool Teamplay
	{
		get
		{
			return mode.Teamplay;
		}
	}

	/// <summary>
	/// Gets that the mode uses an ai track
	/// </summary>
	public bool AITrack
	{
		get
		{
			return mode.AITrack;
		}
	}

	/// <summary>
	/// Binds the config
	/// </summary>
	/// <param name="name"></param>
	public void Bind( string name , GameConfig config )
	{
		mode = mode_by_name( name );
		mode.Bind( config );
	}

	/// <summary>
	/// Gets a mode by name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	private GameMode mode_by_name( string name )
	{
		foreach( GameMode mode in all )
		{
			if ( mode.Name == name )
			{
				return mode;
			}
		}

		return null;
	}

	/// <summary>
	/// Gets the allowed config by mode name
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public GameConfig AllowedConfig( string name )
	{
		return mode_by_name( name ).AllowedConfig;
	}

	/// <summary>
	/// Updates the score panel / handly victory
	/// </summary>
	public void Update()
	{
		mode.Update();
	}

	/// <summary>
	/// Shows the game result screen
	/// </summary>
	public void ShowGameResult()
	{
		mode.ShowGameResult();
	}
}