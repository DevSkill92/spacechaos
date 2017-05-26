using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// Buttons for toggling
/// </summary>
public class ToggleButton : ButtonHandler
{
	[SerializeField]
	private Sprite background_on;
	[SerializeField]
	private Sprite background_off;
	[ SerializeField ]
	private bool default_value;

	private Image image;
	private Toggle toggle;
	private bool is_on;

	/// <summary>
	/// Gets / sets the current state
	/// </summary>
	public bool IsOn
	{
		get
		{
			return is_on;
		}
		set
		{
			is_on = value;
			show_state();
		}
	}

	/// <summary>
	/// Collects components
	/// </summary>
	private void Awake()
	{
		is_on = default_value;
		image = GetComponent<Image>();

		toggle = GetComponentInChildren<Toggle>();
		toggle.interactable = false;

		show_state();
	}

	/// <summary>
	/// Reset to default value on become enabled
	/// </summary>
	public void OnEnable()
	{
		is_on = default_value;
		show_state();
	}

	/// <summary>
	/// Toggle is on
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		IsOn = !is_on;
	}

	/// <summary>
	/// Show current state
	/// </summary>
	private void show_state()
	{
		image.sprite = is_on ? background_on : background_off;
		toggle.isOn = is_on;
	}
}
