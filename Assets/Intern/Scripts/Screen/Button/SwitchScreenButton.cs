using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A ui helper to switch to given screen
/// </summary>
public class SwitchScreenButton : ButtonHandler
{
	[SerializeField]
	private Screen target;

	/// <summary>
	/// Gets the target screen
	/// </summary>
	protected Screen Target
	{
		get
		{
			return target;
		}
	}

	/// <summary>
	/// Do the swith
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		if ( null != target )
		{
			Root.I.Get<ScreenManager>().Switch( target );
		}
	}
}
