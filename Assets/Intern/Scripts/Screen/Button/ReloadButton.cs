using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A ui helper to reload the game
/// </summary>
public class ReloadButton : MonoBehaviour
{
	private bool triggered = false;

	/// <summary>
	/// auto add trigger
	/// </summary>
	void Start()
	{
		EventTrigger trigger = GetComponent<EventTrigger>();
		if ( null == trigger )
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener( HandleClick );

			gameObject.AddComponent<EventTrigger>().triggers.Add( entry );
		}
	}

	/// <summary>
	/// Do the swith
	/// </summary>
	/// <param name="data"></param>
	public virtual void HandleClick( BaseEventData data )
	{
		triggered = true;
	}

	/// <summary>
	/// Force reload after all Update functions are complete
	/// </summary>
	private void LateUpdate()
	{
		if ( triggered )
		{
			Root.I.Reload();
		}
	}
}
