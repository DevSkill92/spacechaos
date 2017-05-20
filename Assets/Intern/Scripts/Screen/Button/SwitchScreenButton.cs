using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A ui helper to switch to given screen
/// </summary>
public class SwitchScreenButton : MonoBehaviour
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
		if ( null != target )
		{
			Root.I.Get<ScreenManager>().Switch( target );
		}
	}
}
