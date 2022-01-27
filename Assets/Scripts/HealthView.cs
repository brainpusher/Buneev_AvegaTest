using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Text health;

    public void UpdateHealth(float healthLeft)
    {
        health.text = healthLeft.ToString();
    }
}
