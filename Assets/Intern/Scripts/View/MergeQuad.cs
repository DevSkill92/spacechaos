using UnityEngine;

/// <summary>
/// Merge multiple quads to one big mesh
/// </summary>
class MergeQuad : MonoBehaviour
{
	private int[] base_indieces = new int[] { 0 , 1 , 2 , 2 , 1 , 3 };
	private Vector3[] base_vertices = new Vector3[]
	{
		new Vector3( -0.5f , 0 , -0.5f ),
		new Vector3( 0.5f , 0 , -0.5f ),
		new Vector3( -0.5f , 0 , 0.5f ),
		new Vector3( 0.5f , 0 , 0.5f )
	};
	private Vector2[] base_uv = new Vector2[]
	{
		new Vector2( 0 , 0 ),
		new Vector2( 1 , 0 ),
		new Vector2( 0 , 1 ),
		new Vector2( 1 , 1 )
	};

	/// <summary>
	/// Create the mesh
	/// </summary>
	/// <param name="position"></param>
	/// <param name="scale"></param>
	/// <param name="material"></param>
	public void Create( Vector3[] position , Vector3 scale , Material material )
	{
		Vector3[] vertices = new Vector3[ base_vertices.Length * position.Length ];
		Vector2[] uv = new Vector2[ base_uv.Length * position.Length ];

		int[] indieces = new int[ base_indieces.Length * position.Length ];

		int n = 0;
		int n_i = 0;
		for ( int s = 0 ; s < position.Length ; s++ )
		{
			for ( int i = 0 ; i < base_indieces.Length ; i++ )
			{
				indieces[ n_i ] = base_indieces[ i ] + n;
				n_i++;
			}

			for ( int v = 0 ; v < base_vertices.Length ; v++ )
			{
				vertices[ n ] = new Vector3(
						base_vertices[ v ].x * scale.x ,
						base_vertices[ v ].y * scale.z ,
						base_vertices[ v ].z * scale.y
					)
					+ position[ s ];
				uv[ n ] = base_uv[ v ];
				n++;
			}
			
		}

		GameObject obj = new GameObject();
		MeshFilter filter = obj.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		mesh.name = "merged_quad";

		mesh.vertices = vertices;
		mesh.triangles = indieces;
		mesh.uv = uv;
		
		mesh.RecalculateBounds();
		filter.mesh = mesh;

		obj.transform.SetParent( transform , true );
		obj.transform.localScale = Vector3.one;
		obj.transform.rotation = Quaternion.Euler( new Vector3( 0 , 0 , 0 ) );

		MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
		renderer.material = material;
		renderer.receiveShadows = false;
		renderer.lightProbeUsage =  UnityEngine.Rendering.LightProbeUsage.Off;
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

	}
}