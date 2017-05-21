using UnityEngine;

/// <summary>
/// Create random instances of given prefab
/// </summary>
class RandomInstance : MonoBehaviour
{
	[SerializeField]
	private Vector2 range = new Vector2( -5000 , 5000 );
	[SerializeField]
	private float height = 0;
	[SerializeField]
	private float random_height_factor = 0.1f;
	[SerializeField]
	private int amount = 1000;
	[SerializeField]
	private GameObject prefab;

	/// <summary>
	/// Create instances
	/// </summary>
	private void Awake()
	{
		for ( int i = 0 ; i < amount ; i++ )
		{
			Transform instance = Instantiate( prefab ).transform;
			instance.SetParent( transform );
			instance.localScale = Vector3.one;
			instance.transform.position = new Vector3(
				Random.Range( range.x , range.y ) ,
				0 != height ? height : Random.Range( range.x , 0 ) * random_height_factor,
				Random.Range( range.x , range.y )
			);
		}
	}
}