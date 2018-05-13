
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class settingMenu : MonoBehaviour {

    public bool volumeOn = true;
   
    public AudioMixer audioMixer;


    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        Debug.Log(volume);
    }

    

}
