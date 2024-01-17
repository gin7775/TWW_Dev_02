using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonidos : MonoBehaviour
{
    // Lista de pistas de m�sica de fondo
    private List<string> pistasDeFondo = new List<string>
    {
        "SoundTrack/MusicaFondo",
       

    };

    private void Awake()
    {
        // Aqu� puedes a�adir cualquier l�gica que necesites en Awake
    }

    public void Start()
    {
        // Reproducir una pista aleatoria al inicio
        ReproducirPistaAleatoria();
    }

    private void ReproducirPistaAleatoria()
    {
        int indiceAleatorio = Random.Range(0, pistasDeFondo.Count);
        string pistaSeleccionada = pistasDeFondo[indiceAleatorio];

        MiFmod.Instance.PlayFondo(pistaSeleccionada);
    }
}
