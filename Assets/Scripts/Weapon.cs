using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        var fpTransform = FPCamera.transform;
        Physics.Raycast(fpTransform.position, fpTransform.forward, out hit, range);
        Debug.Log($"I hit {hit.transform.name}");
    }
}