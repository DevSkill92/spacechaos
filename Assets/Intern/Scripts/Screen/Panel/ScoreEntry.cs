using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI Helper to show a highscore entry
/// </summary>
public class ScoreEntry : MonoBehaviour
{
	[SerializeField]
	private GameObject player_prefab;
	[SerializeField]
	private Transform player_container;
	[SerializeField]
	private Transform score_container;
	[SerializeField]
	private Image player_type_icon;
	[SerializeField]
	private Sprite player_type_icon_single;
	[SerializeField]
	private Sprite player_type_icon_team;
	[SerializeField]
	private ScoreDisplayType[] score_type_handler;

	public void Bind( ScoreSet score )
	{
		// set type icon sprite
		player_type_icon.sprite = 1 < score.Player.Length ? player_type_icon_team : player_type_icon_single;

		// Show players
		foreach( Player player in score.Player )
		{
			GameObject item = Instantiate( player_prefab );
			item.transform.SetParent( player_container , false );
			item.GetComponent<PlayerIcon>().Bind( player );
		}


		// Show score
		foreach( ScoreDisplayType type in score_type_handler )
		{
			if ( type.Type == score.DisplayType )
			{
				GameObject item = Instantiate( type.gameObject );
				item.transform.SetParent( score_container , false );
				( item.GetComponent( type.GetType() ) as ScoreDisplayType ).Bind( score );
			}
		}
	}
}