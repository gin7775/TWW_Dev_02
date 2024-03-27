using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformEffect : MonoBehaviour
{
    public int stateFunction;
    public GameObject[] asignedPlataforms1;
    public GameObject[] asignedPlataforms2;
    public string[] sfxNames; // Array con los nombres de los SFX
    public bool switchState, colliderBlocker; // switchState controla el estado actual, colliderBlocker previene m�s activaciones
    public Renderer[] emissiveRenderers;
    void Start()
    {
        // Opcionalmente, puedes inicializar algo aqu�
    }

    void Update()
    {
        // Si necesitas chequear algo cada frame, har�as aqu�
    }

    public void TriggerPlataforms()
    {
        // Si el collider ya fue bloqueado, no hacer nada
        if (colliderBlocker) return;

        if (stateFunction == 1)
        {
            SwitchPlatforms();
        }
        else if (stateFunction == 2)
        {
            Active();
        }

        // Reproducir un SFX aleatorio si hay alguno asignado
        if (sfxNames.Length > 0)
        {
            int index = Random.Range(0, sfxNames.Length);
            MiFmod.Instance.Play(sfxNames[index]);
        }

        // Despu�s de la primera activaci�n, prevenir futuras activaciones
        colliderBlocker = true;

        // Apaga el material emisivo para los objetos asignados
        TurnOffEmissiveMaterial(1);
    }

    public void SwitchPlatforms()
    {
        if (!switchState)
        {
            foreach (GameObject platform in asignedPlataforms1)
            {
                platform.GetComponent<Animator>().SetTrigger("Active");
            }
            foreach (GameObject platform in asignedPlataforms2)
            {
                platform.GetComponent<Animator>().SetTrigger("Inactive");
            }
            switchState = true;
        }
        else
        {
            foreach (GameObject platform in asignedPlataforms1)
            {
                platform.GetComponent<Animator>().SetTrigger("Inactive");
            }
            foreach (GameObject platform in asignedPlataforms2)
            {
                platform.GetComponent<Animator>().SetTrigger("Active");
            }
            switchState = false;
        }
    }

    public void Active()
    {
        foreach (GameObject platform in asignedPlataforms1)
        {
            platform.GetComponent<Animator>().SetTrigger("Active");
        }
    }

    // M�todo para apagar el material emisivo
    public void TurnOffEmissiveMaterial(int materialIndex)
    {
        foreach (Renderer rend in emissiveRenderers)
        {
            if (rend != null && rend.materials.Length > materialIndex && rend.materials[materialIndex].HasProperty("_EmissionColor"))
            {
                Material[] mats = rend.materials; // Obt�n todos los materiales
                mats[materialIndex].SetColor("_EmissionColor", Color.red); // Modifica el color emisivo del material espec�fico
                rend.materials = mats; // Reasigna el array de materiales modificado al renderer
            }
        }
    }
    
}
