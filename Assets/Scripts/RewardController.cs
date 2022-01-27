using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private RewardViewController rewardViewController;

    private Dictionary<RewardType, int> _countersDictionary = new Dictionary<RewardType, int>();

    private ColorForRewardType _colorForRewardType;
    public Color RewardColor =>_colorForRewardType?.rewardColor ?? Color.white;
    
    public void RewardPickedUp(ColorForRewardType colorForRewardType, int pointsGainedOnPickUp)
    {
        audioSource.Play();
        _colorForRewardType = colorForRewardType;
        RewardType rewardType = _colorForRewardType.rewardType;
        
        if (_countersDictionary.ContainsKey(rewardType))
            _countersDictionary[rewardType] += pointsGainedOnPickUp;
        else
            _countersDictionary.Add(rewardType,pointsGainedOnPickUp);

        rewardViewController.UpdateScoreByType(rewardType,_countersDictionary[rewardType]);
    }

    public void SpawnReward(Vector3 position)
    {
        RewardItem rewardItem =  objectPooler.GetPooledObject(2).GetComponent<RewardItem>();
       rewardItem.SetReferenceToRewardController(this);
       rewardItem.gameObject.transform.position = position;
       rewardItem.gameObject.SetActive(true);
    }
}
