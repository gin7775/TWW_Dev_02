using Michsky.UI.Dark;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolveProyectile : MonoBehaviour
{
    public float transitionTime = 1f; // Duración de la transición en segundos
    private Material material;
    public event Action OnDissolveComplete;
    void Start()
    {
       
        Image uiImage = GetComponent<Image>();
        if (uiImage != null)
        {
            material = uiImage.material;
        }
    }

    public void StartDissolve()
    {

        StartCoroutine(DissolveTransition());
    }
    public void ResetDissolve()
    {
        if (material != null)
        {
            material.SetFloat("_FadeAmount", 0f);
        }
    }

    IEnumerator DissolveTransition()
    {
        float currentTime = 0f;

        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            float fadeValue = Mathf.Lerp(0f, 1f, currentTime / transitionTime);
            material.SetFloat("_FadeAmount", fadeValue);
          
            yield return null;
        }
        
        material.SetFloat("_FadeAmount", 1f); // Asegurarse de que el valor final es 1
        OnDissolveComplete?.Invoke();
    }
}
