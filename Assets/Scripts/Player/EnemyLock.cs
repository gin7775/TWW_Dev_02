using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class EnemyLock : MonoBehaviour
{
    public LayerMask targetLayer;             // La capa de objetos que se pueden bloquear.
    public float maxLockOnDistance ;    // La distancia máxima a la que se pueden bloquear los objetivos.
    public CinemachineVirtualCamera virtualCamera;  // La cámara virtual de Cinemachine.
    ChangeCamera changeCamera;

    
    public Transform currentTarget;       //Esta variable almacena el enemigo actualmente bloqueado

    public GameObject markingObject; // Asigna la imagen desde el Inspector

    public Camera mainCamera;

    public Transform[] availableTargets; // Lista de enemigos disponibles dentro del radio de bloqueo
    public int currentTargetIndex = -1;
    public bool isLockOnMode = false;
    
    private float scrollValue;

    public InputActionReference inputLock;

    public GameObject targetGroup;
    public float rotationSpeed = 50;

    public bool cameraSwitch = false;

    public Vector3 offset;
    [SerializeField]  private Animator cinemachineAnim;
    private void Awake()
    {
     
        inputLock.action.performed += x => scrollValue = x.action.ReadValue<float>();
    }
    private void Start()
    {
        changeCamera = GameObject.Find("TargetGroup1").GetComponent<ChangeCamera>();
    }



    private void Update()
    {
        availableTargets = FindAvailableTargets();          //para buscar todos los enemigos dentro del radio de bloqueo y almacenarlos en el arreglo availableTargets
        if (cameraSwitch)
        {
            return; // No hagas nada si cameraSwitch está activado
        }
        if (isLockOnMode == false && cameraSwitch == false)
        {
            cinemachineAnim.Play("FollowCamera");
            markingObject.SetActive(false);
            
        }
        if (availableTargets.Length == 0 && cameraSwitch == false)
        {
            cinemachineAnim.Play("FollowCamera");
            isLockOnMode = false;
        }

        if (isLockOnMode == true)
        {
            cinemachineAnim.Play("TargetCamera");

            // Verificar si el objetivo actual no es nulo antes de acceder a la imagen de marcado.
            if (currentTarget != null)
            {
                markingObject.SetActive(true);
                markingObject.transform.position = mainCamera.WorldToScreenPoint(currentTarget.transform.position + offset);
            }
           
        }

        if (isLockOnMode && currentTarget != null)
        {
            LookAtTarget(currentTarget.position);
        }

        if (currentTarget == null && availableTargets.Length > 0 && isLockOnMode)
        {
            
            LockOn(FindClosestTarget(availableTargets));  // Encuentra el objetivo más cercano y lo bloquea.
        }
    }

    void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 directionToLook = targetPosition - transform.position;
        directionToLook.y = 0; // Ignora la altura para mantener la rotación en el plano horizontal
        
        Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
       
    }

    public void OnLock(InputValue value)          //Detecta si has pulsado el botón para el lock
    {

        if (value.isPressed)
        {
            if (isLockOnMode)
            {
                // Si el modo de bloqueo está activado y se presiona el botón, desactívalo.
                UnlockTarget();
            }
            else if (availableTargets != null && availableTargets.Length > 0)
            {
                // Si hay objetivos disponibles y el modo de bloqueo está desactivado, actívalo con el objetivo más cercano.
                LockOn(FindClosestTarget(availableTargets));
            }
        }

    }

    public void OnChangeLock(InputValue value)          
    {
        
        if (isLockOnMode == true )
        {

            if(scrollValue > 0f && availableTargets.Length > 1)
            {
                currentTargetIndex = (currentTargetIndex + 1) % availableTargets.Length;
                LockOn(availableTargets[currentTargetIndex]);
                Debug.Log("Arriba");
            }
            else if (scrollValue < 0f && availableTargets.Length > 1)
            {
                // Cambia al objetivo bloqueado anterior al bajar la rueda del ratón
                currentTargetIndex = (currentTargetIndex - 1 + availableTargets.Length) % availableTargets.Length;
                LockOn(availableTargets[currentTargetIndex]);
                Debug.Log("Debajo");
            }

        }


       
    }

   

    private void LockOn(Transform target)  //Se llama para bloquear un objetivo cuando se presiona el botón de bloqueo y cuando se cambia de objetivo.
    {
        UnlockTarget();         //Se llama a UnlockTarget() primero para asegurarse de que cualquier objetivo anterior se desbloquee.
        isLockOnMode = true;
        currentTarget = target;
        
       

        if (currentTarget != null)
        {
            

            virtualCamera.Follow = targetGroup.transform;
            virtualCamera.LookAt = targetGroup.transform;
        }
    }

    private void UnlockTarget()  // Desbloquea el objetivo y oculta el indicador.
    {
        
        if (currentTarget != null)
        {

            isLockOnMode = false;
            currentTarget = null;

            markingObject.SetActive(false);
        }
    }

    private Transform[] FindAvailableTargets()  //Esta función busca enemigos dentro del radio de bloqueo especificado utilizando la función Physics.OverlapSphere() y la capa targetLayer.
    {
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, maxLockOnDistance, targetLayer);    
        Transform[] targets = new Transform[hitTargets.Length];

        for (int i = 0; i < hitTargets.Length; i++)
        {
            targets[i] = hitTargets[i].transform;
        }

        return targets;                       //Devuelve un arreglo de objetos Transform que representan los enemigos disponibles.
    }

    private Transform FindClosestTarget(Transform[] targets)           // Encuentra el objetivo más cercano a la posición del jugador.
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
