using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private StatsViewController statsViewController;
    [SerializeField] private RewardController rewardController;
    [SerializeField] private FP_Input playerInput;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float shootRate = 0.25F;
    [SerializeField] private float reloadTime = 1.0F;
    [SerializeField] private int ammoCount = 15;
    [SerializeField] private float baseDamage = 50f;

    private int ammo;
    private float delay;
    private bool reloading;
    private BulletProjectile _currentBulletProjectile;
    
    private void Start() 
    {
        ammo = ammoCount;
        statsViewController.UpdateAmmoCount(ammoCount);
    }
	
    private void Update() 
    {
        if(playerInput.Shoot())                         //IF SHOOT BUTTON IS PRESSED (Replace your mouse input)
            if(Time.time > delay)
                Shoot();

        if (playerInput.Reload())                        //IF RELOAD BUTTON WAS PRESSED (Replace your keyboard input)
            if (!reloading && ammoCount < ammo)
                StartCoroutine(Reload());
    }

    private void Shoot()
    {
        if (ammoCount > 0)
        {
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            var targetPoint = Physics.Raycast(ray, out var hit) ? hit.point : ray.GetPoint(50); 
            
            Vector3 directionWithoutSpread = targetPoint - firePoint.position;
            
            _currentBulletProjectile = ObjectPooler.SharedInstance.GetPooledObject(1).GetComponent<BulletProjectile>();
            
            _currentBulletProjectile.InitBullet(firePoint, baseDamage,rewardController.RewardColor);
            _currentBulletProjectile.gameObject.SetActive(true);
            _currentBulletProjectile.LaunchBullet(directionWithoutSpread);
            audioSource.Play();
            ammoCount--;
            statsViewController.UpdateAmmoCount(ammoCount);
        }
        else
            Debug.Log("Empty");

        delay = Time.time + shootRate;
    }

    private IEnumerator Reload()
    {
        reloading = true;
     //   Debug.Log("Reloading");
        statsViewController.SetAmmoToReload();
        yield return new WaitForSeconds(reloadTime);
        ammoCount = ammo;
        statsViewController.UpdateAmmoCount(ammoCount);
     //   Debug.Log("Reloading Complete");
        reloading = false;
    }

  /*  private void OnGUI()
    {
        GUILayout.Label("AMMO: " + ammoCount);
    }*/
}
