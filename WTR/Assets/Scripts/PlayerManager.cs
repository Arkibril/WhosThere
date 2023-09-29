using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float sprint = 100f;
    public float maxSprint = 100f;

    private FirstPersonController _firstPersonController;

    private const string VerticalAxis = "Vertical";
    private const string HorizontalAxis = "Horizontal";
    private const KeyCode SprintKey = KeyCode.LeftShift;

    private const float RunLevel = 20.0f;
    private const float WalkLevel = 5.0f;

    public bool m_sprint;
    public bool m_run;
    public bool m_walk;

    public void Start()
    {
        _firstPersonController = GetComponent<FirstPersonController>();
    }

    public void Update()
    {
        SprintOrRun();
        Run();
        RegenSprintBar();
    }

    private void Run()
    {
        if (sprint > 0 && !_firstPersonController.movementInputData.IsCrouching)
        {
            if (Input.GetButton(VerticalAxis) && Input.GetKey(SprintKey) || Input.GetButton(HorizontalAxis) && Input.GetKey(SprintKey))
            {
                sprint -= 10 * Time.deltaTime;
            }
        }
    }

    private void SprintOrRun()
    {
        if (sprint > RunLevel)
        {
            m_sprint = true;
            m_run = false;
            m_walk = false;
        }
        else if (sprint < RunLevel && sprint > WalkLevel)
        {
            m_sprint = false;
            m_run = true;
            m_walk = false;
        }
        else if (sprint <= WalkLevel)
        {
            m_sprint = false;
            m_run = false;
            m_walk = true;
        }
    }

    private void RegenSprintBar()
    {
        if (m_walk || (sprint != maxSprint && _firstPersonController.m_currentSpeed <= _firstPersonController.walkSpeed) || (sprint != maxSprint && _firstPersonController.m_currentSpeed == 0))
        {
            StartCoroutine(Regeneration());
        }
        else if (sprint > RunLevel)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Regeneration()
    {
        yield return new WaitForSeconds(2f);
        if (sprint <= maxSprint)
        {
            sprint += 20 * Time.deltaTime;
        }
    }
}
