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
	public void Show( ScoreSet[] score )
	{
		gameObject.SetActive( true );

		// clear
		foreach( Transform child in entry_container )
		{
			Destroy( child.gameObject );
		}

		// create new entries
		foreach( ScoreSet entry_score in score.OrderByDescending( a => a.Score ) )
		{
			GameObject entry = Instantiate( entry_prefab );
			entry.transform.SetParent( entry_container , true );
			entry.transform.localScale = Vector3.one;
			entry.GetComponent<ScoreEntry>().Bind( entry_score );
		}
	}
}
