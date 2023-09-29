using UnityEngine;

public class InteractionType : MonoBehaviour
{
    public enum interactionType
    {
        Click,
        Hold
    }

    public float holdTime;
    public bool done;
    KeyCode key = KeyCode.E;
    public float timeNeeded = 1f;

    private PlayerInteraction _playerInteraction;

    private void Start()
    {
        _playerInteraction = GameObject.Find("Mark").GetComponentInChildren<PlayerInteraction>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(key)) done = false;
    }

    public void HandleInteraction(interactionType currentType)
    {
        switch (currentType)
        {
            case interactionType.Click:
                if (Input.GetKeyDown(key))
                {
                    _playerInteraction.Interacte();
                }
                break;

            case interactionType.Hold:
                if (Input.GetKey(key) && !done)
                {
                    IncreasedHoldTime();

                    if (GetHoldTime() > timeNeeded)
                    {
                        _playerInteraction.Interacte();
                        done = true;
                        ResetHoldTime();
                    }
                }
                else
                {
                    ResetHoldTime();
                }
                break;
        }
    }

    private void IncreasedHoldTime()
    {
        holdTime += Time.deltaTime;
    }

    private void ResetHoldTime()
    {
        holdTime = 0;
    }

    private float GetHoldTime()
    {
        return holdTime;
    }

}
