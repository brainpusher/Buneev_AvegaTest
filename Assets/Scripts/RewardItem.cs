using System.Collections.Generic;
using UnityEngine;

public class RewardItem : MonoBehaviour
{
    [SerializeField] private int pointsGainedOnPickUp = 1;
    [SerializeField] [TagSelector] private string tagToInteractWith;
    [SerializeField] private Renderer rewardRenderer;
    [SerializeField] private List<ColorForRewardType> availableColors;
    
    private Color CurrentColor { get; set; }
    private ColorForRewardType _currentColorForRewardType;
    private RewardController _rewardControllerReference;
    
    private void OnEnable()
    {
        _currentColorForRewardType = availableColors[UnityEngine.Random.Range(0, availableColors.Count)];
        CurrentColor = _currentColorForRewardType.rewardColor;
        
        ApplyColor();
    }

    private void ApplyColor()
    {
        rewardRenderer.material.color = CurrentColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag(tagToInteractWith)) return;
        
        _rewardControllerReference.RewardPickedUp(_currentColorForRewardType,pointsGainedOnPickUp);
        gameObject.SetActive(false);
    }
    
    public void SetReferenceToRewardController(RewardController rewardController)
    {
        _rewardControllerReference = rewardController;
    }
}
