using UnityEngine;

/// <summary>
/// Z-Rotation by steer
/// </summary>
public class CarRolling : MonoBehaviour
{
	[SerializeField]
	private float rolling = 30;
	[SerializeField]
	private SimpleCar car = null;
	[SerializeField]
	private float speed = 10;

	private float roll;

	public void Update()
	{
		roll = Mathf.MoveTowards( roll , rolling * car.Steer , speed * Time.deltaTime );
		transform.localRotation = Quaternion.Euler( 0 , 0 , roll );
	}

}