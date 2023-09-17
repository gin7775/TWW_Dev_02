using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public Sound[] musicSound, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private SoundManager instance;
    public int gameState = 0;
    public int currentSong = 0;
    public SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        //singletone
        gameState = 0;
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        if (instance!=null&&instance!=this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        musicSource = this.GetComponent<AudioSource>();

    }
    //Aqui se hara un metodo que cambie la musica automaticamente si es necesario(creo que no.) 

    public void ChoseMusic(int soundRequest)
    {
        Debug.Log("Se repoduce "+ soundRequest);
        if (soundRequest != currentSong)
        {
            currentSong = soundRequest;
            soundRequest--;
            PlayMusic(musicSound[soundRequest].name);
        }


    }

    //play the song by name
    public void PlayMusic(string name)
    {
        
        foreach(Sound audio in musicSound)
        {
            if(audio.name == name)
            {
                musicSource.clip = audio.clip;
                musicSource.Play();
            }
            else
            {
                Debug.Log("Sound not found");
            }
        }

      /*  if (s==null)
        {
            Debug.Log("Sound nor found");

        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }*/
    }
}
