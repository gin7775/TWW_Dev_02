using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.UI;

public class EnemyLock : MonoBehaviour
{
    public LayerMask targetLayer;             // La capa de objetos que se pueden bloquear.
    public float maxLockOnDistance ;    // La distancia m�xima a la que se pueden bloquear los objetivos.
    public CinemachineVirtualCamera virtualCamera;  // La c�mara virtual de Cinemachine.



    private Transform currentTarget;       //Esta variable almacena el enemigo actualmente bloqueado

    public Image markingObject; // Asigna la imagen desde el Inspector

    public Camera mainCamera;

    public Transform[] availableTargets; // Lista de enemigos disponibles dentro del radio de bloqueo
    private int currentTargetIndex = -1;
    public bool isLockOnMode = false;
    private bool firstTimeLock = false;
    public float scrollValue;

    public InputActionReference inputLock;


    [SerializeField]  private Animator cinemachineAnim;
    private void Awake()
    {
     
        inputLock.action.performed += x => scrollValue = x.action.ReadValue<float>();
    }

    private void Update()
    {
        availableTargets = FindAvailableTargets();          //para buscar todos los enemigos dentro del radio de bloqueo y almacenarlos en el arreglo availableTargets

        if(isLockOnMode == false)
        {
            cinemachineAnim.Play("FollowCamera");
        }

  

        if (isLockOnMode == true)
        {
            cinemachineAnim.Play("TargetCamera");
        }
    }

   

    public void OnLock(InputValue value)          //Detecta si has pulsado el bot�n para el lock
    {

        if (value.isPressed)
        {
            if (isLockOnMode)
            {
                // Si el modo de bloqueo est� activado y se presiona el bot�n, desact�valo.
                UnlockTarget();
            }
            else if (availableTargets != null && availableTargets.Length > 0)
            {
                // Si hay objetivos disponibles y el modo de bloqueo est� desactivado, act�valo con el objetivo m�s cercano.
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
                // Cambia al objetivo bloqueado anterior al bajar la rueda del rat�n
                currentTargetIndex = (currentTargetIndex - 1 + availableTargets.Length) % availableTargets.Length;
                LockOn(availableTargets[currentTargetIndex]);
                Debug.Log("Debajo");
            }

        }


       
    }

   

    private void LockOn(Transform target)  //Se llama para bloquear un objetivo cuando se presiona el bot�n de bloqueo y cuando se cambia de objetivo.
    {
        UnlockTarget();         //Se llama a UnlockTarget() primero para asegurarse de que cualquier objetivo anterior se desbloquee.
        isLockOnMode = true;
        currentTarget = target;
        
       

        if (currentTarget != null)
        {
            virtualCamera.Follow = currentTarget.transform;            //Se obtiene el transform del enemigo fijado y se le a�ade al un componente del cinemachine para que le siga la camara
            virtualCamera.LookAt = currentTarget.transform;

            cinemachineAnim.Play("TargetCamera");               //Cambia de la camara del jugador a una camara  que fija al enemigo

            markingObject.enabled = true;
            markingObject.transform.position = mainCamera.WorldToViewportPoint(currentTarget.transform.position);
           



        }
    }

    private void UnlockTarget()  // Desbloquea el objetivo y oculta el indicador.
    {
        
        if (currentTarget != null)
        {
            virtualCamera.Follow = null;
            virtualCamera.LookAt = null;

            isLockOnMode = false;
            currentTarget = null;

            markingObject.enabled = false;
        }
    }

    private Transform[] FindAvailableTargets()  //Esta funci�n busca enemigos dentro del radio de bloqueo especificado utilizando la funci�n Physics.OverlapSphere() y la capa targetLayer.
    {
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, maxLockOnDistance, targetLayer);    
        Transform[] targets = new Transform[hitTargets.Length];

        for (int i = 0; i < hitTargets.Length; i++)
        {
            targets[i] = hitTargets[i].transform;
        }

        return targets;                       //Devuelve un arreglo de objetos Transform que representan los enemigos disponibles.
    }

    private Transform FindClosestTarget(Transform[] targets)           //// Encuentra el objetivo m�s cercano a la posici�n del jugador.
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
