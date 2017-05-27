using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/// <summary>
/// Handles the game screen
/// </summary>
public class Game : Screen {
	[ SerializeField]
	private ScorePanel score_panel;
	[SerializeField]
	private Countdown countdown;
	[SerializeField]
	private RoundTimer round_timer;
	[SerializeField]
	private Collider[] initial_leader_trigger;

	private GameModeManager mode;
	private float last_mode_update;
	private PlayerManager player;

	/// <summary>
	/// Getter for the scorepanel
	/// </summary>
	public ScorePanel ScorePanel
	{
		get
		{
			return score_panel;
		}
	}

	/// <summary>
	/// Call leave methods
	/// </summary>
	public override void Leave()
	{
		base.Leave();
		foreach ( RootGameComponent component in FindObjectsOfType<RootGameComponent>() )
		{
			component.Leave();
		}
	}

	/// <summary>
	/// Find planets
	/// </summary>
	public override void Enter()
	{
		base.Enter();


		foreach ( RootGameComponent component in FindObjectsOfType<RootGameComponent>() )
		{
			component.Enter();
		}


		foreach ( Collider trigger in initial_leader_trigger )
		{
			trigger.gameObject.SetActive( true );
		}

		mode = Root.I.Get<GameModeManager>();
		player = Root.I.Get<PlayerManager>();
		player.OnKillAll.AddListener( Exit );

		round_timer.gameObject.SetActive( false );
	}

	/// <summary>
	/// Start round timer
	/// </summary>
	public void StartTimer()
	{
		round_timer.Bind( mode.Timeout );
	}

	/// <summary>
	/// Handle exit
	/// </summary>
	private void Update()
	{
		if ( Input.GetKeyUp( KeyCode.Escape ) )
		{
			Exit();
		}

		if ( Time.time - 1 > last_mode_update )
		{
			last_mode_update = Time.time;
			mode.Update();
		}
	}

	/// <summary>
	/// Binds the amount of player and spawn players
	/// </summary>
	/// <param name="amount"></param>
	/// <param name="allow_keyboard"></param>
	public void Bind( GameConfig config )
	{
		Root.I.Get<GameModeManager>().Bind( config.ModeName , config );

		int start = config.GamepadOnly ? 1 : 0;
		for ( int i = 0 ; i < config.PlayerAmount ; i++ )
		{
			player.SpawnPlayer( i + start , i );
		}

		countdown.Show();
	}

	/// <summary>
	/// Exit game to highscore or title
	/// </summary>
	public void Exit()
	{
		if ( mode.EmptyResult )
		{
			Root.I.Reload();
			return;
		}

		mode.ShowGameResult();
	}

}
