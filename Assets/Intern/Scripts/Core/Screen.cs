using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Screen : MonoBehaviour
{
	[SerializeField]
	private Transform[] elements;

	public virtual void Enter()
	{
		foreach( Transform element in elements )
		{
			element.gameObject.SetActive( true );
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
