using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Configuration des fichiers data")]
    [SerializeField] private bool useEncryption;
    public static bool doIUseEncryption;

    private PlayerStyleData playerStyleData;
    private List<IDataPersistence<PlayerStyleData>> playerStyleDataObjects;
    public static FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    public LanguageData languageData;
    private List<IDataPersistence<LanguageData>> languageDataObjects;

    public PlayerData playerData;
    private List<IDataPersistence<PlayerData>> playerDataObjects;

    private void Awake()
    {
        doIUseEncryption = useEncryption;
        if (instance != null && instance != this)
        {
            Debug.LogError("Il existe déjà un data manager dans la scène");
            Destroy(gameObject); // If another instance already exists, destroy this one
            return;
        }

        instance = this;
    }

    private void Start()
    {
        this.playerStyleDataObjects = FindAllPlayerStyleData();
        this.languageDataObjects = FindAllLanguageData();
        this.playerDataObjects = FindAllPlayerData();

    }

    public void NewPlayerStyle()
    {
        this.playerStyleData = new PlayerStyleData();
    }

    public void LoadPlayerStyle()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot load PlayerStyleData.");
            return;
        }

        this.playerStyleData = dataHandler.Load<PlayerStyleData>();

        if (this.playerStyleData == null)
        {
            Debug.Log("Aucun data n'a été trouvé. Initialisation des data par défauts");
            NewPlayerStyle();
        }

        foreach (IDataPersistence<PlayerStyleData> dataPersistenceObj in playerStyleDataObjects)
        {
            dataPersistenceObj.LoadData(playerStyleData);
        }
    }

    public void SavePlayerStyle()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot save PlayerStyleData.");
            return;
        }

        foreach (IDataPersistence<PlayerStyleData> dataPersistenceObj in playerStyleDataObjects)
        {
            dataPersistenceObj.SaveData(ref playerStyleData);
        }

        dataHandler.Save(playerStyleData);
    }

    public void NewLanguage()
    {
        this.languageData = new LanguageData();
    }

    public void LoadLanguage()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot load LanguageData.");
            return;
        }

        this.languageData = dataHandler.Load<LanguageData>();

        if (this.languageData == null)
        {
            Debug.Log("Aucun data n'a été trouvé. Initialisation des data par défauts");
            NewLanguage();
        }

        foreach (IDataPersistence<LanguageData> dataPersistenceObj in languageDataObjects)
        {
            dataPersistenceObj.LoadData(languageData);
        }
    }

    public void SaveLanguage()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot save LanguageData.");
            return;
        }

        foreach (IDataPersistence<LanguageData> dataPersistenceObj in languageDataObjects)
        {
            dataPersistenceObj.SaveData(ref languageData);
        }

        dataHandler.Save(languageData);
    }

    public void NewPlayerData()
    {
        this.playerData = new PlayerData();
    }

    public void LoadPlayerData()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot load PlayerData.");
            return;
        }

        this.playerData = dataHandler.Load<PlayerData>();

        if (this.playerData == null)
        {
            Debug.Log("Aucun data n'a été trouvé. Initialisation des data par défauts");
            NewPlayerData();
        }

        foreach (IDataPersistence<PlayerData> dataPersistenceObj in playerDataObjects)
        {
            dataPersistenceObj.LoadData(playerData);
        }
    }

    public void SavePlayerData()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot save PlayerData.");
            return;
        }

        foreach (IDataPersistence<PlayerData> dataPersistenceObj in playerDataObjects)
        {
            dataPersistenceObj.SaveData(ref playerData);
        }

        dataHandler.Save(playerData);
    }

    private List<IDataPersistence<PlayerStyleData>> FindAllPlayerStyleData()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IDataPersistence<PlayerStyleData>> playerStyleDataObjects = new List<IDataPersistence<PlayerStyleData>>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IDataPersistence<PlayerStyleData> playerStyleDataObj)
            {
                playerStyleDataObjects.Add(playerStyleDataObj);
            }
        }

        return playerStyleDataObjects;
    }

    private List<IDataPersistence<LanguageData>> FindAllLanguageData()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IDataPersistence<LanguageData>> languageDataObjects = new List<IDataPersistence<LanguageData>>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IDataPersistence<LanguageData> languageDataObj)
            {
                languageDataObjects.Add(languageDataObj);
            }
        }

        return languageDataObjects;
    }

    private List<IDataPersistence<PlayerData>> FindAllPlayerData()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IDataPersistence<PlayerData>> playerDataObjects = new List<IDataPersistence<PlayerData>>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IDataPersistence<PlayerData> playerDataObj)
            {
                playerDataObjects.Add(playerDataObj);
            }
        }

        return playerDataObjects;
    }

}
