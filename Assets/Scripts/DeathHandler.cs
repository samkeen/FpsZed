using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
        
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        // Stop time then give the player the mouse pointer back
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
