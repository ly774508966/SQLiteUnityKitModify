using UnityEngine;
using System.Collections;

public class Singleton<T> where T : Singleton<T>, new()
{

	// Use this for initialization
	private static T shared = null;
	
	public static T Shared()
	{
		if(shared == null)
			shared = new T();
		return shared;
	}
}
