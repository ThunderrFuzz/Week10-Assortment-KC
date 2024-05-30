using UnityEngine;

public class MyPlayer : MonoBehaviour
{
    public WeaponBase pistol;
    //public WeaponBase rifle;

    Shooting shootingScript;

    void Start()
    {
        shootingScript = GetComponent<Shooting>();
        EquipWeapon(pistol);  
    }

    void EquipWeapon(WeaponBase weaponBase)
    {
        shootingScript.currentWeapon = weaponBase;
        shootingScript.gunEnd = weaponBase.gunObject.transform.Find("GunEnd");
        shootingScript.isHoldingGun = true;
        shootingScript.currentAmmo = weaponBase.ammoCapacity;
    }
}
