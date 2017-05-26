using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Exit button in menu
/// </summary>
public class Exit : ButtonHandler
{
	/// <summary>
	/// Exit to windows
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		Application.Quit();
	}
}
