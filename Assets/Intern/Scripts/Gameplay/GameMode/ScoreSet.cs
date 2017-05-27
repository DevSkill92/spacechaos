using UnityEngine;
using System.Collections;

/// <summary>
/// Contains a row of the highscore
/// </summary>
public class ScoreSet
{
	public enum Type { Number , Planet }
	public Player[] Player;
	public int Score;
	public Type DisplayType = Type.Number;
}
