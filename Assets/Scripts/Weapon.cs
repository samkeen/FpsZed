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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzelFlash();
        AnalyzeRayTrace();
    }

    private void PlayMuzzelFlash()
    {
        muzzelFlash.Play();
    }

    private void AnalyzeRayTrace()
    {
        var fpTransform = FPCamera.transform;
        Physics.Raycast(fpTransform.position, fpTransform.forward, out var hit, range);
        hit.transform.GetComponent<EnemyHealth>();
        var enemyHealth = hit.transform.GetComponent<EnemyHealth>();
        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damagePerHit);
        }

        CreateHitImpact(hit);
    }

    private void CreateHitImpact(RaycastHit hitInfo)
    {
        // explode at 90deg (hitInfo.normal) to impacted surface, destroy effect after .1 sec
        GameObject impactEffect = Instantiate(hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(impactEffect, .1f);
    }
}