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
	private int length = 200;
	[ SerializeField]
	private float interval = 0.1f;
	[SerializeField]
	private float steer_cut = 0.01f;
	[SerializeField]
	private Transform container;
	[SerializeField]
	private Material material;
	[SerializeField]
	private int fade = 6;

	private List<Vector3> vertices = new List<Vector3>();
	private List<int> indieces = new List<int>();
	private List<Vector2> uv = new List<Vector2>();
	private Vector3[] normal = new Vector3[ 0 ];

	private float last_update;
	private int y_uv;
	private float last_steer;

	/// <summary>
	/// Generates a new sement
	/// </summary>
	public void create_track( Transform left_anchor , Transform right_anchor , float steer )
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
		if ( 4 <= vertices.Count )
		{
			vertices.Add( find_vertrex_position( left_anchor.transform.position , vertices[ vertices.Count - 2 ] , steer , last_steer , 1 ) );
			vertices.Add( find_vertrex_position( right_anchor.transform.position , vertices[ vertices.Count - 2 ] , steer , last_steer , -1 ) );
		}
		else
		{
			vertices.Add( left_anchor.transform.position );
			vertices.Add( right_anchor.transform.position );
		}

		last_steer = steer;


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
	/// Update track mesh
	/// </summary>
	/// <param name="left_anchor"></param>
	/// <param name="right_anchor"></param>
	public void UpdateTrack( Transform left_anchor , Transform right_anchor , Color color , float steer )
	{
		float time = Time.time;
		if ( time - interval > last_update )
		{
			last_update = Time.time;

			create_track( left_anchor , right_anchor , steer );
		}

		material.color = color;
		material.SetFloat( "_Fade" , ( ( time - last_update ) / interval ) / fade );
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

		if ( 2 < vertext_count )
		{
			int vertex_fade = Math.Min( fade , ( vertext_count / 2 ) - 2 ) * 2;

			// add end factor
			if ( length - 2 <= vertext_count )
			{
				for ( int i = 0 ; i < vertex_fade ; i += 2 )
				{
					normal[ i ].z = 1f - ((float)i / vertex_fade);
					normal[ i + 1 ].z = 1f - ( (float)i / vertex_fade );
					normal[ i ].x = -1;
					normal[ i + 1 ].x = -1;
				}
			}

			// add begin factor
			normal[ vertext_count - 1 ].z = 1;
			normal[ vertext_count - 2 ].z = 1;
			for ( int i = 3 ; i <= vertex_fade + 2 ; i += 2 )
			{
				normal[ vertext_count - i ].z = 1f - ( (float)(i-3) / vertex_fade );
				normal[ vertext_count - i - 1 ].z = 1f - ( (float)(i-3) / vertex_fade );
				normal[ vertext_count - i ].x = 1;
				normal[ vertext_count - i - 1 ].x = 1;
			}
		}

		return normal;
	}

	/// <summary>
	/// Find a nice new vertix postition without overlays
	/// </summary>
	/// <param name="current"></param>
	/// <param name="last"></param>
	private Vector3 find_vertrex_position( Vector3 current , Vector3 previous , float steer , float last_steer , float direction )
	{
		float distance = Vector3.Distance( current , previous );

		if (
			( steer * direction ) * steer_cut > distance
			|| ( last_steer * direction ) * steer_cut > distance
		)
		{
			return previous;
		}

		return current;
	}
}
