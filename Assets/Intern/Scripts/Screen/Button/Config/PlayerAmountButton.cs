using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Sets the player amount
/// </summary>
class PlayerAmountButton : ToggleButton
{
	[SerializeField]
	private int amount;
	[SerializeField]
	private ToggleTeamplay teamplay;

	/// <summary>
	/// Override set player amount
	/// Disables other buttons
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		foreach( ToggleButton button in transform.parent.GetComponentsInChildren<ToggleButton>() )
		{
			button.IsOn = false;
		}

		IsOn = true;
		Root.I.Get<ScreenManager>().Get<Title>().CurrentConfig.PlayerAmount = amount;
		
		teamplay.gameObject.SetActive( 3 < amount );
	}
}