using UnityEngine;
using UnityEngine.UI;

public class AmmoView : MonoBehaviour
{
    [SerializeField] private Text ammo;
    [SerializeField] private string reloadingText = "Reloading...";
    public void UpdateAmmoCount(int ammoLeft)
    {
        ammo.text = ammoLeft.ToString();
    }

    public void SetReloading()
    {
        ammo.text = reloadingText;
    }
}
