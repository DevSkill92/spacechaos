using UnityEngine;
using System.Collections;

/// <summary>
/// Creator for item effects
/// </summary>
public class Item : MonoBehaviour
{
	/// <summary>
	/// Apply item to a player
	/// </summary>
	/// <param name="player"></param>
	public virtual void Apply( Player player )
	{
		GameObject container = Instantiate( gameObject );
		container.transform.SetParent( player.transform , true );
	}
}
