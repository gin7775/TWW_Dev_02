using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSonidos : MonoBehaviour
{
    ActiveMusicBoss activeMusicBoss;
    // Lista de pistas de música de fondo
    private List<string> pistasDeFondo = new List<string>
    {
        "SoundTrack/MusicaFondo",
       

    };

    private void Awake()
    {
        // Aquí puedes añadir cualquier lógica que necesites en Awake
    }

    public void Start()
    {
        
        ReproducirBossMusic();
        ReproducirPistaAleatoria();
    }


    private void ReproducirPistaAleatoria()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            int indiceAleatorio = Random.Range(0, pistasDeFondo.Count);
            string pistaSeleccionada = pistasDeFondo[indiceAleatorio];

            MiFmod.Instance.PlayFondo(pistaSeleccionada);
        }
          
    }

    void ReproducirBossMusic()
    {
       
        if ( SceneManager.GetActiveScene().buildIndex == 5 )
        {
            MiFmod.Instance.PlayFondo("SoundTrack/MusicaFondo2");

        }
    }

    
}
