using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Handle player game logic
/// </summary>
public class Player : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer body;
	[SerializeField]
	private Material[] skin;
	[SerializeField]
	private Material[] skin_leader;
	[SerializeField]
	private CarUserControl control;
	[SerializeField]
	private CaptureLine capture_line;
	[SerializeField]
	private UnityEvent follow = new UnityEvent();
	[SerializeField]
	private UnityEvent lead = new UnityEvent();

	private int joy;
	private int index;

	/// <summary>
	/// Gets the index of the player
	/// </summary>
	public int Index
	{
		get
		{
			return index;
		}
	}

	/// <summary>
	/// Bind user index
	/// </summary>
	/// <param name="joy"></param>
	/// <param name="index"></param>
	public void BindIndex( int joy , int index )
	{
		this.joy = joy;
		this.index = index;
	}

	/// <summary>
	/// Initialize user skin
	/// </summary>
	private void Start()
	{
		Follower();
		control.BindJoyindex( joy );
	}


	/// <summary>
	/// Switch to loader states
	/// </summary>
	public void Leader()
	{
		lead.Invoke();
		body.material = skin_leader[ index ];
	}

	/// <summary>
	/// Switch to follower state
	/// </summary>
	public void Follower()
	{
		body.material = skin[ index ];
		follow.Invoke();
	}

	/// <summary>
	/// Keep last trigger time
	/// </summary>
	/// <param name="collider"></param>
	private void OnTriggerStay( Collider collider )
	{
		if ( collider.gameObject.layer == LayerMask.NameToLayer( "trigger_leader" ) )
		{
			( Root.I.Get<ScreenManager>().Active as Game ).RequestLeaderSwitch( this );
			return;
		}

		Planet planet = collider.GetComponent<Planet>();
		if ( null != planet )
		{
			planet.Capture( this );
			capture_line.Show( planet );
		}
	}
}