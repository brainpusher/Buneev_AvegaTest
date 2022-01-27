using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] private RewardType rewardType;
    [SerializeField] private Text rewardText;

    public RewardType ViewRewardType => rewardType;
    
    public void UpdateScoreText(int score)
    {
        rewardText.text = score.ToString();
    }
}
