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

    public bool isAttacking = false;

    public float moveDistancePerAttack = 2f;
    CharacterController characterController;

    // Referencias a los sonidos
    public AudioClip[] attackSounds;

    private void Awake()
    {
            DOTween.Init();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 
        characterController = GetComponent<CharacterController>();
    
    }

    public void OnAttack(InputValue input)
    {
        if (input.isPressed)
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
        if (step >= 1 && step <= 3)
        {
            // Cancela la animación anterior si se está en un combo
            if (step != 1)
            {
                animator.ResetTrigger($"Attack{step - 1}");
            }

            animator.SetTrigger($"Attack{step}");

            PlayAttackSound(step - 1);

            Vector3 targetPosition = transform.position + transform.forward * moveDistancePerAttack;

            if (!Physics.Raycast(transform.position, transform.forward, moveDistancePerAttack))      //Para que no atraviese paredes
            {
                // Si no hay colisión, mover con DOTween
                transform.DOMove(targetPosition, comboMaxDelay).SetEase(Ease.OutExpo);
            }
        

            // Reinicia el combo después del tercer ataque
            if (step == 3)
            {
                comboStep = 0;
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

    private void Update()
    {
        // Verifica si se pasó el tiempo de gracia para resetear el combo
        if (comboStep > 0 && Time.time - lastInputTime > comboMaxDelay + gracePeriod)
        {
            ResetCombo();
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

}

