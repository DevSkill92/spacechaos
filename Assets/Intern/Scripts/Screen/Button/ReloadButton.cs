using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A ui helper to reload the game
/// </summary>
public class ReloadButton : ButtonHandler
{
	/// <summary>
	/// Do the swith
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		Root.I.Reload();
	}
}
