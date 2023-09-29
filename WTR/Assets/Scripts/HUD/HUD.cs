using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{

    public GameObject Player;
    private PlayerManager _player;
    public Image SprintBarLevelImage;
    public GameObject menuEchap;
    private bool isMenuOpen;
    public GameObject menuOption;

    public CameraController cameraController;

    int menuState;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<PlayerManager>();
        menuEchap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.m_sprint || _player.m_run) SprintBar(); 
        if(Input.GetKeyDown(KeyCode.Escape)) StateManager();

    }

    public void StateManager()
    {
        isMenuOpen = !isMenuOpen;
        menuEchap.SetActive(isMenuOpen);
        Cursor.visible = isMenuOpen;
        Cursor.lockState = isMenuOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        cameraController.enabled = !isMenuOpen;
    }

    public void ResetLockState()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SprintBar()
    {
        SprintBarLevelImage.fillAmount = _player.sprint / _player.maxSprint;

        _player.sprint = Mathf.Clamp(_player.sprint, 0f, _player.maxSprint);
    }
}

