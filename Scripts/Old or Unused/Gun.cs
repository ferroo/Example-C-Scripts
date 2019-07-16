using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {

    public enum GunType {Semi,Burst,Auto};
    public GunType gunType;
    public float rpm;
    public Gun gun;

    //Components
    public Transform spawn;
    private LineRenderer tracer;
    public int damagePerShot = 20;

    //System
    private float secondsBetweenShots;
    private float nextPossibleShootTime;

    private void Start()
    {
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            gun.Shoot();
        }
        else if (Input.GetButton("Fire1"))
        {
            gun.ShootContinuous();
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;
            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                EnemyController enemyHealth = hit.collider.GetComponent<EnemyController>();

                // If the EnemyHealth component exist...
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    //enemyHealth.TakeDamage(damagePerShot, hit.point);
                }
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }


            Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
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

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return new WaitForSeconds(.019f);
        tracer.enabled = false;
    }

}
