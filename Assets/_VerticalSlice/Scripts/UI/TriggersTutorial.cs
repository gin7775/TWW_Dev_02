using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersTutorial : MonoBehaviour
{
    public GameObject bubble; // Asigna el objeto del bocadillo aquí desde el inspector
    private Animator bubbleAnimator; // Para controlar la animación
    private bool isPlayerInside = false;
    private float timer = 0f;
    private const float timeToShowBubble = 5f; // Tiempo después del cual se muestra el bocadillo

    void Start()
    {
        bubble.SetActive(false); // Asegurarse de que el bocadillo esté oculto al inicio
        bubbleAnimator = bubble.GetComponent<Animator>(); // Obtener el componente Animator
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que tu jugador tenga el tag "Player"
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
        bubbleAnimator.SetTrigger("Salir"); // Asume que tienes un trigger "Hide" en tu Animator para iniciar la animación de desaparecer
        yield return new WaitForSeconds(0.3f); // Asume que tu animación dura 1 segundo, ajusta este valor según sea necesario
        bubble.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside)
        {
            timer += Time.deltaTime;
            if (timer >= timeToShowBubble)
            {
                bubble.SetActive(true); // Mostrar el bocadillo después de 5 segundos
            }
        }
    }
}
