using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    AudioSource _as;
    LineRenderer _lr;

    public WeaponBase currentWeapon;
    public bool isHoldingGun = false;
    Camera fpsCam;
    public Transform gunEnd;
    public Transform weaponHolder;
    float nextFire;
    public int currentAmmo;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _lr = GetComponent<LineRenderer>();
        fpsCam = FindAnyObjectByType<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isHoldingGun)
        {
            DropGun();
        }

        if (Input.GetKeyDown(KeyCode.R) && isHoldingGun)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire && currentAmmo > 0 && isHoldingGun)
        {
            nextFire = Time.time + currentWeapon.fireRate;
            StartCoroutine(ShootingEffects());
            currentAmmo--;

            Vector3 origin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0f));
            RaycastHit hit;
            _lr.SetPosition(0, gunEnd.position);
            if (Physics.Raycast(origin, fpsCam.transform.forward, out hit, currentWeapon.range))
            {
                
                _lr.SetPosition(1, hit.point);
                ShootableBox _box = hit.collider.GetComponent<ShootableBox>();
                if (_box != null)
                {
                    _box.Damage(currentWeapon.damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * currentWeapon.hitForce);
                }
            }
            else
            {
                _lr.SetPosition(1, fpsCam.transform.forward * currentWeapon.range);
            }
        }
    }

    IEnumerator ShootingEffects()
    {
        _as.PlayOneShot(currentWeapon.fireSound);
        _lr.enabled = true;
        yield return new WaitForSeconds(currentWeapon.fireRate);
        _lr.enabled = false;
    }

    IEnumerator Reload()
    {
        _as.PlayOneShot(currentWeapon.reloadSound);
        yield return new WaitForSeconds(currentWeapon.reloadTime);
        currentAmmo = currentWeapon.ammoCapacity;
    }

    void DropGun()
    {
        currentWeapon.gunObject.transform.parent = null;
        currentWeapon.gunObject.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon.gunObject.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
        isHoldingGun = false;
    }

    void PickUpGun(Weapon weapon)
    {
        isHoldingGun = true;
        currentWeapon = weapon.weaponBase;
        currentAmmo = currentWeapon.ammoCapacity;
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        gunEnd = weapon.transform.Find("GunEnd");
    }


    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon") && Input.GetKeyDown(KeyCode.E) && !isHoldingGun)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon != null)
            {
                PickUpGun(weapon);
            }
        }
    }
}
