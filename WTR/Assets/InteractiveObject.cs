using Knife.HDRPOutline.Core;
using NOTLonely_Door;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour
{
    public OutlineObject outline;

    private bool isMouseOver = false;

    private void Start()
    {
        outline = GetComponent<OutlineObject>();
    }

    void Update()
    {
        if (isMouseOver)
        {
            outline.enabled = true;

            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("NomDeVotreScene");
            }
        }
        else
        {
            outline.enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}