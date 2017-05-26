using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Root
{
	private static Root instance;
	public Dictionary<Type , IRootObject> component = new Dictionary<Type , IRootObject>();

	public static Root I
	{
		get
		{
			if ( instance == null )
			{
				instance = new Root();
			}
			return instance;
		}
	}

	private Root()
	{
	}

	/// <summary>
	/// Reload the entire game
	/// </summary>
	public void Reload()
	{
		new GameObject().AddComponent<ReloadRequest>().Bind( () =>
		{
			instance = null;
			int scene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene( scene );
		});
	}

	/// <summary>
	/// Gets or creates a root component
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	public T Get<T>() where T : IRootObject , new()
	{
		Type type = typeof( T );
		if ( !component.ContainsKey( type ) )
		{
			if ( type.IsSubclassOf( typeof( RootComponent ) ) )
			{
				UnityEngine.Object obj = GameObject.FindObjectOfType( type );
				if ( null != obj )
				{
					component.Add( type , obj as IRootObject );
				}

				return (T)( obj as IRootObject );
			}
			else
			{
				component[ type ] = new T();
			}
		}
		
		return (T)component[ type ];
	}
}