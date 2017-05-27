using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Button handler
/// </summary>
public abstract class ButtonHandler : MonoBehaviour
{
	/// <summary>
	/// auto add trigger
	/// </summary>
	private void Start()
	{

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerClick;
		entry.callback.AddListener( HandleClick );

		EventTrigger trigger = GetComponent<EventTrigger>();
		if ( null == trigger )
		{
			gameObject.AddComponent<EventTrigger>().triggers.Add( entry );
		}
		else
		{
			trigger.triggers.Add( entry );
		}
	}

	/// <summary>
	/// Handle click here
	/// </summary>
	/// <param name="data"></param>
	public abstract void HandleClick( BaseEventData data );
}
