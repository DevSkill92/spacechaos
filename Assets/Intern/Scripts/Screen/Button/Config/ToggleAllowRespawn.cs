using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Toggle allow respawn of the current config
/// </summary>
public class ToggleAllowRespawn : ToggleButton
{
	/// <summary>
	/// Apply
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		Title screen = Root.I.Get<ScreenManager>().Get<Title>();
		base.HandleClick( data );

		if ( screen.AllowedConfig.AllowRespawn )
		{
			screen.CurrentConfig.AllowRespawn = IsOn;
		}
	}
}
