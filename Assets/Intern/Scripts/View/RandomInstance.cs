using UnityEngine;

/// <summary>
/// Create random instances of given prefab
/// </summary>
class RandomInstance : MonoBehaviour
{
	[SerializeField]
	private Vector2 range = new Vector2( -5000 , 5000 );
	[SerializeField]
	private Vector2 range_height = new Vector2( -5000 , 5000 );
	[SerializeField]
	private float height = 0;
	[SerializeField]
	private int amount = 1000;
	[SerializeField]
	private GameObject prefab;
	[SerializeField]
	private bool merge_to_single = false;

	/// <summary>
	/// Create instances
	/// </summary>
	private void Awake()
	{
		Vector3[] position_list = new Vector3[ amount ];

		for ( int i = 0 ; i < amount ; i++ )
		{
			Vector3 position = new Vector3(
				Random.Range( range.x , range.y ) ,
				0 != height ? height : Random.Range( range_height.x , range_height.y ) ,
				Random.Range( range.x , range.y )
			);

			if ( merge_to_single )
			{
				position_list[ i ] = position;
			}
			else {
				Transform instance = Instantiate( prefab ).transform;
				instance.SetParent( transform , true );
				instance.localScale = prefab.transform.localScale;
				instance.transform.position = position;
			}
		}

		if ( merge_to_single )
		{
			gameObject.AddComponent<MergeQuad>().Create( position_list , prefab.transform.localScale , prefab.GetComponent<MeshRenderer>().sharedMaterial );
		}
	}
}