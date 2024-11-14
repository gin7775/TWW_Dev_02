using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SpellScript : MonoBehaviour
{
    [Header("Configuraciones de Disparo")]
    public GameObject firePoint;
    public GameObject[] proyectiles; // Arreglo de proyectiles
    public GameObject[] effectSpawnProjectile; // Efectos a spawnear con los proyectiles

    private int currentProjectileIndex = 0;

    [Header("Configuraciones de la Cruceta")]
    public GameObject[] crosshairObjects; // Arreglo de GameObjects para la cruceta
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
    private ComboAttackSystem comboAttackSystem;
    public float slopeThreshold = 10f;
    public LayerMask groundLayerMask;
    [Header("Cooldown de Proyectiles")]
    public float[] projectileCooldownTimes; // Cooldown específico para cada proyectil
    public float[] projectileCooldownTimers; // El temporizador que cuenta el tiempo restante para cada proyectil
    private bool[] isProjectileOnCooldown;
     public bool ProjectilesUnlocked = false;
    private WeaponWheelController wheelController;

    public Material fireMaterial;
    public Material swordMaterial;
    public float  intensity = 2.0f;

    PauseMenu pauseMenu;

    private EnemyLock enemyLock;

    

    void Start()
    {
        comboAttackSystem = GetComponent<ComboAttackSystem>();
        enemyLock = GetComponent<EnemyLock>();
        selectedProjectile = proyectiles[0];
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerStats = GetComponent<PlayerStats>();
        wheelController = GameObject.Find("WeaponWheel").GetComponent<WeaponWheelController>();
        pauseMenu = GameObject.Find("UIMenu").GetComponent<PauseMenu>();
        int projectileCount = proyectiles.Length;
        isProjectileOnCooldown = new bool[projectileCount];
        projectileCooldownTimers = new float[projectileCount];

        selectedProjectile = proyectiles[0];

        if (crosshairObjects.Length > 0)
        {
            DeactivateAllCrosshairs(); // Desactiva todas las crucetas al inicio
            
        }
    }

    void Update()
    {
        ManageCooldowns(); // Manejo de cooldowns
        UpdateCrosshair(); // Actualización de la posición de la cruceta
    }

    private void ManageCooldowns()
    {
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

        if (aimDirection.magnitude > 0.1f && !wheelController.weaponWheelSelected) // Si el joystick está suficientemente desviado
        {
            isAimingWithMouse = false; // Desactivar la entrada del mouse si se usa el joystick
            isAimingWithJoystick = true;

            lastAimDirection = mainCamera.transform.right * aimDirection.x + mainCamera.transform.forward * aimDirection.y;
            lastAimDirection.y = 0; // Mantener la cruceta en el plano horizontal
            lastAimDirection.Normalize(); // Normalizar para obtener solo la dirección

            if (crosshairObjects.Length > 0 && !crosshairObjects[currentProjectileIndex].activeSelf)
            {
                ActivateCrosshair(currentProjectileIndex); // Mostrar la cruceta
            }
        }
        else
        {
            lastAimDirection = Vector3.zero;
            isAimingWithJoystick = false;

            if (crosshairObjects.Length > 0 && crosshairObjects[currentProjectileIndex].activeSelf && !isAimingWithMouse)
            {
                DeactivateAllCrosshairs(); // Ocultar la cruceta si no se está usando el mouse
            }
        }
    }

    public void OnMouseAim(InputValue input)
    {
        if (input.isPressed && !comboAttackSystem.isAttacking && !isCooldown && !player.isDodging && !wheelController.weaponWheelSelected)
        {
            isAimingWithMouse = true;
            isRightMouseHeld = true;
            isAimingWithJoystick = false;

            int projectileID = System.Array.IndexOf(proyectiles, selectedProjectile);

            if (projectileCooldownTimers[projectileID] <= 0f)
            {
                if (crosshairObjects.Length > 0 && !crosshairObjects[currentProjectileIndex].activeSelf)
                {
                    ActivateCrosshair(currentProjectileIndex);
                }
                UpdateCrosshair();
            }

        }
        else
        {
            if (isRightMouseHeld)
            {
                StartCoroutine(FireProjectile());
            }
        }
    }

    public void OnRotate(InputValue input)
    {
        if (input.isPressed && !isCooldown && isAimingWithJoystick && !comboAttackSystem.isAttacking && !wheelController.weaponWheelSelected)
        {
            StartCoroutine(FireProjectile());
        }
    }

    private void UpdateCrosshair()
    {
        if (crosshairObjects.Length == 0 || mainCamera == null)
            return;

        Vector3 newCrosshairPosition;

        // Si estamos en LockOn, la cruceta debe seguir al enemigo
        if (enemyLock != null && enemyLock.isLockOnMode && enemyLock.currentTarget != null)
        {
            Vector3 directionToEnemy = (enemyLock.currentTarget.position - transform.position).normalized;
            directionToEnemy.y = 0; // Mantener la dirección en el plano horizontal

            newCrosshairPosition = transform.position + directionToEnemy * crosshairRadius; // Colocamos la cruceta en la dirección del enemigo
        }
        else
        {
            // Si no estamos en LockOn, la cruceta sigue el joystick o el ratón
            if (isAimingWithMouse)
            {
                Plane playerPlane = new Plane(Vector3.up, transform.position);
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (playerPlane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Vector3 directionFromPlayerToMouse = (hitPoint - transform.position).normalized;
                    directionFromPlayerToMouse.y = 0; // Asegurarse de que solo se mueva en el plano horizontal

                    newCrosshairPosition = transform.position + directionFromPlayerToMouse * crosshairRadius;
                }
                else
                {
                    return; // Si no se puede calcular la posición del ratón, no hacemos nada
                }
            }
            else
            {
                // Si no estamos en modo LockOn, la cruceta sigue la dirección del joystick
                newCrosshairPosition = transform.position + lastAimDirection * crosshairRadius;
                newCrosshairPosition.y = transform.position.y; // Mantener la altura del jugador
            }
        }

        // Proyectar un Raycast hacia abajo desde la posición calculada usando la capa de suelo
        RaycastHit hit;
        Vector3 crosshairHeightAdjusted = newCrosshairPosition;

        if (Physics.Raycast(newCrosshairPosition + Vector3.up * 10, Vector3.down, out hit, 20f, groundLayerMask))
        {
            // Calcula el ángulo del terreno en el punto de impacto
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);

            // Solo ajusta la altura si la pendiente excede el umbral
            if (slopeAngle > slopeThreshold)
            {
                crosshairHeightAdjusted.y = hit.point.y + 0.2f; // Añadir un pequeño margen sobre el terreno inclinado
            }
        }

        // Actualizar la posición de la cruceta
        crosshairObjects[currentProjectileIndex].transform.position = crosshairHeightAdjusted;

        // Verificar si la dirección no es cero antes de aplicar LookRotation
        Vector3 directionToCrosshair = crosshairHeightAdjusted - transform.position;
        if (directionToCrosshair != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToCrosshair);
            lookRotation = Quaternion.Euler(90, lookRotation.eulerAngles.y + rotation1, lookRotation.eulerAngles.z);
            crosshairObjects[currentProjectileIndex].transform.rotation = lookRotation;
        }
    }

    IEnumerator FireProjectile()
    {
        int projectileID = System.Array.IndexOf(proyectiles, selectedProjectile);

        if (isProjectileOnCooldown[projectileID])
        {
            Debug.Log("Proyectil en cooldown.");
            yield break;
        }

        isProjectileOnCooldown[projectileID] = true;
        projectileCooldownTimers[projectileID] = projectileCooldownTimes[projectileID];

        animator.SetTrigger("Spell");
        playerStats.canMove = false; // Deshabilitar movimiento durante el lanzamiento
        UpdateCrosshair();

        Vector3 targetPosition;

        // Si estamos en modo LockOn, usamos el enemigo bloqueado
        if (enemyLock != null && enemyLock.isLockOnMode && enemyLock.currentTarget != null)
        {
            targetPosition = enemyLock.currentTarget.position; // Dirección hacia el enemigo bloqueado
        }
        else
        {
            // Si no estamos en LockOn, usamos la cruceta
            targetPosition = crosshairObjects[currentProjectileIndex].transform.position;
        }

        targetPosition.y = transform.position.y; // Mantener la altura constante

        // Calculamos la dirección del disparo hacia el objetivo
        Vector3 aimDir = (targetPosition - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(aimDir); // Rotamos hacia el objetivo

        yield return new WaitForSeconds(0.25f); // Espera un tiempo antes de lanzar el proyectil

        if (firePoint != null)
        {
            // Calculamos la dirección final del proyectil, manteniendo la altura del firePoint
            Vector3 projectileDirection = (new Vector3(targetPosition.x, firePoint.transform.position.y, targetPosition.z) - firePoint.transform.position).normalized;

            // Instanciamos el proyectil en la posición del firePoint y lo orientamos hacia el objetivo
            GameObject vfx = Instantiate(selectedProjectile, firePoint.transform.position, Quaternion.LookRotation(projectileDirection));

            SpellDamage spellDamage = vfx.GetComponent<SpellDamage>();
            if (spellDamage != null)
            {
                spellDamage.projectileID = projectileID + 1; // Asignamos el ID del proyectil
            }
            Destroy(vfx, 2); // Destruir el proyectil después de 2 segundos
        }

        playerStats.canMove = true; // Rehabilitar movimiento

        DeactivateAllCrosshairs(); // Ocultar la cruceta después de lanzar

        isAimingWithMouse = false;
        isRightMouseHeld = false;
    }

  

    public void SetProjectile(int weaponID)
    {
        if (!ProjectilesUnlocked && weaponID > 1)
        {
            Debug.Log("Los proyectiles especiales aún no están desbloqueados.");
            weaponID = 1; // Forzar la selección del proyectil básico
        }
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


        ChangeFireColorBasedOnProjectile(currentProjectileIndex, intensity);
    }

    private void ChangeFireColorBasedOnProjectile(int projectileIndex, float intensity)
    {
        if (fireMaterial != null)
        {
            Color newColor;
            Color buttomColor;
            Color swordColor;
            switch (projectileIndex)
            {
                case 0:
                    newColor = new Color(11.8f, 0.1727885f, 16.50127f); //Morado
                    buttomColor = new Color(1.826875f, 0.0f, 3.379695f);
                    swordColor = new Color(12.4586f, 0f, 18.16482f);
                    break;
                case 1:
                    newColor = new Color(0f, 11.98431f, 11.41961f); // Azul HDR
                    buttomColor = new Color(0f, 1.396078f, 2.996078f);
                    swordColor = new Color(0f, 15.21063f, 18.16482f);
                    break;
                case 2:
                    newColor = new Color(13f, 0f, 0f); // Rojo HDR
                    buttomColor = new Color(6.422235f, 0.06724853f, 0f);
                    swordColor = new Color(24f, 0f, 0f);
                    break;
                default:
                    newColor = new Color(11.8f, 0.1727885f, 16.50127f); //Morado
                    buttomColor = new Color(1.826875f, 0.0f, 3.379695f);
                    swordColor = new Color(12.4586f, 0f, 18.16482f);
                    break;
            }
            newColor *= intensity;
            
            fireMaterial.SetColor("_Color", newColor);
            fireMaterial.SetColor("_ButtomColor",buttomColor);
            swordMaterial.SetColor("_EmissionColor", swordColor);
        }
    }
    public void OnScrollUp(InputValue input)
    {
        if (input.isPressed && !pauseMenu.GameIsPaused && ProjectilesUnlocked)
        {
            ScrollUp();
        }
    }

    // Método para manejar el scroll hacia abajo
    public void OnScrollDown(InputValue input)
    {
        if (input.isPressed && !pauseMenu.GameIsPaused && ProjectilesUnlocked)
        {
            ScrollDown();
        }
    }

    // Función que realiza el scroll hacia arriba
    private void ScrollUp()
    {
        if (!ProjectilesUnlocked) return;
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
        if (!ProjectilesUnlocked) return;
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

    private void ActivateCrosshair(int index)
    {
        DeactivateAllCrosshairs(); // Desactiva todas las crucetas
        crosshairObjects[index].SetActive(true); // Activa la cruceta correspondiente al proyectil seleccionado
    }

    private void DeactivateAllCrosshairs()
    {
        foreach (var crosshair in crosshairObjects)
        {
            crosshair.SetActive(false); // Desactiva cada cruceta en el arreglo
        }
    }

    public void UnlockSpecialProjectiles()
    {
        ProjectilesUnlocked = true;
        Debug.Log("Proyectiles especiales desbloqueados.");
    }
}
