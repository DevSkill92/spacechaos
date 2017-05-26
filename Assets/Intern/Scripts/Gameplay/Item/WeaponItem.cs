using UnityEngine;
using System.Collections;

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
	public override void Apply( Player player )
	{
		player.AttachWeapon( weapon );
	}
}
