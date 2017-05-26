﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Extrem simple car
/// </summary>
public class SimpleCar : MonoBehaviour
{
	[SerializeField]
	private float acceleration = 90;
	[SerializeField]
	private float break_torque = 100;
	[SerializeField]
	private float damp = 60;
	[SerializeField]
	private float speed_max = 50;
	[SerializeField]
	public float speed_min = 0;
	[SerializeField]
	public float max_steer = 200;
	[SerializeField]
	private float speed_breacker_damp = 10f;
	[SerializeField]
	private float speed_breacker_factor = 0.5f;

	private float speed;
	private float steer;
	private float rotation;
	private int joy = 1;
	public float speed_breacker = 0;
	public float speed_breacker_max = 0;

	public float Steer
	{
		get
		{
			return steer;
		}
	}

	public float Speed
	{
		get
		{
			return speed;
		}
	}

	/// <summary>
	/// Binds the used input index
	/// </summary>
	/// <param name="joy"></param>
	public void BindJoyindex( int joy )
	{
		this.joy = joy;
	}

	/// <summary>
	/// Reset after respawn
	/// </summary>
	public void OnEnable()
	{
		ResetState();
	}

	/// <summary>
	/// Reset current car state
	/// </summary>
	public void ResetState()
	{
		speed = 0;
		steer = 0;
		rotation = transform.rotation.eulerAngles.y;
		speed_breacker = 0;
	}

	/// <summary>
	/// Sets the power of the car
	/// </summary>
	/// <param name="speed_max"></param>
	/// <param name="speed_min"></param>
	/// <param name="max_steer"></param>
	public void SetPower( float speed_max , float speed_min , float max_steer , float speed_breacker )
	{
		this.speed_max = speed_max;
		this.speed_min = speed_min;
		this.max_steer = max_steer;
		this.speed_breacker = speed_breacker;
		this.speed_breacker_max = speed_breacker;
	}

	/// <summary>
	/// Drive
	/// </summary>
	private void Update()
	{
		float input_acceleration = Input.GetAxis( "Vertical" + joy );
		float input_rotation = Input.GetAxis( "Horizontal" + joy );

		// apply input
		float current_acceleration = Time.deltaTime * ( 0 < input_acceleration ? acceleration : break_torque ) * input_acceleration;
		if ( speed + current_acceleration < speed_max * input_acceleration )
		{
			speed += current_acceleration;
		}
		
		// damp speed
		speed -= Time.deltaTime * damp * ( 1 - Mathf.Abs( input_acceleration ) );

		float current_speed_max = speed_max;
		if ( 0 < speed_breacker )
		{
			speed_breacker -= speed_breacker_damp * Time.deltaTime;
			current_speed_max = ( 1 - ( ( speed_breacker / speed_breacker_max ) * speed_breacker_factor ) ) * speed_max;
		}

		// cap speed
		speed = Mathf.Max( speed_min , Mathf.Min( current_speed_max , speed ) );

		rotation += steer = input_rotation * Time.deltaTime * max_steer * Mathf.Min( speed , 1 );
		transform.rotation = Quaternion.Euler( new Vector3( 0 , rotation , 0 ) );

		transform.position += transform.forward * speed * Time.deltaTime;
	}

}