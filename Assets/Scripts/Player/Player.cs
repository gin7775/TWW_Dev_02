using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour
{

    [Header("Player Movement")]
    public float playerSpeed;
    public float rotationSpeed;
    private float velocity;
    private float horizontal;
    private float vertical;
    private Vector3 direction;
    public bool isMovementPressed;
    public float angleDifferenceRotation;
    [Header("Combat")]
    public PlayerStats combatTrade;
    public float slashCoooldown;
    public float attackignDash;
    public GameObject slashObjIzquierda;
    public GameObject slashObjDerecha;
    private float lastAttackTime;
    public bool withWeapon;
    public bool isAttacking;
    private Coroutine attackingCoroutine;
    public GameObject damageCollider;
    public GameObject damageCollider2;

    [Header("Dodging")]
    public float dashTime;
    public float dashSpeed;
    public float cooldownTimeDodge;
    private bool isCooldownDodge = false;
    private float cooldownTimerDodge = 0f;
    public bool isDodging;
    private float dodgeTimer;
    [SerializeField] private AnimationCurve dodgeCurve;
    public ParticleSystem particleDash;
    

    [Header("Spell Casting")]
    public bool spellOn;
    public float anglee = 130;
    private SpellScript spellScript;
    private Transform playerGraphics;

    [Header("Audio")]
    [SerializeField] private AudioClip sfxDash;
    [SerializeField] private AudioClip sfxSlash;

    [Header("UI and Input")]
    public PauseMenu menu;
    private PlayerInput playerInput;
    public GameObject Inventory;
    

    [Header("Miscellaneous")]
    public Animator animSlash;
    private Animator animator;
    private CharacterController characterController;
    public int collectedKeys, levelKeys;
    public Collider[] interaction;
    public float gravityMultiplier = 3f;
    public bool lockMovement;
    ComboAttackSystem comboAttackSystem;
    EnemyLock enemyLock;
    WeaponWheelController weaponWheelController;

    private Vector3 gravityVelocity;
    public ParticleSystem[] slash;


    public float gravity = -9.81f;
    public bool IsAttacking { get; internal set; }
    public bool IsDodging { get; internal set; }

    public GameObject firstGameObjectWheel;

    private void Awake()
    {       
            playerGraphics = GameObject.FindGameObjectWithTag("Player").transform;
        enemyLock = GetComponent<EnemyLock>();

        
    }

    private void Start()
    {
        comboAttackSystem = GetComponent<ComboAttackSystem>();

        combatTrade = this.gameObject.GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        menu = GameObject.Find("UIMenu").GetComponent<PauseMenu>();
        playerInput = new PlayerInput();
        weaponWheelController = GameObject.Find("WeaponWheel").GetComponent<WeaponWheelController>();
      
        spellScript = GetComponent<SpellScript>();
      
        //animSlash = GetComponentInChildren<Animator>();
        DOTween.Init();
       
        Keyframe dodgeLastFrame = dodgeCurve[dodgeCurve.length - 1];   
        dodgeTimer = dodgeLastFrame.time;
        cooldownTimerDodge = 0.5f;

    }
    void Update()
    {

        animator.SetBool("isLockOn", enemyLock.isLockOnMode && enemyLock.currentTarget != null);

        if (enemyLock.isLockOnMode && enemyLock.currentTarget != null && combatTrade.canMove)
        {
            FaceTarget();
            MoveRelativeToTargetAndCamera();
        }
        else
        {
            if ( combatTrade.canMove) MovementDirection();
        }

        UpdateAnimations();
        Animations();

        ApplyGravity();
        AttackSpeed();


        if (isCooldownDodge)
        {
            cooldownTimerDodge -= Time.deltaTime;
            if (cooldownTimerDodge <= 0f)
            {
                isCooldownDodge = false;
            }
        }


        
    }

   
    public void OnRoll(InputValue value)
    {
                                   
        if( comboAttackSystem.readyDash && !spellOn && !isCooldownDodge && !comboAttackSystem.isOnMenu && !weaponWheelController.weaponWheelSelected)
        {
            StartCoroutine(Dodge());
            MiFmod.Instance.Play("SFX_2d/Esquive");
            isCooldownDodge = true;
            cooldownTimerDodge = cooldownTimeDodge;
        }
       

    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f; // Asegura que el personaje toque el suelo
        }
        else
        {
            // Aplica la gravedad al personaje si no estEen el suelo
            gravityVelocity.y += gravity * gravityMultiplier * Time.deltaTime;
        }

        characterController.Move(gravityVelocity * Time.deltaTime);

        // Si el personaje estEen el suelo, resetea la velocidad de gravedad para evitar acumulación
        if (characterController.isGrounded)
        {
            gravityVelocity.y = 0;
        }
    }
    private void MoveRelativeToTargetAndCamera()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0; // Ignorar la componente Y para mantener el movimiento en el plano horizontal
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcula la dirección del movimiento basada en las entradas del jugador y la orientación de la cámara
        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;
        moveDirection.Normalize();

        // Mueve el personaje
        characterController.Move(moveDirection * playerSpeed * 0.8f * Time.deltaTime);
    }

    private void FaceTarget()
    {
        Vector3 directionToTarget = enemyLock.currentTarget.position - transform.position;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    
    
    public void OnMoveInput(float horizontal, float vertical)
    {
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        this.vertical = inputDirection.z;
        this.horizontal = inputDirection.x;
        isMovementPressed = inputDirection.magnitude > 0;

    }
    public void OnInteract(InputValue input) 
    {
        interaction = Physics.OverlapSphere(transform.position, 2f);
        foreach(Collider coll in interaction)
        {
            if (coll.gameObject.tag == "load1")
            {
                coll.gameObject.GetComponent<Interaction>().interaction = 1;
                coll.gameObject.GetComponent<Interaction>().Interact();

            }
            if (coll.gameObject.tag == "load2")
            {
                coll.gameObject.GetComponent<Interaction>().interaction = 2;
                coll.gameObject.GetComponent<Interaction>().Interact();

            }
        }
        

    }

    public void MovementDirection()
    {
        if (!combatTrade.canMove) return;

        // Normaliza la entrada de movimiento
        Vector3 inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDirection = Vector3.forward * inputDirection.z + Vector3.right * inputDirection.x;

        // Calcula la dirección de la cámara en el plano horizontal
        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        Quaternion rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);

        // Aplica la rotación de la cámara al movimiento
        moveDirection = rotationToCamera * moveDirection;

        // Comprueba si hay un movimiento de entrada
        if (moveDirection != Vector3.zero && !spellOn)
        {
            // Calcula la rotación deseada
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);

            // Calcula la diferencia de ángulo
            float angleDifference = Quaternion.Angle(transform.rotation, rotationToMoveDirection);

            // Ajusta la velocidad de rotación según la diferencia de ángulo
            float adjustedRotationSpeed = rotationSpeed;
            if (angleDifference > angleDifferenceRotation) // Umbral para giro rápido, puedes ajustar este valor
            {
                adjustedRotationSpeed *= 5f; 
            }

            // Rota el personaje hacia la dirección deseada
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, adjustedRotationSpeed * Time.deltaTime);
        }

        // Mueve el personaje
        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
        direction = moveDirection;
    }


   
    private void UpdateAnimations()
    {
        Vector3 moveInput = new Vector3(horizontal, 0, vertical);
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0; // Mantener el movimiento en el plano horizontal
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Transformar las entradas de movimiento al espacio de la cámara
        Vector3 moveDirection = cameraForward * moveInput.z + cameraRight * moveInput.x;

        // Calcula las entradas relativas para el Animator
        Vector3 localMove = transform.InverseTransformDirection(moveDirection);
        float animHorizontal = localMove.x;
        float animVertical = localMove.z;

        animator.SetFloat("Horizontal", animHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", animVertical, 0.1f, Time.deltaTime);
    }

    public void Animations()
    {
        // Calcula la magnitud del vector de entrada
        float targetSpeed = new Vector2(horizontal, vertical).magnitude;

        // Suaviza el cambio de la velocidad usando Lerp
        float currentSpeed = animator.GetFloat("speed");
        float smoothSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.15f); // 0.1f es el factor de suavizado

        // Establece el valor suavizado en el Animator
        animator.SetFloat("speed", smoothSpeed);
    }

    public void AttackSpeed()
    {
        if (comboAttackSystem == null)
        {
            Debug.LogError("comboAttackSystem is not set.");
            return;
        }
        if (comboAttackSystem.isAttacking)
        {
            playerSpeed = 0;
        }
        else
        {
            playerSpeed = 4.5f;
        }

    }


    public void OnHeal(InputValue input)
    {
        Debug.Log("Curaria");
        if (!comboAttackSystem.isOnMenu)
        {
            combatTrade.HealPlease();
        }
        
        
    }

   

    IEnumerator Dodge()
    {
        float t = 0;
        isDodging = true;
        
        particleDash.Play();
        playerGraphics.DOScale(Vector3.zero, 0.2f);

        // Obtener la orientación de la cámara
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0; // Ignorar la componente Y para mantener el movimiento en el plano horizontal
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular la dirección del dash basada en las entradas del jugador y la orientación de la cámara
        Vector3 dashDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward; // Usar la dirección hacia adelante del personaje si no hay entrada
        }

        while (t < dashTime)
        {
            t += Time.deltaTime;
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null;
        }

        playerGraphics.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.3f).SetEase(Ease.OutBack);

        isDodging = false;
     
    }

    public void OnMenu(InputValue input)
    {
        if (input.isPressed)
        {
            menu.GameIsPaused = true;
            comboAttackSystem.isOnMenu = true;
            if(menu.GameIsPaused == true)
            {
                
                menu.Pause();
                
            }
            else
            {

                menu.Resume();
                comboAttackSystem.isOnMenu = false;

            }

        }
    }

    public void OnInventory(InputValue input)
    {
        if (input.isPressed)
        {
           
            weaponWheelController.weaponWheelSelected = ! weaponWheelController.weaponWheelSelected;
            EventSystem.current.SetSelectedGameObject(firstGameObjectWheel);

        }

    }


    public void ActivarSlash1()
    {

        slash[0].Play();
    }
    public void DesactivarSlash2()
    {

        slash[1].Play();
    }
    public void ActivarSlash3()
    {
        slash[2].Play();
    }

    public void SpeedPlayer()
    {
        playerSpeed = 0;
    }
    public void SpeedPlayer2()
    {
        playerSpeed = 4.5f;
    }
}


