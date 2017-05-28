using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Apply player color to renderer
/// </summary>
public class PlayerColor : MonoBehaviour
{
	[SerializeField]
	private Player player;

	/// <summary>
	/// Hide in appear
	/// </summary>
	private void Start()
	{
		Color color = player.Color;

		MeshRenderer renderer = GetComponent<MeshRenderer>();
		if ( null == renderer )
		{
			renderer = GetComponentInChildren<MeshRenderer>();
		}
		if ( null != renderer )
		{
			renderer.material.color = color;
		}

		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if ( null == sprite )
		{
			sprite = GetComponentInChildren<SpriteRenderer>();
		}
		if ( null != sprite )
		{
			sprite.color = color;
		}
	}
}