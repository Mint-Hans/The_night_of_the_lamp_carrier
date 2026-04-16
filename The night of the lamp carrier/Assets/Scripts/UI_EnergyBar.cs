using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnergyBar : MonoBehaviour
{
    [Header("主要统计")]
    public Stat vitality;

    private Entity entity;
    private CharacterStats myStats;
    private RectTransform myTransform;
    private Slider Energyslider;
    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        Energyslider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<CharacterStats>();

        entity.onFlipped += FlipUI;
        myStats.onEnergyChange += UpdateEnergyUI;
    }
    private void UpdateEnergyUI()
    {
        Energyslider.maxValue = myStats.GetMaxEnergyValue();
        Energyslider.value = myStats.currentEnergy;
    }

    private void FlipUI() => myTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
        myStats.onEnergyChange -= UpdateEnergyUI;
    }
}
