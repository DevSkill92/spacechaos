using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Timer for time based game modes
/// </summary>
public class RoundTimer : MonoBehaviour
{
	[SerializeField]
	private Text label;

	private float countdown;
	private float start;


	/// <summary>
	/// Start coundown
	/// </summary>
	/// <param name="countdown"></param>
	public void Bind( float countdown )
	{
		this.countdown = countdown;
		gameObject.SetActive( 0 < countdown );
		start = Time.time;
	}

	/// <summary>
	/// Check countdown
	/// </summary>
	private void Update()
	{
		if (
			0 < countdown
			&& Time.time - countdown > start
		)
		{
			Root.I.Get<ScreenManager>().Get<Game>().Exit();
		}
		else
		{
			float current = countdown - ( Time.time - start );
			int min = Mathf.FloorToInt( current / 60 );
			int sec = Mathf.FloorToInt( current ) - ( min * 60 );
			label.text = min.ToString( "D2" ) + ":" + sec.ToString( "D2" );
		}
	}
}