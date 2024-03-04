using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersTutorial : MonoBehaviour
{
    public GameObject bubble; // Asigna el objeto del bocadillo aqu� desde el inspector
    private Animator bubbleAnimator; // Para controlar la animaci�n
    private bool isPlayerInside = false;
    private float timer = 0f;
    private const float timeToShowBubble = 5f; // Tiempo despu�s del cual se muestra el bocadillo

    void Start()
    {
        bubble.SetActive(false); // Asegurarse de que el bocadillo est� oculto al inicio
        bubbleAnimator = bubble.GetComponent<Animator>(); // Obtener el componente Animator
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Aseg�rate de que tu jugador tenga el tag "Player"
        {
            isPlayerInside = true;
            timer = 0f; // Reiniciar el temporizador
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            StartCoroutine(PlayAnimationAndHideBubble()); // Iniciar la corrutina para animar y ocultar el bocadillo
        }
    }

    IEnumerator PlayAnimationAndHideBubble()
    {
        bubbleAnimator.SetTrigger("Salir"); // Asume que tienes un trigger "Hide" en tu Animator para iniciar la animaci�n de desaparecer
        yield return new WaitForSeconds(0.3f); // Asume que tu animaci�n dura 1 segundo, ajusta este valor seg�n sea necesario
        bubble.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside)
        {
            timer += Time.deltaTime;
            if (timer >= timeToShowBubble)
            {
                bubble.SetActive(true); // Mostrar el bocadillo despu�s de 5 segundos
            }
        }
    }
}
