using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles ui screens
/// </summary>
public class ScreenManager : RootComponent {

	[SerializeField]
	private Screen[] screen_list;
	[SerializeField]
	private Screen initial_screen;

	private Screen active;

	/// <summary>
	/// Get active screen
	/// </summary>
	public Screen Active
	{
		get
		{
			return active;
		}
	}

	/// <summary>
	/// Show initial screen
	/// </summary>
	private void Awake()
	{
		foreach ( Screen screen in screen_list )
		{
			screen.Leave();
		}

		active = initial_screen;
		active.Enter();
	}

	/// <summary>
	/// Switch to given screen
	/// </summary>
	/// <param name="screen"></param>
	public void Switch( Screen screen )
	{
		if ( active == screen )
		{
			return;
		}

		active.Leave();
		
		foreach ( Screen current in screen_list )
		{
			if ( current == screen )
			{
				current.Enter();
				active = screen;
				return;
			}
		}
	}

	/// <summary>
	/// Get screen of givent type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T Get<T>() where T : Screen
	{
		foreach ( Screen screen in screen_list )
		{
			if ( screen is T )
			{
				return screen as T;
			}
		}

		return null;
	}

	/// <summary>
	/// Switch to screen of givent type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T Switch<T>() where T: Screen
	{
		if ( active is T )
		{
			return null;
		}
		
		foreach ( Screen screen in screen_list )
		{
			if ( screen is T )
			{
				Switch( screen );
				return screen as T;
			}
		}

		return null;
	}
}
