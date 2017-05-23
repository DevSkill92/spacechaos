using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
	private int victory_score = 3;
	[SerializeField]
	private UnityEvent switch_leader = new UnityEvent();
	[SerializeField]
	private ScorePanel score_panel;
	[SerializeField]
	private Countdown countdown;
	[SerializeField]
	private TrackCreator track_creator;
	[SerializeField]
	private Collider[] initial_leader_trigger;

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
	/// Gets all players
	/// </summary>
	public Player[] PlayerList
	{
		get
		{
			return player_list;
		}
	}

	/// <summary>
	/// Gets all planets in scene
	/// </summary>
	public Planet[] PlanetList
	{
		get
		{
			return planet_list;
		}
	}

	/// <summary>
	/// Gets the track container
	/// </summary>
	public TrackCreator TrackCreator
	{
		get
		{
			return track_creator;
		}
	}

	/// <summary>
	/// Find planets
	/// </summary>
	public override void Enter()
	{
		base.Enter();
		planet_list = FindObjectsOfType<Planet>();

		foreach ( Collider trigger in initial_leader_trigger )
		{
			trigger.gameObject.SetActive( true );
		}
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
		countdown.Show();
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
		last_switch = Time.time;

		foreach ( Collider trigger in initial_leader_trigger )
		{
			trigger.gameObject.SetActive( false );
		}

		// make sure that all players are follower
		foreach( Player p in player_list )
		{
			p.Follower();
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
		player.GetComponent<RequireAliveTrigger>().OnDead.AddListener( killed );
		return player;
	}

	/// <summary>
	/// Called on capture a plant
	/// </summary>
	/// <param name="plant"></param>
	public void Capture( Planet plant )
	{
		Dictionary<Player , int> score = calculate_score();
		if ( victory_score <= score.Values.Max() )
		{
			Root.I.Get<ScreenManager>().Switch<Result>().BindScore( score  );
			return;
		}

		if ( null != score_panel )
		{
			score_panel.Show( score );
		}
	}

	/// <summary>
	/// Called after a player dies
	/// </summary>
	/// <param name="player"></param>
	private void killed()
	{
		if ( 1 < PlayerList.Count( a => a.Alive ) )
		{
			return;
		}

		Dictionary<Player , int> score = calculate_score();
		if ( 0 >= score.Values.Max() )
		{
			Root.I.Reload();
			return;
		}

		Root.I.Get<ScreenManager>().Switch<Result>().BindScore( score );
	}

	/// <summary>
	/// Calculates the current score by player
	/// </summary>
	/// <returns></returns>
	private Dictionary<Player,int> calculate_score()
	{
		Dictionary<Player , int> result = new Dictionary<Player , int>();
		foreach( Player player in player_list )
		{
			result.Add( player , 0 );
		}

		foreach( Planet planet in planet_list )
		{
			if ( null != planet.Owner ) {
				if ( planet.Owner.Alive )
				{
					result[ planet.Owner ]++;
				} else {
					planet.Owner = null;
				}
			}
		}

		return result;
	}

}
