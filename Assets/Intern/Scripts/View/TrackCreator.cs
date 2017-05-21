using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Track creator
/// </summary>
public class TrackCreator : MonoBehaviour
{
	[SerializeField]
	private int length = 100;
	[SerializeField]
	private Transform container;
	[SerializeField]
	private Material material;

	private List<Vector3> vertices = new List<Vector3>();
	private List<int> indieces = new List<int>();
	private List<Vector2> uv = new List<Vector2>();
	private Vector3[] normal = new Vector3[ 0 ];

	private float last_update;
	private int y_uv;

	/// <summary>
	/// Generates a new sement
	/// </summary>
	public void create_track( Transform left_anchor , Transform right_anchor )
	{
		foreach( Transform child in container )
		{
			DestroyImmediate( child.gameObject );
		}

		List<Vector3> vertices = this.vertices;

		if ( length < vertices.Count )
		{
			vertices.RemoveRange( 0 , 2 );
			uv.RemoveRange( 0 , 2 );
		}

		// appendd vertices
		vertices.Add( left_anchor.transform.position );
		vertices.Add( right_anchor.transform.position );
		Debug.Log( left_anchor.transform.position );
		int count = vertices.Count;

		// append uvs
		uv.Add( new Vector2( 0 , y_uv ) );
		uv.Add( new Vector2( 1 , y_uv ) );
		y_uv = 0 >= y_uv ? 1 : 0;

		if ( 4 > vertices.Count )
		{
			return;
		}

		if ( length >= vertices.Count )
		{
			// appand indieces
			indieces.Add( count - 2 );
			indieces.Add( count - 4 );
			indieces.Add( count - 3 );
			indieces.Add( count - 2 );
			indieces.Add( count - 3 );
			indieces.Add( count - 1 );
		}

		// recalculate normals
		if ( normal.Length != count )
		{
			normal = generate_normal( count );
		}

		GameObject obj = new GameObject();
		MeshFilter filter = obj.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		mesh.name = "track";

		mesh.vertices = vertices.ToArray();
		mesh.triangles = indieces.ToArray();
		mesh.uv = uv.ToArray();
		mesh.normals = normal;

		filter.mesh = mesh;

		obj.transform.SetParent( container , true );
		obj.transform.localScale = Vector3.one;
		obj.transform.rotation = Quaternion.Euler( new Vector3( 0 , 0 , 0 ) );

		MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
		renderer.material = material;
		renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
		renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

		this.vertices = vertices;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="left_anchor"></param>
	/// <param name="right_anchor"></param>
	public void UpdateTrack( Transform left_anchor , Transform right_anchor , Color color )
	{
		float time = Time.time;
		if ( time - 0.2 > last_update )
		{
			last_update = Time.time;

			create_track( left_anchor , right_anchor );
		}

		material.color = color;
		material.SetFloat( "_Progress" , ( ( time - last_update ) / 0.2f ) );
	}

	/// <summary>
	/// Regenerate normals for given vertex count
	/// </summary>
	/// <param name="vertext_count"></param>
	/// <returns></returns>
	private Vector3[] generate_normal( int vertext_count )
	{
		Vector3[] normal = new Vector3[ vertext_count ];
		for ( int i = 0 ; i < vertext_count ; i++ )
		{
			normal[ i ] = Vector3.up;
		}

		if ( 6 < vertext_count )
		{
			// add end factor
			float factor = 0.6f;
			for ( int i = 0 ; i < 6 ; i += 2 )
			{
				normal[ i ].z = factor;
				normal[ i + 1 ].z = factor;
				factor -= 0.2f;
			}

			// add begin factor
			factor = 0.6f;
			for ( int i = 1 ; i <= 6 ; i += 2 )
			{
				normal[ vertext_count - i ].z = factor;
				normal[ vertext_count - i - 1 ].z = factor;
				normal[ vertext_count - i ].x = 1;
				normal[ vertext_count - i - 1 ].x = 1;
				factor -= 0.2f;
			}
		}

		return normal;
	}
}
