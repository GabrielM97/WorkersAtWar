using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A script to quit the game
public class QuitOnClick : MonoBehaviour 
{

	public void Quit()
	{
		//Some platform specific compilation stuff
		#if UNITY_EDITOR 
				UnityEditor.EditorApplication.isPlaying = false; //Exits Playmode
		#else
				Application.Quit();
		#endif
	}
}
