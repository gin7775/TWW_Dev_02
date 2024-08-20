using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersTutorial : MonoBehaviour
{
    public GameObject bubble; // Asigna el objeto del bocadillo aquí desde el inspector
    private Animator bubbleAnimator; // Para controlar la animación
    private bool isPlayerInside = false;
    private float timer = 0f;
    public  float timeToShowBubble = 5f; // Tiempo después del cual se muestra el bocadillo

    void Start()
    {
        bubble.SetActive(false); // Asegurarse de que el bocadillo esté oculto al inicio
        bubbleAnimator = bubble.GetComponent<Animator>(); // Obtener el componente Animator
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isPlayerInside = true;
            timer = 0f; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            StartCoroutine(PlayAnimationAndHideBubble()); 
        }
    }

    IEnumerator PlayAnimationAndHideBubble()
    {
        bubbleAnimator.SetTrigger("Salir"); 
        yield return new WaitForSeconds(0.3f); 
        bubble.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInside)
        {
            timer += Time.deltaTime;
            if (timer >= timeToShowBubble)
            {
                bubble.SetActive(true); 
            }
        }
    }
}
