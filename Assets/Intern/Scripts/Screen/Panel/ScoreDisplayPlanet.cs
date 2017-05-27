using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Handle planet score
/// </summary>
public class ScoreDisplayPlanet : ScoreDisplayType
{
	[SerializeField]
	private GameObject prefab;
	[ SerializeField ]
	private Transform container;
	static int n = 0;

	/// <summary>
	/// Override type to handle planet
	/// </summary>
	public override ScoreSet.Type Type
	{
		get
		{
			return ScoreSet.Type.Planet;
		}
	}

	/// <summary>
	/// Instantiate prefab for amount of score
	/// </summary>
	/// <param name="score"></param>
	public override void Bind( ScoreSet score )
	{
		for ( int i = 0 ; i < score.Score ; i++ )
		{
			GameObject item = Instantiate( prefab );
			item.transform.SetParent( container , true );
			item.transform.localScale = Vector3.one;
		}
	}
}
