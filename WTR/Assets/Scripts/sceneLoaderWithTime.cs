using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneLoaderWithTime : MonoBehaviour
{
    public string sceneName;
    public float Temp;
    void Start()
    {
        StartCoroutine(ChangeSceneWithDelay());

    }

    private IEnumerator ChangeSceneWithDelay()
    {
        yield return new WaitForSeconds(Temp);
        SceneManager.LoadScene(sceneName);
    }
}

