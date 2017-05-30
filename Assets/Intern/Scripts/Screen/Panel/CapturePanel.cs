using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

/// <summary>
/// A UI Panel which shows the capture progress
/// </summary>
public class CapturePanel : RootGameComponent
{
	[SerializeField]
	private Image progress;
	[SerializeField]
	private Image flicker;
	[SerializeField]
	private float flicker_time_scale;

	private float last_show;

	/// <summary>
	/// Shows the panel with new color / progress
	/// </summary>
	/// <param name="color"></param>
	/// <param name="progress"></param>
	public void Show( Color color , float progress )
	{
		gameObject.SetActive( true );
		last_show = Time.time;

		this.progress.fillAmount = progress;
		this.progress.color = color;
	}

	/// <summary>
	/// Hide and flicker
	/// </summary>
	private void Update()
	{
		float time = Time.time;

		if ( time - 0.1f > last_show )
		{
			gameObject.SetActive( false );
			return;
		}

		flicker.gameObject.SetActive( Mathf.Sin( time * flicker_time_scale ) >= 0 );
	}
}
