using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ComboAttackSystem : MonoBehaviour
{
    private Animator animator;
    private int comboStep = 0;
    private float lastInputTime = Mathf.NegativeInfinity;
    public float comboMaxDelay = 0.8f; // Tiempo máximo para el siguiente golpe del combo
    public float gracePeriod = 0.3f; // Tiempo de gracia para continuar el combo
    private AudioSource audioSource;
    public GameObject damageCollider;
    public GameObject damageCollider2;
    public GameObject damageCollider3;

    public float cooldownTime = 0.2f; // Tiempo de espera después del tercer ataque
    private float cooldownTimer = 0;
    public bool isAttacking = false;

    public float moveDistancePerAttack = 2f;
    public float moveDistancePerAttack2 = 2f;
    CharacterController characterController;
    Player player;

    public bool readyDash;
    // Referencias a los sonidos
    public AudioClip[] attackSounds;
    private bool isTriggerMovementActive = false;
    private Vector3 moveVelocity;
    private float moveTimer;
    public AnimationCurve moveCurve;
    private void Awake()
    {
        DOTween.Init();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        characterController = GetComponent<CharacterController>();

        player = GetComponent<Player>();
    
    }

    public void OnAttack(InputValue input)
    {
        if (input.isPressed && (cooldownTimer <= 0) && !player.spellOn && !player.isDodging)
        {
            isAttacking = true;
            if (Time.time - lastInputTime < comboMaxDelay + gracePeriod)
            {
                comboStep++;
                if (comboStep > 3)
                {
                    comboStep = 1; // Si el combo supera 3, reinicia al primer ataque
                }
            }
            else
            {
                comboStep = 1; // Si se ha excedido el tiempo, reinicia el combo
            }

            lastInputTime = Time.time;
            PerformAttack(comboStep);
        }
    }
    
    private void PerformAttack(int step)
    {

        ResetAllTriggers();
        if (step >= 1 && step <= 3)
        {
            // Cancela la animación anterior si se está en un combo
            if (step != 1)
            {
                animator.ResetTrigger($"Attack{step - 1}");
            }

            animator.SetTrigger($"Attack{step}");

            PlayAttackSound(step - 1);

           
        

            // Reinicia el combo después del tercer ataque
            if (step == 3)
            {
                comboStep = 0;
                cooldownTimer = cooldownTime;
                
            }
        }
    }

    private void PlayAttackSound(int index)
    {
        if (attackSounds != null && attackSounds.Length > index)
        {
            audioSource.clip = attackSounds[index];
            audioSource.Play();
        }
    }

        public void TriggerMovement()
        {
         Vector3 targetPosition = transform.position + transform.forward * moveDistancePerAttack;
         if (!Physics.Raycast(transform.position, transform.forward, moveDistancePerAttack))
         {
            moveVelocity = (targetPosition - transform.position) / comboMaxDelay;
            moveTimer = 0f;
            isTriggerMovementActive = true;
         }
    }

        public void TriggerMovementCombo3()
        {
        Vector3 targetPosition = transform.position + transform.forward * moveDistancePerAttack2;
        if (!Physics.Raycast(transform.position, transform.forward, moveDistancePerAttack2))
        {
            moveVelocity = (targetPosition - transform.position) / comboMaxDelay;
            moveTimer = 0f;
            isTriggerMovementActive = true;
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        // Verifica si se pasó el tiempo de gracia para resetear el combo
        if (comboStep > 0 && Time.time - lastInputTime > comboMaxDelay + gracePeriod)
        {
            ResetCombo();
        }
        if (isTriggerMovementActive)
        {
            moveTimer += Time.deltaTime;
            float moveFactor = moveCurve.Evaluate(moveTimer / comboMaxDelay);
            characterController.Move(moveVelocity * moveFactor * Time.deltaTime);

            if (moveTimer >= comboMaxDelay)
            {
                isTriggerMovementActive = false;
            }
        }
    }

    
    private void ResetCombo()
    {
        comboStep = 0;
        ResetAllTriggers();
        animator.SetTrigger("Move");
        isAttacking = false;
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("Move"); 
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

    public void enableCollider3()
    {
        damageCollider3.SetActive(true);

    }

    public void disableCollider3()
    {

        damageCollider3.SetActive(false);
    }

    public void DisableBool()
    {
            isAttacking = false;
    }


    public void ActiveDashBool1()
    {
        readyDash = true;
    }
    public void DisableDashBool1()
    {
        readyDash = false;
    }
}

