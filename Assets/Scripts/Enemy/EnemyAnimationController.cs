using System;
using System.Collections;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public event Action OnDieAnimationFinished = delegate {  };
    
    [SerializeField] private Animator animator;
    [SerializeField] private HealthManager healthManager;
    
    private static readonly int DieStateFullHash = Animator.StringToHash("Base Layer.Die");
    private static readonly int WalkForward = Animator.StringToHash("Walk Forward");
    private static readonly int Attack1 = Animator.StringToHash("Attack 01");
    private static readonly int TakeDamage = Animator.StringToHash("Take Damage");
    private static readonly int Die = Animator.StringToHash("Die");
    
    private void OnEnable()
    {
        healthManager.OnDied += PlayDieAnimation;
        healthManager.OnTakeDamage += PlayTakeDamage;
    }

    private void OnDisable()
    {
        healthManager.OnDied -= PlayDieAnimation;
        healthManager.OnTakeDamage -= PlayTakeDamage;
    }
    
    public void PlayWalk(bool status)
    {
        animator.SetBool(WalkForward,status);
    }

    public void PlayAttack()
    {
        animator.SetTrigger(Attack1);
    }

    public void PlayTakeDamage()
    {
        animator.SetTrigger(TakeDamage);
    }

    public void PlayDieAnimation()
    {
        PlayWalk(false);
        animator.SetTrigger(Die);
        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).fullPathHash != DieStateFullHash)
            yield return null;

        int targetAnimLayerIndex = animator.GetLayerIndex("Base Layer");
        float waitTime = animator.GetCurrentAnimatorStateInfo(targetAnimLayerIndex).length/2f;
        yield return new WaitForSeconds(waitTime);

        OnDieAnimationFinished?.Invoke();
    }
}
