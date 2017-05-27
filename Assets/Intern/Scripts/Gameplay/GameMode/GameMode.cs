using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Abstract class for the game modies
/// </summary>
public abstract class GameMode
{
	private GameConfig current_config;

	/// <summary>
	/// Name of the mode ( default: class name )
	/// </summary>
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
	/// Timeout for rounds 0 = no timeout
	/// </summary>
	public virtual float Timeout
	{
		get
		{
			return 0;
		}
	}

	/// <summary>
	/// Get the used score type for highscore
	/// </summary>
	protected virtual ScoreSet.Type ScoreType
	{
		get
		{
			return ScoreSet.Type.Number;
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
	/// Gets the score of a team
	/// </summary>
	/// <returns></returns>
	public int GetScore( Player[] player )
	{
		return player.Sum( a => GetScore( a ) );
	}

	/// <summary>
	/// Gets the current highscore
	/// </summary>
	/// <returns></returns>
	public virtual ScoreSet[] GetScore()
	{
		Player[] player_list = Root.I.Get<PlayerManager>().All;
		ScoreSet[] result;

		if ( Teamplay )
		{
			result = new ScoreSet[ player_list.Length / 2 ];
			for ( int i = 0 ; i < result.Length ; i++ )
			{
				Player[] team = new Player[] { player_list[ i * 2 ] , player_list[ ( i * 2 ) + 1 ] };

				result[ i ] = new ScoreSet()
				{
					Player = team ,
					DisplayType = ScoreType ,
					Score = GetScore( team )
				};
			}

		}
		else
		{
			result = new ScoreSet[ player_list.Length ];
			for ( int i = 0 ; i < result.Length ; i++ )
			{
				result[ i ] = new ScoreSet()
				{
					Player = new Player[] { player_list[ i ] } ,
					DisplayType = ScoreType ,
					Score = GetScore( player_list[ i ] )
				};
			}
		}

		return result;
	}

	/// <summary>
	/// Updates the score
	/// Do victory check here
	/// </summary>
	public virtual ScoreSet[] Update()
	{
		return GetScore();
	}
}