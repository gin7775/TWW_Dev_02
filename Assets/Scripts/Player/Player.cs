using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using DG.Tweening;

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
    private bool isDodging;
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
    private PauseMenu menu;
    private PlayerInput playerInput;

    [Header("Miscellaneous")]
    public Animator animSlash;
    private Animator animator;
    private CharacterController characterController;
    public int collectedKeys, levelKeys;
    public Collider[] interaction;
    public float gravityMultiplier = 3f;
    public bool lockMovement;
    ComboAttackSystem comboAttackSystem;

    private void Awake()
    {       
            playerGraphics = GameObject.FindGameObjectWithTag("Player").transform; 
        
    }

    private void Start()
    {
        combatTrade = this.gameObject.GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        menu = GameObject.Find("UIMenu").GetComponent<PauseMenu>();
        playerInput = new PlayerInput();
        
        comboAttackSystem = GetComponent<ComboAttackSystem>();
        spellScript = GetComponent<SpellScript>();
       
        //animSlash = GetComponentInChildren<Animator>();
        DOTween.Init();
       
        Keyframe dodgeLastFrame = dodgeCurve[dodgeCurve.length - 1];   
        dodgeTimer = dodgeLastFrame.time;
    }
    void Update()
    {
      
        if (!isDodging ) MovementDirection();
        Animations();


        AttackSpeed();
               
        SpellActivate();

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
                                   
            if(!isDodging && !isAttacking && !spellOn && !isCooldownDodge)
            {
                StartCoroutine(Dodge());
            ControladorSonidos.Instance.EjecutarSonido(sfxDash);
            isCooldownDodge = true;
              cooldownTimerDodge = cooldownTimeDodge;
        }
       

    }

   

    public void OnRotate(InputValue input)
    {   
        if(!spellScript.isCooldown)
        {
            StartCoroutine(rotateSpellAtack());
            

        }
        
        if (spellOn && !isAttacking && !isDodging && !spellScript.isCooldown )
        {
            
           RotatePlayer();
            
        }
    }
    
    public void OnMoveInput(float horizontal, float vertical)
    {
        this.vertical = vertical;
        this.horizontal = horizontal;
        isMovementPressed = horizontal != 0 || vertical != 0 ;
        
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
        Vector3 moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;

      

        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);          //Necesitamos el vector que tiene la camara 

        Quaternion rotationToCamera = Quaternion.LookRotation(cameraForward, Vector3.up);
        
        moveDirection = rotationToCamera * moveDirection;
                           

        if(moveDirection != Vector3.zero &&!spellOn)
        {
            Quaternion rotationToMoveDirection = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToMoveDirection, rotationSpeed * Time.deltaTime);
        }
   

        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);
        direction = moveDirection;
    }

    
    
    public void Animations()
    {
        animator.SetFloat("speed", characterController.velocity.sqrMagnitude / playerSpeed);
    }

    public void AttackSpeed()
    {
        if (comboAttackSystem.isAttacking)
        {
            playerSpeed = 0;
        }
        else
        {
            playerSpeed = 4.5f;
        }
      
    }

    public void SpellActivate()
    {

        if (spellOn)
        {
            playerSpeed = 0f;
        }
      
    }
    public void OnHeal(InputValue input)
    {
        Debug.Log("Curaria");
       
        combatTrade.HealPlease();
        
    }

    IEnumerator rotateSpellAtack()
    {
        spellOn = true;

        yield return new WaitForSeconds(0.4f);

        spellOn = false;

    }

    IEnumerator Dodge()
    {
        float t = 0;
        isDodging = true;
        
        particleDash.Play();
        playerGraphics.DOScale(Vector3.zero, 0.2f);
        while (t < dashTime)
        {

            t += Time.deltaTime;
            characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            yield return null;
        }
      
       // float startTime = Time.time;

        playerGraphics.DOScale(new Vector3(1.3f,1.3f,1.3f), 0.3f).SetEase(Ease.OutBack); 


        isDodging = false;
    }

    private IEnumerator dashAttacking()
    {

        
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {

         characterController.Move(transform.forward * attackignDash * Time.deltaTime);

          yield return null;
        }


       
    }

   

    void RotatePlayer()
    {
        
       // animator.SetTrigger("Spell");

        Vector3 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

        Vector3 direction = mouseOnScreen - positionOnScreen;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - anglee;

        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
     

    }

    public void OnMenu(InputValue input)
    {
        if (input.isPressed)
        {
            menu.GameIsPaused = true;
            if(menu.GameIsPaused == true)
            {

                menu.Pause();

            }
            else
            {

                menu.Resume();

            }

        }

       


    }
   

    public void ActivarSlashDerecha()
    {
        
        slashObjDerecha.SetActive(true);
    }
    public void DesactivarSlashDerecha()
    {
       
        slashObjDerecha.SetActive(false);
    }
  }


