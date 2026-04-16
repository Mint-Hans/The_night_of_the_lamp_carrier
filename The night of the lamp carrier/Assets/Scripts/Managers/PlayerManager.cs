using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Player player;

    public int currency;
    private void Awake()
    {
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    public int GetCurrency() => currency;

    public void LoadData(GameData _date)
    {
        this.currency = _date.currency;
    }

    public void SaveDate(ref GameData _date)
    {
        _date.currency = this.currency;
    }
}
