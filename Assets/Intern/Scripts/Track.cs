using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Intern.Scripts
{
	public class Track: MonoBehaviour
	{
		[SerializeField]
		private GameObject left_anchor;
		[SerializeField]
		private GameObject right_anchor;
		[SerializeField]
		private GameObject center;
		[SerializeField]
		private Material material;

		private List<Vector3> vertices = new List<Vector3>();
		private List<int> indieces = new List<int>();
		private List<Vector2> uv = new List<Vector2>();

		private float last_update;
		private int y_uv;

		private void update_track() {
			foreach( Transform child in transform )
			{
				DestroyImmediate( child.gameObject );
			}

			List<Vector3> vertices = this.vertices;

			if ( 100 < vertices.Count )
			{
				vertices.RemoveRange( 0 , 2 );
				uv.RemoveRange( 0 , 2 );
				//indieces.RemoveRange( 0 , 6 );
			}

			vertices.Add( left_anchor.transform.position );
			vertices.Add( right_anchor.transform.position );
			uv.Add( new Vector2( 0 , y_uv ) );
			uv.Add( new Vector2( 1 , y_uv ) );
			y_uv = 0 >= y_uv ? 1 : 0;

			if ( 4 > vertices.Count )
			{
				return;
			}

			if ( 150 > vertices.Count )
			{
				int count = vertices.Count;
				indieces.Add( count - 2 );
				indieces.Add( count - 4 );
				indieces.Add( count - 3 );
				indieces.Add( count - 2 );
				indieces.Add( count - 3 );
				indieces.Add( count - 1 );
			}

			GameObject obj = new GameObject();
			MeshFilter filter = obj.AddComponent<MeshFilter>();
			Mesh mesh = new Mesh();
			mesh.name = "track";

			mesh.vertices = vertices.ToArray();
			mesh.triangles = indieces.ToArray();
			mesh.uv = uv.ToArray();

			filter.mesh = mesh;

			obj.transform.SetParent( transform , true );
			obj.transform.localScale = Vector3.one;
			obj.transform.rotation = Quaternion.Euler( new Vector3( 0 , 0 , 0 ) );

			MeshRenderer renderer = obj.AddComponent<MeshRenderer>();
			renderer.material = material;

			this.vertices = vertices;
		}

		private void Update()
		{
			if ( Time.time - 0.2 > last_update )
			{
				last_update = Time.time;

				update_track();
			}

			material.SetVector( "_StartPosition" , ( left_anchor.transform.position + right_anchor.transform.position ) / 2 );
		}

	}
}
