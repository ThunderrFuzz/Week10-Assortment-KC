using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public Shooting shootingScript;
   
    void Update()
    { 
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = "Ammo: " + shootingScript.currentAmmo + " / " + shootingScript.currentWeapon.ammoCapacity;
    }
}
