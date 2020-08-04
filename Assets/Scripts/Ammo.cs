using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;

    public int GetAmmoAmount()
    {
        return ammoAmount;
    }

    public void ReduceAmmoAmount()
    {
        ammoAmount--;
    }
}
