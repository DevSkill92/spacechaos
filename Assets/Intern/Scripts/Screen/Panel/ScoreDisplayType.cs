using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// An abstract class for different socre set types
/// </summary>
public abstract class ScoreDisplayType : MonoBehaviour
{
	/// <summary>
	/// Gets the type which this component can handle
	/// </summary>
	public abstract ScoreSet.Type Type { get; }

	/// <summary>
	/// Binds the score
	/// </summary>
	/// <param name="score"></param>
	public abstract void Bind( ScoreSet score );
}
