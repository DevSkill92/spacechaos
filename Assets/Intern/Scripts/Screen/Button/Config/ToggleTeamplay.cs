using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Toggle teamplay of the current config
/// </summary>
public class ToggleTeamplay : ToggleButton
{
	/// <summary>
	/// Apply
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		base.HandleClick( data );
		Title screen = Root.I.Get<ScreenManager>().Get<Title>();
		if ( 3 >= screen.CurrentConfig.PlayerAmount )
		{
			gameObject.SetActive( false );
			return;
		}

		if ( screen.AllowedConfig.Teamplay )
		{
			screen.CurrentConfig.Teamplay = IsOn;
		}
	}
}
