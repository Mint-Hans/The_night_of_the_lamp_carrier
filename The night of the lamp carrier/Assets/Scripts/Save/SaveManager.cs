using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDateHander dataHandler;

    public static SaveManager instance;

    [SerializeField] private string fileName;

    [ContextMenu("Delete save file")]
    public void DeleteSavedData()
    {
        dataHandler = new FileDateHander(Application.persistentDataPath,fileName);
        dataHandler.Delete();
    }
    private void Start()
    {
        dataHandler = new FileDateHander(Application.persistentDataPath,fileName);
        saveManagers = FindAllSaveManagers();

        LoadGame(); 
    }
    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (this.gameData == null)
        { 
            NewGame();
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveDate(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }
}
