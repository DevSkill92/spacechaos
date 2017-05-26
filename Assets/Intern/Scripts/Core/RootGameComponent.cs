using UnityEngine;
using System.Collections;

/// <summary>
/// An abstract class for rootcomponents of a game (usualy game managers)
/// </summary>
public abstract class RootGameComponent : RootComponent
{
	/// <summary>
	/// Called on enter the game
	/// </summary>
	public virtual void Enter()
	{

	}

	/// <summary>
	/// Called on leave the game
	/// </summary>
	public virtual void Leave()
	{

	}
}
