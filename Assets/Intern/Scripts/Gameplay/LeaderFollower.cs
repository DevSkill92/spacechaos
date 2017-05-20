using UnityEngine;

/// <summary>
/// Center object to leader position
/// </summary>
class LeaderFollower : MonoBehaviour
{
	/// <summary>
	/// Updates the position
	/// </summary>
	private void Update()
	{
		Player leader = Root.I.Get<ScreenManager>().Get<Game>().Leader;
		if ( null != leader )
		{
			transform.position = leader.transform.position;
		}
	}
}