using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float zoomedOutFov = 60f;
    [SerializeField] private float zoomedInFov = 20f;
    [SerializeField] private float zoomedOutMouseSensitivity = 2f;
    [SerializeField] private float zoomedInMouseSensitivity = .5f;

    private RigidbodyFirstPersonController fpsController;

    private bool zoomedInToggle = false;

    private void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                zoomedInToggle = true;
                fpsCamera.fieldOfView = zoomedInFov;
                fpsController.mouseLook.XSensitivity = zoomedInMouseSensitivity;
                fpsController.mouseLook.YSensitivity = zoomedInMouseSensitivity;
            }
            else
            {
                zoomedInToggle = false;
                fpsCamera.fieldOfView = zoomedOutFov;
                fpsController.mouseLook.XSensitivity = zoomedOutMouseSensitivity;
                fpsController.mouseLook.YSensitivity = zoomedOutMouseSensitivity;
            }
        }
    }
}
