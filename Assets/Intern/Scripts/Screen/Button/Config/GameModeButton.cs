using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameModeButton : ButtonHandler
{
	[SerializeField]
	private string mode;
	[SerializeField]
	private Sprite background_on;
	[SerializeField]
	private Sprite background_off;
	[SerializeField]
	private Transform start;
	[SerializeField]
	private Transform config;

	[SerializeField]
	private ToggleButton teamplay;
	[SerializeField]
	private ToggleButton gamepad_only;
	[SerializeField]
	private ToggleButton allow_item;
	[SerializeField]
	private ToggleButton allow_respawn;

	private Image image;

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	/// <summary>
	/// Reset
	/// </summary>
	public void OnEnable()
	{
		show_state( false );
	}

	/// <summary>
	/// Enable mode
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		// disale other buttons
		foreach( GameModeButton button in transform.parent.GetComponentsInChildren<GameModeButton>() )
		{
			button.show_state( false );
		}

		// enable button
		config.gameObject.SetActive( true );
		start.gameObject.SetActive( true );
		show_state( true );

		// show game config
		show_config( Root.I.Get<ScreenManager>().Get<Title>().SwitchConfig( mode ) );
	}

	/// <summary>
	/// Show current state
	/// </summary>
	private void show_state( bool state )
	{
		image.sprite = state ? background_on : background_off;
	}

	/// <summary>
	/// Customize option items for config
	/// </summary>
	/// <param name="config"></param>
	private void show_config( GameConfig config )
	{
		teamplay.gameObject.SetActive( false );
		gamepad_only.gameObject.SetActive( config.GamepadOnly );
		allow_item.gameObject.SetActive( config.AllowItem );
		allow_respawn.gameObject.SetActive( config.AllowRespawn );
	}
}
