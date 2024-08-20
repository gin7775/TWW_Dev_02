using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiFmod : MonoBehaviour
{
    public string nombrePath;
    public bool ffff = false;
    public bool ffff2 = false;

    //instance
    public static MiFmod Instance;

    private string currentStringFond;
    private FMOD.Studio.EventInstance PF;
    private GameObject Player;
    // Start is called before the first frame update

    private void Awake()
    {
        //Instance = this;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
    }
    private GameObject GetPlayer()
    {
        return GameObject.FindWithTag("Player"); // Asegúrate de que el jugador tiene esta etiqueta
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GetPlayer();
        if (player != null)
        {
            FMODUnity.RuntimeManager.SetListenerLocation(player);

            // Resto de tu lógica de Update...
        }
        if (ffff == true)
        {
            PlayFondo(nombrePath);
            ffff = false;
        }
        if (ffff2 == true)
        {
            Play(nombrePath);
            ffff2 = false;
        }
    }

    public void PlayFondo(string path)
    {
        if (path != currentStringFond)
        {
            StopFondo();
            currentStringFond = path;
            PF = FMODUnity.RuntimeManager.CreateInstance("event:/" + path);
            PF.start();
            //FMODUnity.RuntimeManager.PlayOneShot("event:/sountrack/" + path);
        }
    }

    public void StopFondo()
    {
        PF.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        currentStringFond = "";
    }

    public void Play(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/" + path, Camera.main.transform.position);
    }
    public void Play3D(string path, GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/" + path, go);
    }
    public void Play3DFondo(string path, Transform ta, Rigidbody ri)
    {
        PF = FMODUnity.RuntimeManager.CreateInstance("event:/" + path);
        PF.start();
    }
    public void Play3DFondoUpdate(GameObject go)
    {
        PF.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(go));
    }
}