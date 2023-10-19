using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GateCheck : MonoBehaviour, IDataPersistence<FirstLaunchData>
{
   
    private bool first;

    private void Start()
    {

        DataPersistenceManager.dataHandler = new FileDataHandler(Application.persistentDataPath, "First_data", DataPersistenceManager.doIUseEncryption);
        DataPersistenceManager.instance.LoadFirst();
        DataPersistenceManager.instance.SaveFirst();



        if (first == true)
        {
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

        }
        else if(first == false)
        {
            first = true;
            DataPersistenceManager.instance.SaveFirst();

            SceneManager.LoadSceneAsync("Language", LoadSceneMode.Single);
            first = true;
        }
    }

    public void LoadData(FirstLaunchData data)
    {
        first = data.firstYes;
    }

    public void SaveData(ref FirstLaunchData data)
    {
        data.firstYes = first;
    }

    public void ChangeValue(bool index)
    {

        first = index;

        DataPersistenceManager.instance.SaveFirst();
    }

}
