using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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