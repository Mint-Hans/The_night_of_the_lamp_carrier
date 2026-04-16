using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStats : MonoBehaviour
{
    public int maxEnergy;

    [SerializeField] private int currentEnergy;
    void Start()
    {
        currentEnergy = maxEnergy;
    }

    public void TakeConsumption(int _consumption)
    {
        currentEnergy -= _consumption;

        if (currentEnergy < 0)
            Tire();

    }
    private void Tire()
    {
        throw new NotImplementedException();
    }
}
