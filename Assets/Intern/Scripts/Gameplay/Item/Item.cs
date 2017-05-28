using UnityEngine;
using System.Collections;

/// <summary>
/// Creator for item effects
/// </summary>
public abstract class Item : MonoBehaviour
{
	private bool applyed = false;
	[SerializeField]
	private GameObject item_effect;

	/// <summary>
	/// Apply item to a player
	/// </summary>
	/// <param name="player"></param>
	public virtual void Apply( Player player )
	{
		if ( applyed )
		{
			return;
		}

		applyed = true;
		handle_apply( player );
		player.ShowEffect( item_effect );

		Destroy( gameObject );
	}

	/// <summary>
	/// Apply your stuff here
	/// </summary>
	protected abstract void handle_apply( Player player );
}
