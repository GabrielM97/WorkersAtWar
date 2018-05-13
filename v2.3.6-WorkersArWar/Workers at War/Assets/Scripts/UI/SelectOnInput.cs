using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour 
{

	//References
	public EventSystem eventSystem;
	public GameObject selectedObject; 

	private bool btnSelected;
	
	// Update is called once per frame
	void Update () 
	{
		//Vertical movement and selection detection
		if (Input.GetAxisRaw ("Vertical") != 0 && btnSelected == false) 
		{
			eventSystem.SetSelectedGameObject (selectedObject);
			btnSelected = true;
		}
	}

	//Deactivating the object
	private void OnDisable()
	{
		btnSelected = false;
	}
}