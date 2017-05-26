using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Merge multiple quads to one big mesh
/// </summary>
class Intro : MonoBehaviour
{
	[SerializeField]
	private Vector3 speed;

	private float start;

	private void Start()
	{
		start = Time.time;
	}

	private void Update()
	{
		float input_speed = 1;
		for( int i = 0 ; i <= 4 ; i++ )
		{
			input_speed += Input.GetAxis( "Vertical" + i ) * 2;
		}

		transform.position += speed * Time.deltaTime * input_speed;

		if ( 270 < transform.position.z )
		{
			SceneManager.LoadScene( "main" );
		}
	}
}