using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract class for the game modies
/// </summary>
public abstract class GameMode
{
	private GameConfig current_config;

	public string Name
	{
		get
		{
			return GetType().Name;
		}
	}

	/// <summary>
	/// Gets that the mode allows respawn
	/// </summary>
	public virtual bool AllowRespawn
	{
		get
		{
			return current_config.AllowRespawn;
		}
	}

	/// <summary>
	/// Gets that the mode allows item spawn
	/// </summary>
	public virtual bool AllowItem
	{
		get
		{
			return current_config.AllowItem;
		}
	}

	/// <summary>
	/// Gets that the mode uses teamplay
	/// </summary>
	public virtual bool Teamplay
	{
		get
		{
			return current_config.Teamplay;
		}
	}


	/// <summary>
	/// Gets that the mode allows capturing planets
	/// </summary>
	public virtual bool AllowCapture
	{
		get
		{
			return false;
		}
	}

	/// <summary>
	/// Gets that the mode allows leader change
	/// </summary>
	public virtual bool AllowLeader
	{
		get
		{
			return true;
		}
	}

	/// <summary>
	/// Gets that the mode uses an ai track
	/// </summary>
	public virtual bool AITrack
	{
		get
		{
			return false;
		}
	}

	/// <summary>
	/// Gets all allowed options of this mode
	/// </summary>
	public virtual GameConfig AllowedConfig
	{
		get
		{
			return new GameConfig()
			{
				Teamplay = true ,
				AllowItem = true ,
				GamepadOnly = true ,
				AllowRespawn = true ,
				ModeName = Name ,
				PlayerAmount = 2
			};
		}
	}

	/// <summary>
	/// Bind the current config
	/// </summary>
	/// <param name="config"></param>
	public void Bind( GameConfig config )
	{
		config.Validate( AllowedConfig );
		current_config = config;
	}

	/// <summary>
	/// Gets the score of a single player
	/// </summary>
	/// <param name="player"></param>
	/// <returns></returns>
	public abstract int GetScore( Player player );

	/// <summary>
	/// Updates the score panel
	/// Do victory check here
	/// </summary>
	public abstract void Update();

	/// <summary>
	/// Shows the game result screen
	/// </summary>
	public abstract void ShowGameResult();
}