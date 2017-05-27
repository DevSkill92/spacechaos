using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Handle number score
/// </summary>
public class ScoreDisplayNumber : ScoreDisplayType
{
	[SerializeField]
	private Text target_text;

	/// <summary>
	/// Override type to handle number
	/// </summary>
	public override ScoreSet.Type Type
	{
		get
		{
			return ScoreSet.Type.Number;
		}
	}

	/// <summary>
	/// Puts the score as number to target text
	/// </summary>
	/// <param name="score"></param>
	public override void Bind( ScoreSet score )
	{
		target_text.text = score.Score.ToString();
	}
}
