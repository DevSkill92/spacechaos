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
	private Dictionary<Player , float> next_item = new Dictionary<Player , float>();
	private bool allow_item;

	/// <summary>
	/// Check enabled, store PlayerManager
	/// </summary>
	public override void Enter()
	{
		base.Enter();

		player_manager = Root.I.Get<PlayerManager>();
		allow_item = Root.I.Get<GameModeManager>().AllowItem;
		next_item = new Dictionary<Player , float>();

		gameObject.SetActive( allow_item );
	}

	/// <summary>
	/// Give items
	/// </summary>
	private void Update()
	{
		if ( !allow_item )
		{
			return;
		}

		foreach( Player player in player_manager.All )
		{
			if ( !next_item.ContainsKey( player ) )
			{
				next_item.Add( player , 0 );
			}

			if ( next_item[ player ] < Time.time )
			{
				give_item( player );
				next_item[ player ] = Time.time + Random.Range( chang_range.x , chang_range.y );
			}
		}
	}

	/// <summary>
	/// Give item to player
	/// </summary>
	private void give_item( Player player )
	{
		item_list[ Random.Range( 0 , item_list.Length ) ].Apply( player );
	}
}
