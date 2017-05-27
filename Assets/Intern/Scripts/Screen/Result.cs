using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the result screen
/// </summary>
public class Result : Screen {
	[SerializeField]
	private ScorePanel score_panel;

	private ScoreSet[] score;

	/// <summary>
	/// Show score on enter
	/// </summary>
	public override void Enter()
	{
		base.Enter();
	}

	/// <summary>
	/// Bind player score to show
	/// </summary>
	/// <param name="score"></param>
	public void BindScore( ScoreSet[] score )
	{
		this.score = score;
		score_panel.Show( score );
	}

}