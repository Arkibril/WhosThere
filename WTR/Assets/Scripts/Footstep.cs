using UnityEngine;

public class Footstep : MonoBehaviour
{
    private FirstPersonController firstPersonController;
    private PlayerManager _player;
    public AudioSource audio;
    public AudioSource audioRun;
    public bool isRuning;
    void Start()
    {
        firstPersonController = this.GetComponent<FirstPersonController>();
        _player = this.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) && _player.m_sprint == true)
        {
            isRuning = true;
        }
        else 
        {
            isRuning = false;
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Horizontal") && audio.isPlaying == false && isRuning == false)
        {
            audio.volume = Random.Range(0.1f, 0.3f);
            audio.pitch = Random.Range(0.5f, 1.2f);
            audio.Play();

            if (isRuning) 
            {
                audio.volume = Random.Range(0.1f, 0.3f);
                audio.pitch = Random.Range(0.5f, 1.2f);
                audioRun.Play();
            }
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Vertical") && audio.isPlaying == false && isRuning == false)
        {
            audio.volume = Random.Range(0.1f, 0.3f);
            audio.pitch = Random.Range(0.5f, 1.2f);
            audio.Play();
        }

        if (firstPersonController.GetComponent<FirstPersonController>().m_isGrounded == true && Input.GetButton("Vertical") && audioRun.isPlaying == false && isRuning == true)
        {
            audioRun.volume = Random.Range(0.1f, 0.3f);
            audioRun.pitch = Random.Range(0.9f, 1.5f);
            audioRun.Play();
        }
    }
}
