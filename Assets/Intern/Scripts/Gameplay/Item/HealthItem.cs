using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Item for weapons
/// </summary>
public class HealthItem : Item
{
	[SerializeField]
	private float health = 10;

	/// <summary>
	/// Override to attach instead of instantiate
	/// </summary>
	/// <param name="player"></param>
	protected override void handle_apply( Player player )
	{
		player.GiveHealth( health );
	}
}
