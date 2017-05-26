using UnityEngine;
using System.Collections;

/// <summary>
/// Holds the config of the game
/// </summary>
public class GameConfig
{
	public bool Teamplay;
	public bool GamepadOnly;
	public bool AllowItem;
	public bool AllowRespawn;
	public bool Online;
	public int PlayerAmount;
	public string ModeName;

	/// <summary>
	/// Validate to given allowed config
	/// </summary>
	/// <param name="allowed"></param>
	public void Validate( GameConfig allowed )
	{
		Teamplay = allowed.Teamplay && 3 < PlayerAmount && Teamplay;
		GamepadOnly = allowed.GamepadOnly && GamepadOnly;
		AllowItem = allowed.AllowItem && AllowItem;
		AllowRespawn = allowed.AllowRespawn && AllowRespawn;
		Online = allowed.Online && Online;
		ModeName = allowed.ModeName;
	}
}
