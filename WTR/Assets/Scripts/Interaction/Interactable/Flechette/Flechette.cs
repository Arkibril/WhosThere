using UnityEngine;

public class Flechette : MonoBehaviour
{

    public GameObject player;
    public GameObject flechetteCamera;
    public GameObject pointsGO;

    public TMPro.TextMeshProUGUI[] scoreText;
    public GameObject[] GUI;
    public int GUIActivate = 0;
    public float holdTime;

    public UnityEngine.UI.Image[] V_Filler;

    public int total = 0;
    public int scoreNeeded;

    public bool done;

    private Vector3 screenPosition;
    private Vector3 worldposition;

    public GameObject[] flechette;
    public bool[] onWall;
    public bool[] inAir;
    private bool wait;

    public bool onIt;

    private void Start()
    {
        scoreNeeded = Random.Range(3, 180);
        for (int i = 0; i < flechette.Length; i++)
        {
            flechette[i].transform.position = flechetteCamera.transform.position + new Vector3(-1, 0, 0);
            flechette[i].GetComponent<Rigidbody>().useGravity = false;
            flechette[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            onWall[i] = false;
            inAir[i] = false;
        }

        pointsGO.SetActive(false);
        flechetteCamera.SetActive(false);
        GUI[0].SetActive(false);
        GUI[1].SetActive(false);

        for(int i = 0; i < V_Filler.Length; i++)
        {
            V_Filler[i].fillAmount = 0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        total = flechette[0].GetComponent<Fleche>().point + flechette[1].GetComponent<Fleche>().point + flechette[2].GetComponent<Fleche>().point;

        for(int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = total.ToString() + "/" + scoreNeeded.ToString();
        }
        
        if (onIt && Input.GetKeyDown(KeyCode.Escape)) Escape();

        if(!onWall[0] && !onWall[1] && !onWall[2] &&  onIt && total < scoreNeeded) FlechetteLunch(0);
        else if(onWall[0] && !onWall[1] && !onWall[2] && onIt && total < scoreNeeded) FlechetteLunch(1);
        else if(onWall[0] && onWall[1] && !onWall[2] && onIt && total < scoreNeeded) FlechetteLunch(2);

        if(onWall[0] && onWall[1] && onWall[2] && onIt && total < scoreNeeded)
        {
            for(int i = 0; i < flechette.Length; i++)
            {
                flechette[i].SetActive(false);
                flechette[i].GetComponent<Fleche>().hurt = false;
                onWall[i] = false;
            }
        }

        if(total >= scoreNeeded)
        {
            Escape();
            done = true;
        }

        if (done) GetComponentInChildren<MeshCollider>().enabled = false;

        if (onIt)
        {
            if (GUIActivate == 0)
            {
                GUI[0].SetActive(true);
                GUI[1].SetActive(false);
            }
            if (GUIActivate == 1)
            {
                GUI[0].SetActive(false);
                GUI[1].SetActive(true);
            }
        }

        OnWallVerification();
        TutoInput();
    }

    void Escape()
    {
        player.SetActive(true);
        flechetteCamera.SetActive(false);
        onIt = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pointsGO.SetActive(false);
    }

    void FlechetteLunch(int array)
    {
        flechette[array].SetActive(true);
        
        if (!inAir[array] && !onWall[array])
        {
            screenPosition = Input.mousePosition;
            screenPosition.z = flechetteCamera.GetComponent<Camera>().nearClipPlane + 0.5f;

            worldposition = flechetteCamera.GetComponent<Camera>().ScreenToWorldPoint(screenPosition);
            flechette[array].transform.position = worldposition;
            flechette[array].transform.localRotation = Quaternion.identity;
        }

        if (!inAir[array] && Input.GetButtonDown("Fire1") && !onWall[array])
        {
            flechette[array].GetComponent<Rigidbody>().AddForce(flechette[0].transform.forward * 2, ForceMode.Impulse);
            flechette[array].GetComponent<Rigidbody>().useGravity = true;
            inAir[array] = true;
            //flechette[array].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezeRotationX;
        }
    }

    void OnWallVerification()
    {
        if (flechette[0].GetComponent<Fleche>().hurt)
        {
            inAir[0] = false;
            onWall[0] = true;
        }
        
        if (flechette[1].GetComponent<Fleche>().hurt)
        {
            inAir[1] = false;
            onWall[1] = true;
        }

        if (flechette[2].GetComponent<Fleche>().hurt)
        {
            inAir[2] = false;
            onWall[2] = true;
        }
    }

    void TutoInput()
    {
        if (onIt)
        {

            if (Input.GetKeyUp(KeyCode.V))
            {
                wait = false;
                holdTime = 0f;
                if (GUIActivate == 0)
                {
                    V_Filler[0].fillAmount = 0;
                }
                else if (GUIActivate == 1)
                {
                    V_Filler[1].fillAmount = 0;
                }
            }

            if (Input.GetKey(KeyCode.V) && !wait)
            {
                holdTime += Time.deltaTime;
                if (GUIActivate == 0)
                {
                    V_Filler[0].fillAmount = holdTime;
                }
                else if (GUIActivate == 1)
                {
                    V_Filler[1].fillAmount = holdTime;
                }

                if (holdTime >= 1)
                {

                    if (GUIActivate == 0)
                    {
                        GUIActivate = 1;
                        wait = true;
                        holdTime = 0f;
                        V_Filler[0].fillAmount = 0;
                    }
                    else if (GUIActivate == 1)
                    {
                        GUIActivate = 0;
                        wait = true;
                        holdTime = 0f;
                        V_Filler[1].fillAmount = 0;
                    }
                }

            }
        }
    }

}
