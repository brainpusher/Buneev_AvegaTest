using UnityEngine;

public class StatsViewController : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private HealthView healthView;
    [SerializeField] private AmmoView ammoView;
    private void OnEnable()
    { 
        healthManager.OnHealthChange += UpdateHealth;
    } 
    private void OnDisable()
    { 
        healthManager.OnHealthChange -= UpdateHealth;
    }
    
    public void UpdateHealth(float healthLeft)
    {
       healthView.UpdateHealth(healthLeft);
    } 
    public void UpdateAmmoCount(int ammoCount)
    {
       ammoView.UpdateAmmoCount(ammoCount);
    }
    public void SetAmmoToReload()
    {
       ammoView.SetReloading();
    }    
}
