using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Spawns alive trigger instances
/// </summary>
public class AliveTriggerCreator : MonoBehaviour
{
	[SerializeField]
	private Transform creator;
	[SerializeField]
	private float interval;
	[SerializeField]
	private GameObject prefab;

	private float last_add;

	/// <summary>
	/// Call create in giver interval
	/// </summary>
	private void Update()
	{
		if ( Time.time - interval > last_add )
		{
			create();
		}
	}

	/// <summary>
	///  Spawns trigger instance
	/// </summary>
	private void create()
	{
		GameObject new_object = Instantiate( prefab );
		new_object.transform.position = creator.transform.position;
		new_object.transform.rotation = creator.transform.rotation;
	}

}