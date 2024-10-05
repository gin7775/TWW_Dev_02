using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class SpellScript : MonoBehaviour
{
    [Header("Configuraciones de Disparo")]
    public GameObject firePoint;
    public GameObject[] proyectiles;
    public GameObject[] effectSpawnProjectile;

    private int currentProjectileIndex = 0;

    [Header("Configuraciones de la Cruceta")]
    public GameObject crosshair; // GameObject para la cruceta
    public float crosshairRadius = 2.0f; // Radio del círculo alrededor del jugador
    public Camera mainCamera; // Cámara principal

    public bool isCooldown = false;
    private float cooldownTimer = 0f;
    private Vector2 aimDirection; // Dirección en la que apunta el joystick derecho
    private Vector3 lastAimDirection; // Última dirección calculada para la cruceta
    private Player player;
    private Animator animator;
    private PlayerStats playerStats;
    public float rotation1;
    private bool isAimingWithMouse = false; // Para saber si se está apuntando con el mouse
    private bool isRightMouseHeld = false; // Para saber si se mantiene presionado el botón derecho del mouse
    private bool isAimingWithJoystick = false; // Para saber si se está apuntando con el joystick
    private GameObject selectedProjectile;
    ComboAttackSystem comboAttackSystem;

    [Header("Cooldown de Proyectiles")]
    public float[] projectileCooldownTimes; // Cooldown específico para cada proyectil
    public float[] projectileCooldownTimers; // El temporizador que cuenta el tiempo restante para cada proyectil
    private bool[] isProjectileOnCooldown;
    public TextMeshProUGUI cooldownIndicator;

    WeaponWheelController wheelController;
    void Start()
    {
        comboAttackSystem = GetComponent<ComboAttackSystem>();
        selectedProjectile = proyectiles[0];
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        wheelController = GameObject.Find("WeaponWheel").GetComponent<WeaponWheelController>();
        int projectileCount = proyectiles.Length;
        isProjectileOnCooldown = new bool[projectileCount];
        projectileCooldownTimers = new float[projectileCount];

        selectedProjectile = proyectiles[0];

        if (crosshair != null)
        {
            crosshair.SetActive(false); // Cruceta oculta al inicio
            crosshair.transform.rotation = Quaternion.Euler(90, 0, 0); // Establece la rotación fija inicial de la cruceta (mirando hacia adelante)
        }
    }

    void Update()
    {
        ManageCooldowns();
       
        UpdateCrosshair();
    }

    private void ManageCooldowns()
    {
        // Este bucle mantiene el cooldown activo aunque no estés usando el proyectil actualmente
        for (int i = 0; i < proyectiles.Length; i++)
        {
            if (isProjectileOnCooldown[i])
            {
                projectileCooldownTimers[i] -= Time.deltaTime;
                if (projectileCooldownTimers[i] <= 0f)
                {
                    isProjectileOnCooldown[i] = false; // Resetear el cooldown
                    projectileCooldownTimers[i] = 0f;
                }
            }
        }
    }

   
    public void OnAim(InputValue input)
    {
        aimDirection = input.Get<Vector2>(); // Obtiene la dirección del joystick derecho

        if (aimDirection.magnitude > 0.1f) // Si el joystick está suficientemente desviado
        {
            isAimingWithMouse = false; // Desactivar la entrada del mouse si se usa el joystick
            isAimingWithJoystick = true;

            // Convertir la dirección del joystick a un vector en el espacio del mundo
            lastAimDirection = mainCamera.transform.right * aimDirection.x + mainCamera.transform.forward * aimDirection.y;
            lastAimDirection.y = 0; // Mantener la cruceta en el plano horizontal
            lastAimDirection.Normalize(); // Normalizar para obtener solo la dirección

            if (crosshair != null && !crosshair.activeSelf)
            {
                crosshair.SetActive(true); // Mostrar la cruceta
            }
        }
        else
        {
            lastAimDirection = Vector3.zero; // Reseteamos la dirección cuando no se está moviendo el joystick
            isAimingWithJoystick = false;

            if (crosshair != null && crosshair.activeSelf && !isAimingWithMouse)
            {
                crosshair.SetActive(false); // Ocultar la cruceta si no se está usando el mouse
            }
        }
    }

    public void OnMouseAim(InputValue input)
    {
        // Verifica si el botón del mouse está siendo presionado
        if (input.isPressed && !comboAttackSystem.isAttacking && !isCooldown && !player.isDodging)
        {
            // Si el botón está presionado, indicar que se está apuntando con el mouse
            isAimingWithMouse = true;
            // Y también indicar que el botón derecho del mouse está siendo mantenido presionado
            isRightMouseHeld = true;
            // Asegurar que el joystick no está siendo usado para apuntar simultáneamente
            isAimingWithJoystick = false;

            int projectileID = System.Array.IndexOf(proyectiles, selectedProjectile);

            // Verifica si el proyectil está en cooldown, si es así, no mostrar la cruceta
            if (projectileCooldownTimers[projectileID] <= 0f)
            {
                // Si la cruceta no está activa, activarla para que se muestre en la pantalla
                if (crosshair != null && !crosshair.activeSelf)
                {
                    crosshair.SetActive(true);
                }

                // Actualizar la posición de la cruceta basada en la posición actual del cursor del mouse
                UpdateCrosshair();
            }

        }
        else  // En caso de que el botón del mouse haya sido soltado
        {
            // Verificar que el botón derecho del mouse había sido presionado previamente
            if (isRightMouseHeld)
            {
                // Iniciar la corutina que maneja el disparo del proyectil
                StartCoroutine(FireProjectile());

               
            }
        }
    }

    public void OnRotate(InputValue input) //USANDO EL MANDO INPUT
    {
        if (input.isPressed && !isCooldown && isAimingWithJoystick && !comboAttackSystem.isAttacking )
        {
            StartCoroutine(FireProjectile()); // Disparar cuando se presiona el botón de disparo en el mando
        }
    }

    private void UpdateCrosshair()
    {
        if (crosshair == null || mainCamera == null)
            return;

        if (lastAimDirection != Vector3.zero || isAimingWithMouse) // Actualizar si la dirección es válida o se usa el mouse
        {
            Vector3 newCrosshairPosition;

            if (isAimingWithMouse)
            {
                // Obtener la posición del mouse en un plano horizontal a la altura del jugador
                Plane playerPlane = new Plane(Vector3.up, transform.position);
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (playerPlane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);

                    // Calcular la dirección desde el jugador hacia la posición del mouse
                    Vector3 directionFromPlayerToMouse = (hitPoint - transform.position).normalized;
                    directionFromPlayerToMouse.y = 0; // Mantener la cruceta en el plano horizontal

                    // Calcular la nueva posición de la cruceta en un círculo alrededor del jugador
                    newCrosshairPosition = transform.position + directionFromPlayerToMouse * crosshairRadius;
                }
                else
                {
                    return;
                }
            }
            else
            {
                // Calcular la nueva posición de la cruceta en un círculo alrededor del jugador
                newCrosshairPosition = transform.position + lastAimDirection * crosshairRadius;
                newCrosshairPosition.y = transform.position.y; // Mantener la cruceta al nivel del jugador
            }

            // Actualizar la posición de la cruceta
            crosshair.transform.position = newCrosshairPosition;

            // Rotar la cruceta para que mire hacia la dirección que apunta, incluyendo la dirección vertical
            Quaternion lookRotation = Quaternion.LookRotation(lastAimDirection != Vector3.zero ? lastAimDirection : crosshair.transform.position - transform.position);

            // Modificar solo la rotación en el eje X para mantenerla en 90 grados
            lookRotation = Quaternion.Euler(90, lookRotation.eulerAngles.y, lookRotation.eulerAngles.z + rotation1);

            // Aplicar la rotación final a la cruceta
            crosshair.transform.rotation = lookRotation;
        }
    }

    IEnumerator FireProjectile()
    {

        int projectileID = System.Array.IndexOf(proyectiles, selectedProjectile);

        // Verificar si el proyectil está en cooldown
        if (isProjectileOnCooldown[projectileID])
        {
            Debug.Log("Proyectil en cooldown.");
            yield break;
        }

        // Establecer el cooldown del proyectil
        isProjectileOnCooldown[projectileID] = true;
        projectileCooldownTimers[projectileID] = projectileCooldownTimes[projectileID];

        animator.SetTrigger("Spell");
        playerStats.canMove = false;
        UpdateCrosshair();

        Vector3 targetPosition = crosshair.transform.position;
        targetPosition.y = transform.position.y;
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(aimDir);

        yield return new WaitForSeconds(0.25f);

        if (firePoint != null)
        {
            Vector3 projectileDirection = (new Vector3(crosshair.transform.position.x, firePoint.transform.position.y, crosshair.transform.position.z) - firePoint.transform.position).normalized;

            // Instanciar el proyectil seleccionado
            GameObject vfx = Instantiate(selectedProjectile, firePoint.transform.position, Quaternion.LookRotation(projectileDirection));
            SpellDamage spellDamage = vfx.GetComponent<SpellDamage>();
            if (spellDamage != null)
            {
                spellDamage.projectileID = projectileID + 1; // Almacenar el ID del proyectil
            }
            Destroy(vfx, 2);  // Limpiar el proyectil después de 2 segundos
        }

        playerStats.canMove = true;

        if (crosshair != null && crosshair.activeSelf)
        {
            crosshair.SetActive(false);
        }

        isAimingWithMouse = false;
        isRightMouseHeld = false;
    }

    public void SetProjectile(int weaponID)
    {
        // Cambiar entre proyectiles
        if (weaponID > 0 && weaponID <= proyectiles.Length)
        {
            selectedProjectile = proyectiles[weaponID - 1];
            currentProjectileIndex = weaponID - 1; // Actualiza el índice actual
        }
        else
        {
            selectedProjectile = proyectiles[0];
            currentProjectileIndex = 0; // Reinicia al primer proyectil
        }
    }

    public void OnScrollUp(InputValue input)
    {
        if (input.isPressed)
        {
            ScrollUp();
        }
    }

    // Método para manejar el scroll hacia abajo
    public void OnScrollDown(InputValue input)
    {
        if (input.isPressed)
        {
            ScrollDown();
        }
    }

    // Función que realiza el scroll hacia arriba
    private void ScrollUp()
    {
        currentProjectileIndex--; // Restamos para ir hacia arriba

        if (currentProjectileIndex < 0)
        {
            currentProjectileIndex = proyectiles.Length - 1; // Si llegamos al final, volvemos al último proyectil
        }

        // Actualizar el ID del arma en el Weapon Wheel
        wheelController.weaponID = currentProjectileIndex + 1; // Asegúrate de que el ID sea correcto (1-indexado)
        wheelController.UpdateWeaponIcon(); // Actualizar el ícono en la UI
        SetProjectile(currentProjectileIndex + 1); // Actualiza el proyectil
        Debug.Log("Proyectil seleccionado: " + selectedProjectile.name);
    }

    // Función que realiza el scroll hacia abajo
    private void ScrollDown()
    {
        currentProjectileIndex++; // Sumamos para ir hacia abajo

        if (currentProjectileIndex >= proyectiles.Length)
        {
            currentProjectileIndex = 0; // Si llegamos al final, volvemos al primer proyectil
        }

        // Actualizar el ID del arma en el Weapon Wheel
        wheelController.weaponID = currentProjectileIndex + 1; // Asegúrate de que el ID sea correcto (1-indexado)
        wheelController.UpdateWeaponIcon(); // Actualizar el ícono en la UI
        SetProjectile(currentProjectileIndex + 1); // Actualiza el proyectil
        Debug.Log("Proyectil seleccionado: " + selectedProjectile.name);
    }

}
