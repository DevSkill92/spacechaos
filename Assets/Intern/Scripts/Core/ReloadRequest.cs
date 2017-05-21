using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Requests a reload, only for internal use
/// </summary>
class ReloadRequest : RootComponent
{
	private Action callback;
	private bool triggered;

	/// <summary>
	/// Binds the callback
	/// </summary>
	/// <param name="callback"></param>
	public void Bind( Action callback )
	{
		this.callback = callback;
	}

	/// <summary>
	/// Force reload after all Update functions are complete
	/// </summary>
	private void LateUpdate()
	{
		if ( triggered  )
		{
			return;
		}
		triggered = true;

		callback();
	}
}