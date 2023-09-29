using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        PlayerPrefs.SetInt("Language", 0);
    }

    public void SetLanguage(int language) 
    {
        PlayerPrefs.SetInt("Language", language);
    }
}
