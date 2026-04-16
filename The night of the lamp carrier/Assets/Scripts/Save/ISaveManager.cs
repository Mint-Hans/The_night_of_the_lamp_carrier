using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveManager
{
    void LoadData(GameData _date);
    void SaveDate(ref GameData _date);
}
