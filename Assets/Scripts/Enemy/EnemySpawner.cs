using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RewardController rewardController;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform playerToFollow; 
    [SerializeField] private float enemySpawnTime = 3f;
    
    private EnemyAI currentEnemyAI;
    private NavMeshTriangulation _navMeshTriangulation;
    
    private void Start()
    {
        _navMeshTriangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemyWithDelay());
    }
  
    private IEnumerator SpawnEnemyWithDelay()
    {
        while(true)
        {

            yield return new WaitForSeconds(enemySpawnTime);
            currentEnemyAI =  ObjectPooler.SharedInstance.GetPooledObject(0).GetComponent<EnemyAI>();
            StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        bool isPointToSpawnFound = false;

        while (!isPointToSpawnFound)
        {
            int vertexIndex = Random.Range(0, _navMeshTriangulation.vertices.Length);
            
            //Check if we can find navmesh poisition by navmesh vertex position
            if (NavMesh.SamplePosition(_navMeshTriangulation.vertices[vertexIndex], out var navMeshHit, 2f, 1))
            {
                if (IsPositionOutsideCameraView(navMeshHit.position))
                {
                    isPointToSpawnFound = true;
                    currentEnemyAI.Agent.Warp(navMeshHit.position);
                    currentEnemyAI.Agent.enabled = true;
                }
            }
            
            yield return null;
                
            currentEnemyAI.SetPlayerToFollow(playerToFollow);
            currentEnemyAI.SetReferenceToRewardController(rewardController);
            currentEnemyAI.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Check if position is not in field of view of given camera
    /// </summary>
    private bool IsPositionOutsideCameraView(Vector3 position)
    {
        Vector3 screenPoint = playerCamera.WorldToViewportPoint(position);
        return screenPoint.z < 0 || !(screenPoint.x > 0 && screenPoint.x < 1) || !(screenPoint.y > 0 &&
            screenPoint.y < 1);
    }
}
