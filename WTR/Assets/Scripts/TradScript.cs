using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class TradScript : MonoBehaviour, IDataPersistence<LanguageData>
{
    public Text text;

    public string FR;
    public string EN;

    public void LoadData(LanguageData data)
    {
        
    }

    public void SaveData(ref LanguageData data)
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        // Acc�der au FileDataHandler via la propri�t� dataHandler
        FileDataHandler handler = DataPersistenceManager.dataHandler;

        // R�cup�rer les donn�es de langue depuis le handler
        LanguageData languageData = handler.Load<LanguageData>();

        text = this.gameObject.GetComponent<Text>();

        if (languageData.languageIndex == 1)
        {
            text.text = FR;
        }
        else if(languageData.languageIndex == 2)
        {
            text.text = EN;
        }
    }


}
