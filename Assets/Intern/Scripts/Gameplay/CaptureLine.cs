using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Handle planet game logic
/// </summary>
public class CaptureLine : MonoBehaviour
{
	[SerializeField]
	private float timeout = 1;
	[SerializeField]
	private Transform container;
	[SerializeField]
	private Material material;

	private float last_show;
	private Planet current_target;

	/// <summary>
	/// Hide in appear
	/// </summary>
	private void Start()
	{
		gameObject.SetActive( false );
	}

	/// <summary>
	/// Hide and turn to target
	/// </summary>
	private void Update()
	{
		// hide after timeout
		if ( Time.time - timeout > last_show )
		{
			gameObject.SetActive( false );
			return;
		}

		if ( null != current_target )
		{
			Vector3 position = container.transform.position;
			Vector3 target_pos = current_target.transform.position;
			Vector3 center = Vector3.Lerp( target_pos , position , 0.5f );
			float distance = Vector3.Distance( position , target_pos );

			transform.LookAt( target_pos );

			transform.position = center;
			transform.localScale = new Vector3( 1 , 1 , distance );

			material.mainTextureScale = new Vector2( 1 , distance / 100 );
			material.mainTextureOffset = new Vector2( 0 , Time.time * 2 );
		}
	}

	/// <summary>
	/// Show line to target
	/// </summary>
	/// <param name="target"></param>
	public void Show( Planet target )
	{
		current_target = target;
		last_show = Time.time;
		gameObject.transform.SetParent( null );
		gameObject.SetActive( true );
	}
}