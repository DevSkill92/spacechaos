using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Destroys the component after timeout
/// </summary>
public class DestroyTimeout : MonoBehaviour
{
	[SerializeField]
	private float timeout;

	private float expired = 0;
	private bool destroyed = false;

	private void Start()
	{
		expired = Time.time + timeout;
	}

	/// <summary>
	/// Start timer
	/// </summary>
	/// <param name="timeout"></param>
	public void Bind( float timeout )
	{
		this.timeout = timeout;
		expired = Time.time + timeout;
	}

	/// <summary>
	/// Do check, destory
	/// </summary>
	private void Update()
	{
		if ( 
			Time.time > expired
			&& !destroyed
		)
		{
			Destroy( gameObject );
			destroyed = true;
		}
	}

}