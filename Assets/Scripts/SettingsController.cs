using UnityEngine;
using UnityEngine.UI; // Importante para trabajar con UI
using FMOD.Studio;
using System.Text;
using System.Collections.Generic;
using FMODUnity;
using FMOD;

public class SettingsController : MonoBehaviour
{
    private Bus musicBus;
    private Bus sfxBus;
    private Bus masterBus;
    private Bus ambientBus;
    
    private void Awake()
    {
        
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/VFX");
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        ambientBus = FMODUnity.RuntimeManager.GetBus("bus:/Ambient");
    }
   

    public void ChangeMusicVolume(float volume)
    {
       
        musicBus.setVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
       
        sfxBus.setVolume(volume);
    }
    public void ChangeMasterVolume(float volume)
    {
        
        masterBus.setVolume(volume);
    }
    public void ChangeAmbientVolume(float volume)
    {
        
        ambientBus.setVolume(volume);
    }

   
}
