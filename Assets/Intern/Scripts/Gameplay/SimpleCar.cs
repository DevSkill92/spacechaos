using UnityEngine;
using System.Collections;

//CarController1.js


public class SimpleCar : MonoBehaviour
{

	public float enginePower = 150.0f;
	public float power = 0.0f;
	public float brake = 0.0f;
	public float steer = 0.0f;
	public float maxSteer = 25.0f;
	public WheelCollider FrontLeft;
	public WheelCollider FrontRight;
	public WheelCollider RearLeft;
	public WheelCollider RearRight;
	public Rigidbody rigidbody;

	void Start()
	{
		rigidbody.centerOfMass = new Vector3( 0f , -0.5f , 0.3f );
	}

	void Update()
	{
		power = Input.GetAxis( "Vertical" ) * enginePower * Time.deltaTime * 250.0f;
		steer = Input.GetAxis( "Horizontal" ) * maxSteer;
		brake = power > 0 ? 0 : 5;

		
		FrontLeft.steerAngle = steer;
		FrontRight.steerAngle = steer;

		FrontLeft.brakeTorque = brake;
		FrontRight.brakeTorque = brake;
		RearLeft.brakeTorque = brake;
		RearRight.brakeTorque = brake;
		RearLeft.motorTorque = power;
		RearRight.motorTorque = power;
	}

}