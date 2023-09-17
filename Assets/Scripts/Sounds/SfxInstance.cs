using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxInstance : MonoBehaviour
{

    public SfxHolder sonidos;
    public AudioSource instanciaSFX;
    public int currentSfx;
    // Start is called before the first frame update
    private void Start()
    {
        sonidos = this.GetComponent<SfxHolder>();
        instanciaSFX = this.GetComponent<AudioSource>();
    }

    public void ChoseMusic(int soundRequest)
    {
        Debug.Log("Se repoduce " + soundRequest);
        if (soundRequest != currentSfx)
        {
            currentSfx = soundRequest;
            soundRequest--;
            PlayMusic(sonidos.soundHolder[soundRequest].name);
        }


    }

    //play the song by name
    public void PlayMusic(string name)
    {

        foreach (Sound audio in sonidos.soundHolder)
        {
            if (audio.name == name)
            {
                instanciaSFX.clip = audio.clip;
                instanciaSFX.Play();
            }
            else
            {
                Debug.Log("Sound not found");
            }
        }

        
    }

}
