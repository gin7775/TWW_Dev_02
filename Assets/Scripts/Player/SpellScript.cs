using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellScript : MonoBehaviour
{
    public GameObject firePoint; // Punto desde donde se dispara el proyectil
    public float cooldownTime = 1.0f; // Tiempo de recarga entre disparos
    public GameObject[] proyectiles; // Array de proyectiles
    public GameObject[] effectSpawnProjectile; // Efectos visuales del disparo
    public GameObject crosshair; // GameObject para la cruceta
    public float crosshairRadius = 2.0f; // Radio del círculo alrededor del jugador
    public Camera mainCamera; // Cámara principal

    private bool isCooldown = false;
    private float cooldownTimer = 0f;
    private Vector2 aimDirection; // Dirección en la que apunta el joystick derecho
    private Player player;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

        if (crosshair != null)
        {
            crosshair.SetActive(true); // Aseguramos que la cruceta esté visible al inicio
        }
    }

    void Update()
    {
        ManageCooldown();
        UpdateCrosshair();
    }

    private void ManageCooldown()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }
    }

    public void OnAim(InputValue input)
    {
        aimDirection = input.Get<Vector2>(); // Obtiene la dirección del joystick derecho
        UpdateCrosshair();
    }

    private void UpdateCrosshair()
    {
        if (aimDirection.magnitude > 0.1f) // Solo actualizamos si el joystick está suficientemente desviado
        {
            Vector3 aimDirWorld = mainCamera.transform.right * aimDirection.x + mainCamera.transform.forward * aimDirection.y;
            aimDirWorld.y = 0; // Ignora el componente vertical para mantenerlo en el plano horizontal
            aimDirWorld.Normalize();

            crosshair.transform.position = transform.position + aimDirWorld * crosshairRadius;
        }
    }

    public void OnShoot(InputValue input)
    {
        if (input.isPressed && !isCooldown)
        {
            RotateTowardsAim(); // Rota el jugador hacia la dirección del lanzamiento
            StartCoroutine(FireProjectile());
        }
    }

    private void RotateTowardsAim()
    {
        Vector3 aimDir = (crosshair.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(aimDir);
        transform.rotation = targetRotation;
    }

    IEnumerator FireProjectile()
    {
        isCooldown = true;
        cooldownTimer = cooldownTime;

        yield return new WaitForSeconds(0.4f);
        if (firePoint != null)
        {
            Vector3 aimDir = (crosshair.transform.position - firePoint.transform.position).normalized;
            GameObject vfx = Instantiate(proyectiles[0], firePoint.transform.position, Quaternion.LookRotation(aimDir));
            GameObject effectSpawn = Instantiate(effectSpawnProjectile[0], firePoint.transform.position, Quaternion.LookRotation(aimDir));
            Destroy(vfx, 2);
            Destroy(effectSpawn, 2);
        }
    }
}
