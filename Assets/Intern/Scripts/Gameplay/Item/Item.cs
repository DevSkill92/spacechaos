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
	[SerializeField]
	private float lifetime = 4;
	[SerializeField]
	private float fade = 0.3f;

	private float start;

	private void Start()
	{
		start = Time.time;
		transform.localScale = Vector3.zero;
	}

	private void Update()
	{
		float expired = Time.time - start;

		// fade in / out
		if ( fade > expired )
		{
			transform.localScale = Vector3.one * ( expired / fade );
		}
		else if ( lifetime - fade < expired )
		{
			transform.localScale = Vector3.one * (  1 - ( ( expired - ( lifetime - fade ) ) / fade  ) );
		}
		else
		{
			transform.localScale = Vector3.one;
		}

		// Destroy after lifetime
		if (
			!applyed
			&& lifetime < expired
		)
		{
			Destroy( gameObject );
			applyed = true;
		}
	}

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
