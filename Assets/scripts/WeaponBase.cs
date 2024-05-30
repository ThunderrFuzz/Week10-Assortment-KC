using UnityEngine;

[System.Serializable]
public class WeaponBase
{
    public string weaponName;
    public GameObject gunObject;
    public float fireRate;
    public float reloadTime;
    public int ammoCapacity;
    public int damage;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public float hitForce = 15f;
    public float range = 50f;
}
