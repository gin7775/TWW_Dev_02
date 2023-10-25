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

    [Header("Controls")]

    public float playerSpeed;

    public PlayerStats combatTrade;

    public int collectedKeys,levelKeys;

    public float speedMovement;

    private bool isMovementPressed;

    public bool spellOn;

    public float rotationSpeed;

    public float anglee = 130;

   
    public float gravityMultiplier = 3f;

    private float velocity;

    public float dashTime;
    public float dashSpeed;

    public float cooldownTimeDodge;
    public bool isCooldownDodge = false;
    private float cooldownTimerDodge = 0f;

    float horizontal;

    float vertical;
    [SerializeField] AnimationCurve dodgeCurve;

    bool isDodging;

    float dodgeTimer;

    public GameObject slashObjIzquierda;
    public GameObject slashObjDerecha;
    public Collider[] interaction;

    public float slashCoooldown;

    public float attackignDash;

    private float lastAttackTime;
    public bool withWeapon;
    public bool isAttacking;
    private Transform playerGraphics;

    Vector3 direction;
    Animator animator;

    public Animator animSlash;

    [SerializeField]
    private AudioClip sfxDash;

    [SerializeField]
    private AudioClip sfxSlash;

    

    private PlayerInput playerInput;
    CharacterController characterController;
    public GameObject damageCollider;

    public GameObject damageCollider2;

    private Coroutine attackingCoroutine;
    
    public ParticleSystem particleDash;

    SpellScript spellScript;

    public bool lockMovement;

    private void Awake()
    {       
            playerGraphics = GameObject.FindGameObjectWithTag("Player").transform;       
    }

    private void Start()
    {
        combatTrade = this.gameObject.GetComponent<PlayerStats>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        
        
        playerInput = new PlayerInput();
        

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

        //if (!lockMovement) PlayerRotation();

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Movement")){
            
            playerSpeed = 4.5f;
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SlashLeft"))
        {
          
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SlashRight"))
        {

            isAttacking = true;
        }

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

   

    public void OnAttack(InputValue input)
    {
        if(!isDodging && !spellOn && withWeapon )
        {
            StartCoroutine(AttackingCoroutine());
            StartCoroutine(dashAttacking());
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("SlashLeft"))
            {
                
                
                if (attackingCoroutine != null)
                {
                    StopCoroutine(attackingCoroutine);

                }
                
                animator.Play("SlashRight", 0);
                ControladorSonidos.Instance.EjecutarSonido(sfxSlash);
                attackingCoroutine = StartCoroutine(AttackingCoroutine());

            }
            else
            {
                if(Time.time > lastAttackTime + slashCoooldown)
                {
                   
                    if (attackingCoroutine != null) StopCoroutine(attackingCoroutine);
                    animator.SetTrigger("SlashLeft");
                    animSlash.SetTrigger("SlashLeft");
                    ControladorSonidos.Instance.EjecutarSonido(sfxSlash);
                    attackingCoroutine = StartCoroutine(AttackingCoroutine());                 
                    lastAttackTime = Time.time;

                }               
            }
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
        if (isAttacking)
        {
            playerSpeed = 0;
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

    private IEnumerator AttackingCoroutine()
    {
                   
        
        playerSpeed = 0f;
        yield return new WaitForSecondsRealtime(0.6f);
        playerSpeed = 4.5f;
      
     
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
    public void enableCollider()
    {
        damageCollider.SetActive(true);
        
    }

    public void disableCollider()
    {

        damageCollider.SetActive(false);
    }
    public void enableCollider2()
    {
        damageCollider2.SetActive(true);

    }

    public void disableCollider2()
    {

        damageCollider2.SetActive(false);
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


