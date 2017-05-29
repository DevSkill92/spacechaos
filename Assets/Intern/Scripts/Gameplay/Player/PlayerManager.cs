using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// Handles the players in the game
/// </summary>
public class PlayerManager : RootGameComponent
{
	[SerializeField]
	private GameObject player_prefab;
	[SerializeField]
	private Transform[] start_point;
	[SerializeField]
	private AICar ai;
	[SerializeField]
	private Transform ai_start;
	[SerializeField]
	private float switch_cooldown = 2;

	[SerializeField]
	private UnityEvent on_switch_leader = new UnityEvent();
	public UnityEvent OnSwitchLeader { get { return on_switch_leader; } }
	[SerializeField]
	private UnityEvent on_kill_all = new UnityEvent();
	public UnityEvent OnKillAll { get { return on_kill_all; } }

	private Player leader;
	private float last_switch;
	private List<Player> player_list = new List<Player>();

	/// <summary>
	/// Get the current leader
	/// </summary>
	public Player Leader
	{
		get
		{
			return leader;
		}
	}

	/// <summary>
	/// Gets all players
	/// </summary>
	public Player[] All
	{
		get
		{
			return player_list.ToArray();
		}
	}

	/// <summary>
	/// Getter for the ai car
	/// </summary>
	public AICar AICar
	{
		get
		{
			return ai;
		}
	}

	/// <summary>
	/// Clear on enter
	/// </summary>
	public override void Enter()
	{
		base.Enter();

		player_list = new List<Player>();
		ai.ResetCar( ai_start.transform.position );
		ai.gameObject.SetActive( Root.I.Get<GameModeManager>().AITrack );
	}

	/// <summary>
	/// Spawns a player
	/// </summary>
	/// <param name="joynum"></param>
	/// <param name="index"></param>
	public Player SpawnPlayer( int joy , int index )
	{
		GameObject container = Instantiate( player_prefab );
		Player player = container.GetComponent<Player>();
		player.GetComponent<Player>().BindIndex( joy , index );
		player.transform.position = start_point[ index ].transform.position;
		player.GetComponent<RequireAliveTrigger>().OnDead.AddListener( killed );

		player_list.Add( player );

		return player;
	}

	/// <summary>
	/// Switch the leader to given player if possible
	/// </summary>
	/// <param name="player"></param>
	public bool RequestLeaderSwitch( Player player=null )
	{
		if (
			null != player
			&& last_switch > Time.time - switch_cooldown 
		)
		{
			return false;
		}

		// switch to nearest other player
		if ( null == player )
		{
			if ( null == leader )
			{
				return false;
			}

			float min_dst = float.MaxValue;
			Vector3 leader_pos = leader.transform.position;
			foreach( Player p in All )
			{
				if ( 
					p == leader
					|| !p.Alive
				)
				{
					continue;
				}

				float dst = Vector3.Distance( p.transform.position , leader_pos );
				if ( dst < min_dst )
				{
					min_dst = dst;
					player = p;
				}
			}
		}


		// make sure that all players are follower
		bool speed_breaker = null != leader;
		foreach ( Player p in player_list )
		{
			p.Follower( speed_breaker );
		}
		leader = null;

		if ( null != player )
		{
			last_switch = Time.time;

			leader = player;
			leader.Leader();

			on_switch_leader.Invoke();
		}

		return true;
	}

	/// <summary>
	/// Called after a player dies
	/// </summary>
	/// <param name="player"></param>
	private void killed()
	{
		if ( 1 < All.Count( a => a.Alive ) )
		{
			return;
		}

		on_kill_all.Invoke();
	}
}
