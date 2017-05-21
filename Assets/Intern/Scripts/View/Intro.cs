using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Merge multiple quads to one big mesh
/// </summary>
class Intro : MonoBehaviour
{
	[SerializeField]
	private Vector3 speed;
	[SerializeField]
	private float timeout;

	private float start;

	private void Start()
	{
		start = Time.time;
	}

	private void Update()
	{
		transform.position += speed * Time.deltaTime;

		if ( Time.time - timeout > start )
		{
			SceneManager.LoadScene( "main" );
		}
	}
}