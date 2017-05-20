using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the game screen
/// </summary>
public class Game : Screen {
	[SerializeField]
	private GameObject player_prefab;
	[SerializeField]
	private Transform[] start_point;
	[SerializeField]
	private float switch_cooldown = 2;
	[SerializeField]
	private UnityEvent switch_leader = new UnityEvent();

	private Player leader;
	private float last_switch;
	private Player[] player_list;
	private Planet[] planet_list;

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
	/// Gets all planets in scene
	/// </summary>
	public Planet[] Planet_List
	{
		get
		{
			return planet_list;
		}
	}

	/// <summary>
	/// Find planets
	/// </summary>
	private void Start()
	{
		planet_list = FindObjectsOfType<Planet>();
	}

	/// <summary>
	/// Binds the amount of player and spawn players
	/// </summary>
	/// <param name="amount"></param>
	/// <param name="allow_keyboard"></param>
	public void BindPlayerAmount( int amount , bool allow_keyboard )
	{
		int start = allow_keyboard ? 0 : 1;

		player_list = new Player[ amount ];
		for ( int i = 0 ; i < amount ; i++ )
		{
			player_list[ i ] = spawn_player( start + i , i );
		}
	}

	/// <summary>
	/// Switch the leader to given player if possible
	/// </summary>
	/// <param name="player"></param>
	public bool RequestLeaderSwitch( Player player )
	{
		if ( last_switch > Time.time - switch_cooldown )
		{
			return false;
		}

		if ( null != leader )
		{
			leader.Follower();
		}

		leader = player;
		leader.Leader();

		switch_leader.Invoke();

		return true;
	}

	/// <summary>
	/// Spawns a player
	/// </summary>
	/// <param name="joynum"></param>
	/// <param name="index"></param>
	private Player spawn_player( int joy, int index )
	{
		GameObject container = Instantiate( player_prefab );
		Player player = container.GetComponent<Player>();
		player.GetComponent<Player>().BindIndex( joy , index );
		player.transform.position = start_point[ index ].transform.position;
		return player;
	}

	/// <summary>
	/// Called on capture a plant
	/// </summary>
	/// <param name="plant"></param>
	public void Capture( Planet plant )
	{
	}

}
