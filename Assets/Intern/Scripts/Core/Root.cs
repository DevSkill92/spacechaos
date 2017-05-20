﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Root
{
	private static Root instance;
	public Dictionary<Type , RootComponent> component = new Dictionary<Type , RootComponent>();

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

	public T Get<T>() where T : RootComponent
	{
		Type type = typeof( T );
		if ( !component.ContainsKey( type ) )
		{
			UnityEngine.Object obj = GameObject.FindObjectOfType( type );
			component.Add( type , obj as T );
		}

		return component[ type ] as T;
	}
}