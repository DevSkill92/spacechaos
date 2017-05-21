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
	private GameObject score_item_prefab;
	[SerializeField]
	private Transform score_container;
	[SerializeField]
	private Image player_color;

	public void Bind( Player player , int score )
	{
		player_color.color = player.Color;

		foreach( Transform child in score_container )
		{
			DestroyImmediate( child.gameObject );
		}

		for( int i = 0 ; i < score ; i++ )
		{
			GameObject score_item = Instantiate( score_item_prefab );
			score_item.transform.SetParent( score_container , true );
			score_item.transform.localScale = Vector3.one;
		}
	}
}