using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SpellScript : MonoBehaviour
{
    [Header("Configuraciones de Disparo")]
    public GameObject firePoint; // Punto desde donde se dispara el proyectil
    public float cooldownTime = 1.0f; // Tiempo de recarga entre disparos
    public GameObject[] proyectiles; // Array de proyectiles
    public GameObject[] effectSpawnProjectile; // Efectos visuales del disparo

    [Header("Configuraciones de la Cruceta")]
    public GameObject crosshair; // GameObject para la cruceta
    public float crosshairRadius = 2.0f; // Radio del c�rculo alrededor del jugador
    public Camera mainCamera; // C�mara principal

    private bool isCooldown = false;
    private float cooldownTimer = 0f;
    private Vector2 aimDirection; // Direcci�n en la que apunta el joystick derecho
    private Vector3 lastAimDirection; // �ltima direcci�n calculada para la cruceta
    private Player player;
    private Animator animator;
    private PlayerStats playerStats;
    public float rotation1;
    private bool isAimingWithMouse = false; // Para saber si se est� apuntando con el mouse
    private bool isRightMouseHeld = false; // Para saber si se mantiene presionado el bot�n derecho del mouse
    private bool isAimingWithJoystick = false; // Para saber si se est� apuntando con el joystick

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        if (crosshair != null)
        {
            crosshair.SetActive(false); // Cruceta oculta al inicio
            crosshair.transform.rotation = Quaternion.Euler(90, 0, 0); // Establece la rotaci�n fija inicial de la cruceta (mirando hacia adelante)
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
        aimDirection = input.Get<Vector2>(); // Obtiene la direcci�n del joystick derecho

        if (aimDirection.magnitude > 0.1f) // Si el joystick est� suficientemente desviado
        {
            isAimingWithMouse = false; // Desactivar la entrada del mouse si se usa el joystick
            isAimingWithJoystick = true;

            // Convertir la direcci�n del joystick a un vector en el espacio del mundo
            lastAimDirection = mainCamera.transform.right * aimDirection.x + mainCamera.transform.forward * aimDirection.y;
            lastAimDirection.y = 0; // Mantener la cruceta en el plano horizontal
            lastAimDirection.Normalize(); // Normalizar para obtener solo la direcci�n

            if (crosshair != null && !crosshair.activeSelf)
            {
                crosshair.SetActive(true); // Mostrar la cruceta
            }
        }
        else
        {
            lastAimDirection = Vector3.zero; // Reseteamos la direcci�n cuando no se est� moviendo el joystick
            isAimingWithJoystick = false;

            if (crosshair != null && crosshair.activeSelf && !isAimingWithMouse)
            {
                crosshair.SetActive(false); // Ocultar la cruceta si no se est� usando el mouse
            }
        }
    }

    public void OnMouseAim(InputValue input)
    {
        if (input.isPressed)
        {
            
            isAimingWithMouse = true;
            isRightMouseHeld = true;
            isAimingWithJoystick = false; 

            
            if (crosshair != null && !crosshair.activeSelf)
            {
                crosshair.SetActive(true);
            }

            UpdateCrosshair(); 
        }
        else
        {
         
            if (isRightMouseHeld)
            {
               
                StartCoroutine(FireProjectile());

                // Desactivar la cruceta
                if (crosshair != null && crosshair.activeSelf)
                {
                    crosshair.SetActive(false);
                }

                isAimingWithMouse = false;
                isRightMouseHeld = false; 
            }
        }
    }

    public void OnRotate(InputValue input)
    {
        if (input.isPressed && !isCooldown)
        {
            StartCoroutine(FireProjectile()); // Disparar cuando se presiona el bot�n de disparo en el mando
        }
    }

    private void UpdateCrosshair()
    {
        if (crosshair == null || mainCamera == null)
            return;

        if (lastAimDirection != Vector3.zero || isAimingWithMouse) // Actualizar si la direcci�n es v�lida o se usa el mouse
        {
            Vector3 newCrosshairPosition;

            if (isAimingWithMouse)
            {
                // Obtener la posici�n del mouse en un plano horizontal a la altura del jugador
                Plane playerPlane = new Plane(Vector3.up, transform.position);
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (playerPlane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);

                    // Calcular la direcci�n desde el jugador hacia la posici�n del mouse
                    Vector3 directionFromPlayerToMouse = (hitPoint - transform.position).normalized;
                    directionFromPlayerToMouse.y = 0; // Mantener la cruceta en el plano horizontal

                    // Calcular la nueva posici�n de la cruceta en un c�rculo alrededor del jugador
                    newCrosshairPosition = transform.position + directionFromPlayerToMouse * crosshairRadius;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // Calcular la nueva posici�n de la cruceta en un c�rculo alrededor del jugador
                newCrosshairPosition = transform.position + lastAimDirection * crosshairRadius;
                newCrosshairPosition.y = transform.position.y; // Mantener la cruceta al nivel del jugador
            }

            // Actualizar la posici�n de la cruceta
            crosshair.transform.position = newCrosshairPosition;

            // Rotar la cruceta para que mire hacia la direcci�n que apunta, incluyendo la direcci�n vertical
            Quaternion lookRotation = Quaternion.LookRotation(lastAimDirection != Vector3.zero ? lastAimDirection : crosshair.transform.position - transform.position);

            // Modificar solo la rotaci�n en el eje X para mantenerla en 90 grados
            lookRotation = Quaternion.Euler(90, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z + rotation1);

            // Aplicar la rotaci�n final a la cruceta
            crosshair.transform.rotation = lookRotation;
        }
    }

    IEnumerator FireProjectile()
    {
        if (isCooldown)
            yield break;  // Si todav�a est� en cooldown, no disparar

        isCooldown = true;  // Iniciar el cooldown
        cooldownTimer = cooldownTime;  // Reiniciar el temporizador del cooldown

        animator.SetTrigger("Spell");

        playerStats.canMove = false;

        // 1. Rotar el jugador hacia la direcci�n de la cruceta
        Vector3 targetPosition = crosshair.transform.position;
        targetPosition.y = transform.position.y; // Mantener al jugador en su altura actual
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(aimDir);

        // 2. Esperar un momento antes de disparar 
        yield return new WaitForSeconds(0.2f);

        // 3. Lanzar el proyectil
        if (firePoint != null)
        {
            // Calcular la direcci�n del proyectil hacia la cruceta pero manteniendo la altura del firePoint
            Vector3 projectileDirection = (new Vector3(crosshair.transform.position.x, firePoint.transform.position.y, crosshair.transform.position.z) - firePoint.transform.position).normalized;

            // Crear el proyectil en la posici�n del firePoint y rotarlo hacia la direcci�n calculada
            GameObject vfx = Instantiate(proyectiles[0], firePoint.transform.position, Quaternion.LookRotation(projectileDirection));

            Destroy(vfx, 2); // Destruir el proyectil despu�s de 2 segundos
        }

        playerStats.canMove = true;

        
    }
}
