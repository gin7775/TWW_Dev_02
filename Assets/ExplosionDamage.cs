using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public int damage = 25;
    private Collider myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider>(); // Obtener el collider del VFX de explosión
        StartCoroutine(DisableColliderAfterDelay(0.1f)); // Desactivar el collider después de 0.1 segundos
    }

    private IEnumerator DisableColliderAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (myCollider != null)
        {
            myCollider.enabled = false; // Desactivar el collider
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats playerStats = other.GetComponent<PlayerStats>();

        if (playerStats != null)
        {
            playerStats.TakeDamage(damage);
            Debug.Log("Has tocado");
        }
    }
}
