using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Toggle allow item of the current config
/// </summary>
public class ToggleAllowItem : ToggleButton
{
	/// <summary>
	/// Apply
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		base.HandleClick( data );
		Title screen = Root.I.Get<ScreenManager>().Get<Title>();

		if ( screen.AllowedConfig.AllowItem )
		{
			screen.CurrentConfig.AllowItem = IsOn;
		}
	}
}
