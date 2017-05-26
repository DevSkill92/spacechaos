using UnityEngine;

/// <summary>
/// Handles the volume of music sources
/// </summary>
public class MusicMixer : MonoBehaviour
{
	[SerializeField]
	private AudioSource idle;
	[SerializeField]
	private AudioSource capture;
	[SerializeField]
	private float transition_speed;

	/// <summary>
	/// Trigger transitions
	/// </summary>
	private void Update()
	{
		PlayerManager manager = Root.I.Get<PlayerManager>();
		if ( null == manager )
		{
			return;
		}

		Player leader = manager.Leader;

		if (
			null != leader
			&& leader.Capturing 
		)
		{
			transition( capture , idle );
		} else
		{

			transition( idle , capture );
		}
	}

	/// <summary>
	/// Handle transition
	/// </summary>
	/// <param name="active"></param>
	/// <param name="inactive"></param>
	private void transition( AudioSource active , AudioSource inactive )
	{
		if ( active.volume < 1 )
		{
			active.volume = Mathf.Min( 1 , active.volume + ( transition_speed * Time.deltaTime ) );
		}

		if ( inactive.volume > 0 )
		{
			inactive.volume = Mathf.Max( 0 , inactive.volume - ( transition_speed * Time.deltaTime ) );
		}
	}
}
