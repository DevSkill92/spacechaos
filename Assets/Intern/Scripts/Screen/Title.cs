﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : Screen {
	[SerializeField]
	private Transform game_type;
	[SerializeField]
	private Transform game_config;
	[SerializeField]
	private Transform control;
	[SerializeField]
	private Transform start;

	private bool online = true;
	private GameConfig allowed_config;
	private GameConfig current_config;

	/// <summary>
	/// Get the current config allowed by mode
	/// </summary>
	public GameConfig AllowedConfig
	{
		get
		{
			return allowed_config;
		}
	}

	/// <summary>
	/// Gets the current config ( changable )
	/// </summary>
	public GameConfig CurrentConfig
	{
		get
		{
			return current_config;
		}
	}

	public override void Enter()
	{
		base.Enter();

		reset_state();
	}

	public void ShowLocal()
	{
		reset_state();

		game_type.gameObject.SetActive( true );
		online = false;
	}

	private void reset_state()
	{
		game_type.gameObject.SetActive( false );
		game_config.gameObject.SetActive( false );
		start.gameObject.SetActive( false );
		control.gameObject.SetActive( false );
	}

	/// <summary>
	/// Switch the current conig
	/// </summary>
	/// <param name="mode"></param>
	/// <returns></returns>
	public GameConfig SwitchConfig( string mode )
	{
		allowed_config = Root.I.Get<GameModeManager>().AllowedConfig( mode );
		current_config = allowed_config.Clone();
		return allowed_config;
	}

	/// <summary>
	/// Show controlls image
	/// </summary>
	public void ShowControl()
	{
		reset_state();
		control.gameObject.SetActive( true );
	}

	/// <summary>
	/// Starts the game with the current config
	/// </summary>
	public void StartGame()
	{
		CurrentConfig.Validate( allowed_config );
		Root.I.Get<GameModeManager>().Bind( CurrentConfig.ModeName , CurrentConfig );
		Root.I.Get<ScreenManager>().Switch<Game>().Bind( CurrentConfig );
	}

}
