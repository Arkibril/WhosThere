using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("State")]
    public bool isLocked;
    public bool isOpen;
    public bool isIn;
    public GameObject chairOnDoor = null;

    [Header("Controls")]
    public float openingSpeed;
    public float closingSpeed;
    [Range(0, 1)]
    public float closeStartAt;
    [Range(0, 1)]
    public float openStartAt;

    [Header("Animations")]
    public string openingAnim;
    public Animation anim;

    [Header("Sounds")]
    public AudioClip Open;
    public AudioClip Close;
    public AudioClip Locked;
    public AudioSource audio;

    private void Update()
    {
        anim = GetComponent<Animation>();
        audio = GetComponent<AudioSource>();
        CheckIfLocked();
        Init();
    }

    public void Sounds()
    {
        if (!isOpen) 
        {
            audio.clip = Close;
            audio.Play();
        }
        else
        {
            audio.clip = Open;
            audio.Play();
        }
    }

    void CheckIfLocked()
    {
        if (chairOnDoor != null && !chairOnDoor.GetComponent<Chair>().onDoor)
        {
            chairOnDoor = null;
        }

        if (chairOnDoor != null)
        {
            isLocked = true;
        }
        else
        {
            isLocked = false;
        }
    }

    private void Init()
    {
        if (gameObject.name == "DoorRoom")
        {
            openingSpeed = 1;
            closingSpeed = -1.3f;
            closeStartAt = 0.539f;
            openStartAt = 0f;

            openingAnim = "Door_open";
        }
        if(gameObject.name == "Window")
        {
            openingSpeed = 1;
            closingSpeed = -1;
            closeStartAt = 1;
            openStartAt = 0;

            openingAnim = "Window_open";
        }
        if(gameObject.name == "DoorMain")
        {
            openingSpeed = 1;
            closingSpeed = -1.3f;
            closeStartAt = 0.539f;
            openStartAt = 0f;
            isLocked = true;

            openingAnim = "Door_open";
        }
        if(gameObject.name == "Drawer")
        {
            openingSpeed = 1;
            closingSpeed = -1;
            closeStartAt = 1;
            openStartAt = 0;

            openingAnim = "Drawer";
        }
        if(gameObject.name == "Door_Bottom")
        {
            openingSpeed = 1;
            closingSpeed = -1;
            closeStartAt = 0.5f;
            openStartAt = 0;

            openingAnim = "KitchenDoor";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mark") isIn = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Mark") isIn = false;
    }
}

