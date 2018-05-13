using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Loads a scene when a button is clicked
public class LoadSceneOnClick : MonoBehaviour 
{

    /** The sceneIndex is being set in the button onClick() 
	 *  for example, Play btn asks for the Game scene that has index 1
	 */
    public int Current;
    public static int previousScene;
    public Scene currentScene;
    

	public void LoadByIndex(int sceneIndex)
	{
        previousScene = Current;

        SceneManager.LoadSceneAsync(sceneIndex);
       
	}

    public void returnTolastScene()
    {
        SceneManager.LoadScene(Current);
        
    }
}
