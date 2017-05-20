using UnityEngine;

/// <summary>
/// Center object to leader position
/// </summary>
class LeaderFollower : MonoBehaviour
{
	private float height;

	/// <summary>
	/// Keep initial height
	/// </summary>
	private void Start()
	{
		height = transform.position.y;
	}

	/// <summary>
	/// Updates the position
	/// </summary>
	private void Update()
	{
		Player leader = Root.I.Get<ScreenManager>().Get<Game>().Leader;
		if ( null != leader )
		{
			Vector3 position = leader.transform.position;
			position.y = height;
			transform.position = position;
		}
	}
}