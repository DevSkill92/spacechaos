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
	private Vector2 chang_range;

	private PlayerManager player_manager;
	private bool allow_item;
	private float next_item;

	/// <summary>
	/// Check enabled, store PlayerManager
	/// </summary>
	public override void Enter()
	{
		base.Enter();

		player_manager = Root.I.Get<PlayerManager>();
		allow_item = Root.I.Get<GameModeManager>().AllowItem;

		gameObject.SetActive( allow_item );

		next_item = Time.time + 5;
	}

	/// <summary>
	/// Give items
	/// </summary>
	private void Update()
	{
		if (
			!allow_item
			|| Time.time < next_item
		)
		{
			return;
		}

		TrackCreator track = Root.I.Get<TrackCreator>();
		Instantiate( item_list[ Random.Range( 0 , item_list.Length ) ].gameObject )
			.transform.position = Root.I.Get<TrackCreator>().random_point( new Vector2( track.Length - 10 , track.Length - 3 )  ) + Vector3.up;

		next_item = Time.time + Random.Range( chang_range.x , chang_range.y );
	}
}
