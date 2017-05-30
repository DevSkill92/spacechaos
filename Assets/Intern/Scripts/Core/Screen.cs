using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class Screen : MonoBehaviour
{
	[SerializeField]
	private Transform[] elements;

	[SerializeField]
	private UnityEvent on_click = new UnityEvent();
	public UnityEvent OnClick { get { return on_click; } }

	public virtual void Enter()
	{
		foreach( Transform element in elements )
		{
			element.gameObject.SetActive( true );
		}
	}

	private void Update()
	{
		if ( Input.GetMouseButtonUp( 0 ) )
		{
			on_click.Invoke();
		}
	}

	public virtual void Leave()
	{
		foreach ( Transform element in elements )
		{
			element.gameObject.SetActive( false );
		}
	}
}
