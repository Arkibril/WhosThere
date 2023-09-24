using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{
    public GameObject _menuCanva;
    public static bool _isMenuOpen = false;
    public Slider sensitivitySlider;
    public CameraController cam;

    // Two Menu GameObject
    public GameObject _main;
    public GameObject _option;
    void Start()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
        cam.ChangeSensitivity(sensitivitySlider.value);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            _isMenuOpen = !_isMenuOpen;
            _menuCanva.SetActive(_isMenuOpen);
            if(_isMenuOpen){
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }else{
                ReturnToGame();
            }

        }

    }

    void Sensitivity(){
        cam.ChangeSensitivity(sensitivitySlider.value);
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
    }

    private void GameCursor(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ReturnToGame(){
        Sensitivity();
        GameCursor();
        // Load Menu Scene
    }

    public void OnQuitButton(){
        ReturnToGame();
        // Load Main Menu Scene
    }
    public void OnContinueButton(){
        ReturnToGame();
        _isMenuOpen = false;
        _menuCanva.SetActive(true);
    }

    private void SwitchMenu(){
        _main.SetActive(true);
        _option.SetActive(false);
    }

}
