using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class EnemyLock : MonoBehaviour
{
    public LayerMask targetLayer; // La capa de objetos que se pueden bloquear.
    public float maxLockOnDistance; // La distancia máxima a la que se pueden bloquear los objetivos.
    public CinemachineVirtualCamera virtualCamera; // La cámara virtual de Cinemachine.
    private ChangeCamera changeCamera;

    public Transform currentTarget; // Esta variable almacena el enemigo actualmente bloqueado
    public GameObject markingObject; // Asigna la imagen desde el Inspector
    public Camera mainCamera;

    public Transform[] availableTargets; // Lista de enemigos disponibles dentro del radio de bloqueo
    public int currentTargetIndex = -1;
    public bool isLockOnMode = false;

    private float scrollValue;

    public InputActionReference inputLock;
    public InputActionReference changeLockAction; // Acción para cambiar el objetivo usando L2

    public GameObject targetGroup;
    public float rotationSpeed = 50;
    public bool cameraSwitch = false;
    private Transform previousTarget = null;
    public Vector3 offset;
    [SerializeField] private Animator cinemachineAnim;

    private void Awake()
    {
        // No necesitamos suscribir eventos aquí si usamos InputValue en métodos generados
    }

    private void Start()
    {
        changeCamera = GameObject.Find("TargetGroup1").GetComponent<ChangeCamera>();
    }

    private void Update()
    {
        availableTargets = FindAvailableTargets(); // Buscar todos los enemigos dentro del radio de bloqueo

        if (cameraSwitch)
        {
            return; // No hagas nada si cameraSwitch está activado
        }

        if (!isLockOnMode && !cameraSwitch)
        {
            cinemachineAnim.Play("FollowCamera");
            markingObject.SetActive(false);
        }

        if (availableTargets.Length == 0 && !cameraSwitch)
        {
            cinemachineAnim.Play("FollowCamera");
            isLockOnMode = false;
        }

        if (isLockOnMode)
        {
            cinemachineAnim.Play("TargetCamera");

            if (currentTarget != null)
            {
                markingObject.SetActive(true);
                markingObject.transform.position = mainCamera.WorldToScreenPoint(currentTarget.transform.position + offset);
            }

            if (previousTarget != currentTarget)
            {
                if (previousTarget != null)
                {
                    var healthManager = previousTarget.GetComponent<EnemyHealthBarManager>();
                    if (healthManager != null)
                    {
                        healthManager.DeactivateHealthBar();
                    }
                }

                if (currentTarget != null)
                {
                    var healthManager = currentTarget.GetComponent<EnemyHealthBarManager>();
                    if (healthManager != null)
                    {
                        healthManager.ActivateHealthBar();
                    }
                }

                previousTarget = currentTarget;
            }
        }
        else
        {
            if (previousTarget != null)
            {
                var healthManager = previousTarget.GetComponent<EnemyHealthBarManager>();
                if (healthManager != null)
                {
                    healthManager.DeactivateHealthBar();
                }
                previousTarget = null;
            }
        }

        if (isLockOnMode && currentTarget != null)
        {
            LookAtTarget(currentTarget.position);
        }

        if (currentTarget == null && availableTargets.Length > 0 && isLockOnMode)
        {
            LockOn(FindClosestTarget(availableTargets)); // Encuentra el objetivo más cercano y lo bloquea.
        }
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 directionToLook = targetPosition - transform.position;
        directionToLook.y = 0; // Ignora la altura para mantener la rotación en el plano horizontal

        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void OnLock(InputValue value) // Detecta si has pulsado el botón para el lock
    {
        if (value.isPressed)
        {
            if (isLockOnMode)
            {
                UnlockTarget();
            }
            else if (availableTargets.Length > 0)
            {
                LockOn(FindClosestTarget(availableTargets));
            }
        }
    }

    public void OnChangeLock(InputValue value)
    {
        if (!isLockOnMode || !value.isPressed) return;

        // Cambiar al siguiente objetivo al presionar L2
        if (availableTargets.Length > 1)
        {
            currentTargetIndex = (currentTargetIndex + 1) % availableTargets.Length;
            LockOn(availableTargets[currentTargetIndex]);
            Debug.Log("Siguiente objetivo (L2)");
        }
    }

    private void LockOn(Transform target)
    {
        UnlockTarget();
        isLockOnMode = true;
        currentTarget = target;

        if (currentTarget != null)
        {
            virtualCamera.Follow = targetGroup.transform;
            virtualCamera.LookAt = targetGroup.transform;
        }
    }

    private void UnlockTarget()
    {
        if (currentTarget != null)
        {
            isLockOnMode = false;
            currentTarget = null;
            markingObject.SetActive(false);
        }
    }

    private Transform[] FindAvailableTargets()
    {
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, maxLockOnDistance, targetLayer);
        Transform[] targets = new Transform[hitTargets.Length];

        for (int i = 0; i < hitTargets.Length; i++)
        {
            targets[i] = hitTargets[i].transform;
        }

        return targets;
    }

    private Transform FindClosestTarget(Transform[] targets)
    {
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }
}
