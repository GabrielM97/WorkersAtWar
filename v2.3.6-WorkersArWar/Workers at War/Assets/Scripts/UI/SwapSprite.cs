using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SwapSprite : MonoBehaviour {

    settingMenu menu;

    private Button button;
    public Sprite musicOn;
    public Sprite musicOff;
    private bool volumeOn;
    public AudioMixer audioMixer;

    private void Start()
    {
        volumeOn = true;
    }

    // Update is called once per frame
    void Update () {
		
        if(volumeOn)
        {
            gameObject.GetComponent<Image>().sprite = musicOn;
        }else
        {
            gameObject.GetComponent<Image>().sprite = musicOff;
        }
	}

    public void toggleVolume()
    {

        if (volumeOn)
        {
            audioMixer.SetFloat("Volume", -80);
            volumeOn = false;
            Debug.Log("click");

        }
        else
        {
            audioMixer.SetFloat("Volume", 0);
            volumeOn = true;
        }
    }
}
