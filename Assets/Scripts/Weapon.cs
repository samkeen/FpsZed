using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damagePerHit = 20f;
    [SerializeField] private ParticleSystem muzzelFlash;
    // we use GameObject rather than ParticleSystem so we can intanciate and destroy this object
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private float timeBetweenShots = 0.5f;
    private bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetAmmoAmount(ammoType)>=1)
        {
            PlayMuzzelFlash();
            AnalyzeRayTrace();
            ammoSlot.ReduceAmmoAmount(ammoType);
        }
        // return control to thread, return here in `timeBetweenShots` seconds
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzelFlash()
    {
        muzzelFlash.Play();
    }

    private void AnalyzeRayTrace()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damagePerHit);
        }
    }

    private void CreateHitImpact(RaycastHit hitInfo)
    {
        // explode at 90deg (hitInfo.normal) to impacted surface, destroy effect after .1 sec
        GameObject impactEffect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(impactEffect, .1f);
    }
}