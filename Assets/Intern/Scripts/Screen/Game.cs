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
	private Transform score_panel;
	[SerializeField]
	private Countdown countdown;
	[SerializeField]
	private Collider[] initial_leader_trigger;

	private GameModeManager score;
	private PlayerManager player;

	/// <summary>
	/// Getter for the scorepanel
	/// </summary>
	public Transform ScorePanel
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

		score = Root.I.Get<GameModeManager>();
		player = Root.I.Get<PlayerManager>();
		player.OnKillAll.AddListener( Exit );
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

		score.Update();
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
		if ( score.EmptyResult )
		{
			Root.I.Reload();
			return;
		}

		score.ShowGameResult();
	}

}
