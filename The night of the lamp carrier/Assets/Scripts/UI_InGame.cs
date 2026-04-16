using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    [Header("Health UI")]
    [SerializeField] private Slider healthSlider;

    [Header("Energy UI")]
    [SerializeField] private Slider energySlider;

    void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHealthChange += UpdateHealthUI;
            playerStats.onEnergyChange += UpdateEnergyUI;
        }


    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = playerStats.GetMaxHealthValue();
            healthSlider.value = playerStats.currentHealth;
        }
    }
    private void UpdateEnergyUI()
    {
        if (energySlider != null)
        {
            energySlider.maxValue = playerStats.GetMaxEnergyValue();
            energySlider.value = playerStats.currentEnergy;
        }
    }
}