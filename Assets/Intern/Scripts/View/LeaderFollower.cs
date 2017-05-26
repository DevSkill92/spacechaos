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
		PlayerManager manager = Root.I.Get<PlayerManager>();
		if ( null == manager )
		{
			return;
		}

		Transform target = null;

		if ( null != manager.Leader )
		{
			target = manager.Leader.transform;
		} else if ( manager.AICar.gameObject.activeInHierarchy )
		{
			target = manager.AICar.transform;
		}

		if ( null != target )
		{
			Vector3 position = target.position;
			position.y = height;
			transform.position = position;
		}
	}
}