using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Item for weapons
/// </summary>
public class WeaponItem : Item
{
	[SerializeField]
	private Weapon weapon;

	/// <summary>
	/// Override to attach instead of instantiate
	/// </summary>
	/// <param name="player"></param>
	protected override void handle_apply( Player player )
	{
		player.AttachWeapon( weapon );
	}
}
