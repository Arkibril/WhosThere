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
    public List<IDataPersistence<LanguageData>> languageDataObjetcs;

    public FirstLaunchData firstLaunchData;
    public List<IDataPersistence<FirstLaunchData>> firstLaunchDataObjects;

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
        this.languageDataObjetcs = FindAllLanguageData();
        this.firstLaunchDataObjects = FindAllFirstLaunchData();

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

        foreach (IDataPersistence<LanguageData> dataPersistenceObj in languageDataObjetcs)
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

        foreach (IDataPersistence<LanguageData> dataPersistenceObj in languageDataObjetcs)
        {
            dataPersistenceObj.SaveData(ref languageData);
        }

        dataHandler.Save(languageData);
    }

    public void NewFirstLaunch()
    {
        this.firstLaunchData = new FirstLaunchData();
    }

    public void LoadFirst()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot load FirstLaunchData.");
            return;
        }

        this.firstLaunchData = dataHandler.Load<FirstLaunchData>();

        if (this.firstLaunchData == null)
        {
            Debug.Log("Aucun data n'a été trouvé. Initialisation des data par défauts");
            NewFirstLaunch();
        }

        foreach (IDataPersistence<FirstLaunchData> dataPersistenceObj in firstLaunchDataObjects)
        {
            dataPersistenceObj.LoadData(firstLaunchData);
        }
    }

    public void SaveFirst()
    {
        if (dataHandler == null)
        {
            Debug.LogError("DataHandler is not assigned. Cannot save FirstLaunchData.");
            return;
        }

        foreach (IDataPersistence<FirstLaunchData> dataPersistenceObj in firstLaunchDataObjects)
        {
            dataPersistenceObj.SaveData(ref firstLaunchData);
        }

        dataHandler.Save(firstLaunchData);
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
        List<IDataPersistence<LanguageData>> languageDataObjetcs = new List<IDataPersistence<LanguageData>>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IDataPersistence<LanguageData> languageDataObj)
            {
                languageDataObjetcs.Add(languageDataObj);
            }
        }

        return languageDataObjetcs;
    }

    private List<IDataPersistence<FirstLaunchData>> FindAllFirstLaunchData()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IDataPersistence<FirstLaunchData>> firstLaunchDataObjects = new List<IDataPersistence<FirstLaunchData>>();

        foreach (var monoBehaviour in monoBehaviours)
        {
            if (monoBehaviour is IDataPersistence<FirstLaunchData> firstLaunchDataObj)
            {
                firstLaunchDataObjects.Add(firstLaunchDataObj);
            }
        }

        return firstLaunchDataObjects;
    }
}
