using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemyAnimationController enemyAnimationController;
    [SerializeField] private float baseDamage = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float timeBetweenAttacks = 3.5f;
    [SerializeField] private float attackRange = 2f;

    private Transform _playerToFollow;
    private bool _playerInAttackRange;
    private bool _alreadyAttacked;
    private RewardController _rewardControllerReference;

    public NavMeshAgent Agent => agent;

    private void OnEnable()
    {
        enemyAnimationController.OnDieAnimationFinished += DestroyEnemy;
    }

    private void OnDisable()
    {
        enemyAnimationController.OnDieAnimationFinished -= DestroyEnemy;
    }

    private void Update()
    {
        _playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if (!_playerInAttackRange)
            FollowPlayer();
        else
            AttackPlayer();
    }
    
    public void SetPlayerToFollow(Transform playerToFollow)
    {
        _playerToFollow = playerToFollow;
    }

    private void FollowPlayer()
    {
        agent.SetDestination(_playerToFollow.position);
        enemyAnimationController.PlayWalk(true);
    }

    public void SetReferenceToRewardController(RewardController rewardController)
    {
        _rewardControllerReference = rewardController;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        enemyAnimationController.PlayWalk(false);

        transform.LookAt(_playerToFollow);

        if (_alreadyAttacked) return;
        
        _playerToFollow.GetComponent<HealthManager>().TakeDamage(baseDamage);
        enemyAnimationController.PlayAttack();

        _alreadyAttacked = true;
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
    }
    
    private void DestroyEnemy() 
    {
        _rewardControllerReference.SpawnReward(enemyCollider.bounds.center);
        gameObject.SetActive(false);
    }
    private void ResetAttack()
    {
        _alreadyAttacked = false;
    }
}
