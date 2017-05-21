using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Handles the player highscore
/// </summary>
public class ScorePanel : MonoBehaviour
{
	[SerializeField]
	private Transform entry_container;
	[SerializeField]
	private GameObject entry_prefab;

	/// <summary>
	/// Hide one start do prevent empty results
	/// </summary>
	private void Awake()
	{
		gameObject.SetActive( false );
	}

	/// <summary>
	/// Update the displayed content
	/// </summary>
	/// <param name="score"></param>
	public void Show( Dictionary<Player,int> score )
	{
		gameObject.SetActive( true );

		// clear
		foreach( Transform child in entry_container )
		{
			Destroy( child.gameObject );
		}

		// create new entries
		foreach( Player player in score.OrderBy( a => a.Value ).Select( a => a.Key ) )
		{
			if ( 0 < score[ player ] )
			{
				GameObject entry = Instantiate( entry_prefab );
				entry.transform.SetParent( entry_container , true );
				entry.transform.localScale = Vector3.one;
				entry.GetComponent<ScoreEntry>().Bind( player , score[ player ] );
			}
		}
	}
}
