using UnityEngine;
using System.Collections;

/// <summary>
/// Rotate around given axis
/// </summary>
public class Rotate : MonoBehaviour
{
	[SerializeField]
	private Vector3 speed;

	private Vector3 rotation;

	/// <summary>
	/// Store initial rotation
	/// </summary>
	private void Start()
	{
		rotation = transform.rotation.eulerAngles;
	}

	/// <summary>
	/// Add rotation
	/// </summary>
	private void Update()
	{
		rotation += speed * Time.deltaTime;
		transform.rotation = Quaternion.Euler( rotation );
	}
}