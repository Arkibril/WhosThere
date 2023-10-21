using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScreen : MonoBehaviour, IDataPersistence<PlayerData>
{

    private bool isNewPlayer;
    private string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        DataPersistenceManager.dataHandler = new FileDataHandler(Application.persistentDataPath, "player_data", DataPersistenceManager.doIUseEncryption);
        StartCoroutine(NewPlayerCheck());
    }

    public void LoadData(PlayerData data){
        isNewPlayer = data.isNewPlayer;
    }

    public void SaveData(ref PlayerData data){
        data.isNewPlayer = isNewPlayer;
    }

    IEnumerator NewPlayerCheck(){
        yield return new WaitForSeconds(0.5f);
        DataPersistenceManager.instance.LoadPlayerData();
        yield return new WaitForSeconds(1);
        if (isNewPlayer){
            sceneToLoad = "Language";
            isNewPlayer = false;
            DataPersistenceManager.instance.SavePlayerData();
        }
        else{
            sceneToLoad = "MainMenu";
        }
    }

    public void ChangeScene(){
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
