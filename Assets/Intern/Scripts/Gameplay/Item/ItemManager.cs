using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Provide items for players
/// </summary>
public class ItemManager : RootGameComponent
{
	[SerializeField]
	private Item[] item_list;
	[SerializeField]
	private Item[] priority_item_list;
	[SerializeField]
	private Vector2 chang_range;

	private PlayerManager player_manager;
	private GameModeManager mode;
	private float next_item;

	/// <summary>
	/// Check enabled, store PlayerManager
	/// </summary>
	public override void Enter()
	{
		base.Enter();

		player_manager = Root.I.Get<PlayerManager>();
		mode = Root.I.Get<GameModeManager>();

		gameObject.SetActive( mode.AllowItem );

		next_item = Time.time + 5;
	}

	/// <summary>
	/// Give items
	/// </summary>
	private void Update()
	{
		if (
			!mode.AllowItem
			|| Time.time < next_item
		)
		{
			return;
		}

		TrackCreator track = Root.I.Get<TrackCreator>();
		Vector3 position = Root.I.Get<TrackCreator>().random_point( new Vector2( track.Length - 10 , track.Length - 3 ) );

		if ( 0 != position.magnitude )
		{
			Item item = null;
			float max_prio = ( ( ( player_manager.All.Length + mode.ItemPriorityAdd ) - 2 ) * 0.17f ) + 0.25f;
			if (
				0.25f < max_prio
				&& max_prio * 100 > Random.Range( 0 , 100 )
			)
			{
				item = priority_item_list[ Random.Range( 0 , priority_item_list.Length ) ];
			}
			else
			{
				item = item_list[ Random.Range( 0 , item_list.Length ) ];
			}

			Instantiate( item.gameObject ).transform.position = position + Vector3.up;
		}

		next_item = Time.time + Random.Range( chang_range.x , chang_range.y );
	}
}
