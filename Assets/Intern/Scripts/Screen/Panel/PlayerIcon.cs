using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the player color on given target image
/// </summary>
public class PlayerIcon : MonoBehaviour
{
	[SerializeField]
	private Image target_image;

	/// <summary>
	/// Bind player color
	/// </summary>
	/// <param name="player"></param>
	public void Bind( Player player )
	{
		target_image.color = player.Color;
	}
}