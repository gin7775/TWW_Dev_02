using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    public GameObject healthBarCanvas;

    public void ActivateHealthBar()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(true);
    }

    public void DeactivateHealthBar()
    {
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(false);
    }
}
