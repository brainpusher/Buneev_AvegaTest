using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardViewController : MonoBehaviour
{
    [SerializeField] private List<RewardView> rewardViews;


    public void UpdateScoreByType(RewardType rewardType, int amount)
    {
        RewardView rewardView = GetRewardViewByType(rewardType);
        rewardView.UpdateScoreText(amount);
    }
    
    private RewardView GetRewardViewByType(RewardType rewardType)
    {
        return rewardViews.FirstOrDefault(rewardView => rewardView.ViewRewardType.Equals(rewardType));
    }
}
