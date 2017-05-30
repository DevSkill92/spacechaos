using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Apply player color to renderer
/// </summary>
public class PlaySound : MonoBehaviour
{
	[SerializeField]
	private AudioClip sound;

	/// <summary>
	/// Hide in appear
	/// </summary>
	public void Play()
	{
		GameObject container = new GameObject();
		container.AddComponent<DestroyTimeout>().Bind( sound.length + 0.2f );
		AudioSource audio = container.AddComponent<AudioSource>();
		audio.clip = sound;
		container.transform.SetParent( Camera.main.transform , false );
		container.transform.position = Camera.main.transform.position;
		audio.Play();
	}
}