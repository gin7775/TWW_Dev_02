using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Sprite[] maskSprites; // Imágenes de la máscara en diferentes estados
    private Image maskImage;

    void Start()
    {
        maskImage = GetComponent<Image>();
    }

    public void UpdateMaskImage(int currentHealth)
    {
       
        if (currentHealth == 100)
        {
            maskImage.sprite = maskSprites[0]; // Máscara completa
        }
        else if (currentHealth < 100 && currentHealth >= 75)
        {
            maskImage.sprite = maskSprites[1]; // Máscara con algunos daños
        }
        else if (currentHealth < 75 && currentHealth >= 50)
        {
            maskImage.sprite = maskSprites[2]; // Máscara más dañada
        }
        else if (currentHealth < 50 && currentHealth >= 25)
        {
            maskImage.sprite = maskSprites[3]; // Máscara aún más dañada
        }
        else if (currentHealth < 25 && currentHealth > 0)
        {
            maskImage.sprite = maskSprites[4]; // Máscara casi rota
        }
    }

    public void BreakMask()
    {
        
        maskImage.sprite = maskSprites[maskSprites.Length - 1];
    }

}
