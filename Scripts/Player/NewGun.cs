using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewGun : MonoBehaviour {

    public enum GunType { Semi, Burst, Auto };
    public GunType gunType;
    public float rpm;
    public float damage = 10f;
    public float range = 100f;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    public int maxAmmo = 10;
    public int reserveAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Animator animator;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImpactEffect;
    public Text ammoCounter;

    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
        //reserveAmmo = Mathf.Clamp(30, 0, 300);
        currentAmmo = maxAmmo;
        AmmoCounter();
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update () {

        AmmoCounter();

        if (reserveAmmo <= 0)
        {
            reserveAmmo = 0;
        }

        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKey("r"))
        {
            StartCoroutine(Reload());
            return;
        }

        if (PauseMenu1.GameIsPaused)
        {
            return;
        }
 
        else if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1"))
        {
            ShootContinuous();
        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        reserveAmmo -= Mathf.Abs(maxAmmo - currentAmmo);

        currentAmmo = maxAmmo;

        isReloading = false;
    }

    void Shoot ()
    {
        if (CanShoot())
        {

            currentAmmo--;

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            MuzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

                nextPossibleShootTime = Time.time + secondsBetweenShots;

                GameObject impact = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
        }

    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }

        //if (reserveAmmo == 0 && currentAmmo == 1)
        if (reserveAmmo == 0 && currentAmmo == 1)
        {
            canShoot = false;
        }

        return canShoot;
    }

    void AmmoCounter()
    {
        ammoCounter.text = "Ammo: " + currentAmmo + " / " + reserveAmmo;
    }
}
