using UnityEngine;
using UnityEngine.EventSystems;

class PlayerAmountButton : SwitchScreenButton
{
	[SerializeField]
	private int amount;
	[SerializeField]
	private bool allow_keyboard;

	/// <summary>
	/// Override to bind player amount
	/// </summary>
	/// <param name="data"></param>
	public override void HandleClick( BaseEventData data )
	{
		base.HandleClick( data );

		Screen target = Target;

		if (
			null != target
			&& target is Game
		)
		{
			(target as Game).BindPlayerAmount( amount , allow_keyboard );
		}
	}
}