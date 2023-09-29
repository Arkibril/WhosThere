using UnityEngine;

public class MouvementAnimation : MonoBehaviour
{
    public Animator anim;
    private FirstPersonController fpsScript;
    public float CrouchKeyPressed =  0f;

    private Vector3 moveDirection;
    public Transform Player;

    private PlayerManager _player;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        _player = GetComponent<PlayerManager>();
        fpsScript = GetComponent<FirstPersonController>();
    }

    void Animation()
    {
        groundedCheck();
        Jumping();
        Walking();
        Crounching();
        Diagonal();
        Sprint();

        FixCombinedTouch();
    }

    // Update is called once per frame
    void Update()
    {


        Animation();
        Inputs();

        anim.SetFloat("verticalSpeed", Input.GetAxisRaw("Vertical"), 0.1f, Time.deltaTime);
        anim.SetFloat("horizontalSpeed", Input.GetAxisRaw("Horizontal"), 0.1f, Time.deltaTime);

        if(anim.GetFloat("verticalSpeed") < 0 || anim.GetFloat("horizontalSpeed") < 0)
        {
            anim.SetBool("Run & Sprint", false);
        }

    }

    void Inputs()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        moveDirection = Player.forward * verticalInput + Player.right * horizontalInput;
    }

    void Crounching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetBool("Crouched", true);
            CrouchKeyPressed += 1;
        }

        if (CrouchKeyPressed == 2)
        {
            anim.SetBool("Crouched", false);
            CrouchKeyPressed = 0f;
        }

        if (CrouchKeyPressed == 1 && Input.GetButton("Vertical"))
        {
            anim.SetBool("CrouchingMouvement", true);
            anim.SetFloat("crouchedValue", 1f);
        }
        else if(CrouchKeyPressed == 1 && Input.GetButton("Horizontal"))
        {
            anim.SetBool("CrouchingMouvement", true);
            anim.SetFloat("crouchedValue", -1f);
        }
        else
        {
            anim.SetBool("CrouchingMouvement", false);
        }
    }

    void Walking()
    {
        // Avancer devant et derriere
        if (Input.GetButton("Vertical") || Input.GetButton("Vertical") && _player.m_walk)
        {
            anim.SetBool("Front & Back", true);
        }
        else
        {
            anim.SetBool("Front & Back", false);
        }

        // Avancer droite gauche
        if (Input.GetButton("Horizontal") || (Input.GetButton("Horizontal") && _player.m_walk))
        {
            anim.SetBool("Left & Right", true);
        }
        else
        {
            anim.SetBool("Left & Right", false);
        }


    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            anim.SetBool("Jumping", true);
        }
        else
        {
            anim.SetBool("Jumping", false);
        }

    }

    void groundedCheck()
    {
        if (fpsScript.m_isGrounded)
        {
            anim.SetBool("IsGrounded", true);
            anim.SetBool("InTheAir", false);
        }
        else
        {
            anim.SetBool("InTheAir", true);
            anim.SetBool("IsGrounded", false);
        }
    }

    void Diagonal()
    {
        if(Input.GetButton("Horizontal") && Input.GetButton("Vertical"))
        {
            anim.SetBool("Diagonal", true);
            DiagonalInit();
        }
        else
        {
            anim.SetBool("Diagonal", false);
        }
    }

    void DiagonalInit()
    {
        anim.SetBool("Left & Right", false);
        anim.SetBool("Front & Back", false);
    }

    void Sprint()
    {
        if(Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) && _player.m_sprint)
        {
            anim.SetBool("Run & Sprint", true);
            anim.SetFloat("sprintValue", 2, 0.1f, Time.deltaTime);
        }
        else if(Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift) && _player.m_sprint)
        {
            anim.SetBool("Run & Sprint", true);
            anim.SetFloat("sprintValue", 2, 0.1f, Time.deltaTime);
        }
        else if (Input.GetButton("Vertical") && Input.GetKey(KeyCode.LeftShift) && _player.m_run)
        {
            anim.SetBool("Run & Sprint", true);
            anim.SetFloat("sprintValue", 1, 0.1f, Time.deltaTime);
        }
        else if (Input.GetButton("Horizontal") && Input.GetKey(KeyCode.LeftShift) && _player.m_run)
        {
            anim.SetBool("Run & Sprint", true);
            anim.SetFloat("sprintValue", 1, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run & Sprint", false);
            anim.SetFloat("sprintValue", 0, 0.1f, Time.deltaTime);
        }
    }

    void FixCombinedTouch()
    {

        if(Input.GetButton("Vertical") && moveDirection == Vector3.zero)
        {
            anim.SetBool("Front & Back", false);
            anim.SetBool("Diagonal", false);
        }        
        else if(Input.GetButton("Horizontal") && moveDirection == Vector3.zero)
        {
            anim.SetBool("Left & Right", false);
            anim.SetBool("Diagonal", false);
        }

    }


}
