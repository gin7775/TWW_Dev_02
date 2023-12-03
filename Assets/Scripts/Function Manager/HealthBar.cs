using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Sprite[] maskSprites; // Im�genes de la m�scara en diferentes estados
    private Image maskImage;

    void Start()
    {
        maskImage = GetComponent<Image>();
    }

    public void UpdateMaskImage(int currentHealth)
    {
       
        if (currentHealth == 100)
        {
            maskImage.sprite = maskSprites[0]; // M�scara completa
        }
        else if (currentHealth < 100 && currentHealth >= 75)
        {
            maskImage.sprite = maskSprites[1]; // M�scara con algunos da�os
        }
        else if (currentHealth < 75 && currentHealth >= 50)
        {
            maskImage.sprite = maskSprites[2]; // M�scara m�s da�ada
        }
        else if (currentHealth < 50 && currentHealth >= 25)
        {
            maskImage.sprite = maskSprites[3]; // M�scara a�n m�s da�ada
        }
        else if (currentHealth < 25 && currentHealth > 0)
        {
            maskImage.sprite = maskSprites[4]; // M�scara casi rota
        }
    }

    public void BreakMask()
    {
        
        maskImage.sprite = maskSprites[maskSprites.Length - 1];
    }

}
