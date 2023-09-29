using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour, IDataPersistence<LanguageData>
{
    private int languageIndex;

    private void Start()
    {
        DataPersistenceManager.dataHandler = new FileDataHandler(Application.persistentDataPath, "language_data", DataPersistenceManager.doIUseEncryption);

        DataPersistenceManager.instance.LoadLanguage();

    }

    public void LoadData(LanguageData data)
    {
        languageIndex = data.languageIndex;
    }

    public void SaveData(ref LanguageData data)
    {
        data.languageIndex = languageIndex;
    }

    public void ChangeLanguage(int index)
    {
        // Modifier la valeur de languageIndex
        languageIndex = index;

        // Sauvegarder la langue après avoir changé la valeur de languageIndex
        DataPersistenceManager.instance.SaveLanguage();
    }

    
}
