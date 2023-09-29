using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Knife.HDRPOutline.Core;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Object")]
    [SerializeField]
    private GameObject viewObject = null;
    [SerializeField]
    public GameObject selectedObject = null;
    public GameObject interactionCanvas;
    private OutlineObject _outlineObject = null;

    [Header("Crosshair")]
    public GameObject crosshair;
    public GameObject crosshairInteract;
    public GameObject crosshairLevel;

    [Header("References")]
    public Chair _chair;
    private ChairIndication _chairIndication;
    private Door _door;
    private Levier _levier;
    private Torche _torche;
    private RecordPlayer _recordPlayer;
    private Toaster _toaster;
    [SerializeField]
    private Toast _toast;
    private BreadPlate _breadPlate;
    private Flechette _flechette;
    public InteractionType _interactionType;
    private Washer _washer;
    private Dryer _dryer;
    private PileOfClothes _clothes;
    private Fireplace _fireplace;
    private TaskManager _taskManager;

    //private TMPro.TextMeshProUGUI interacationText;
    private GameObject chairPoint;
    private GameObject torchePoint;
    private GameObject Mark;
    private GameObject cameraPivot;

    private Vector3 chairPos;
    private Quaternion chairRot;
    private Vector3 torchePos;
    private Quaternion torcheRot;

    [Header("Value")]
    public float interactionDistance;
    private RaycastHit hit;

    private void Start(){
        //interacationText = interactionCanvas.GetComponent<TMPro.TextMeshProUGUI>();
        chairPoint = GameObject.Find("Chaise Point");
        torchePoint = GameObject.Find("Torche Point");
        cameraPivot = GameObject.Find("Camera_Pivot");
        Mark = GameObject.Find("Mark");

        _interactionType = GetComponent<InteractionType>();
    }

    private void Update()
    {

        chairRot = Mark.transform.rotation;
        chairPos = chairPoint.transform.position;

        torcheRot = Quaternion.Euler(cameraPivot.transform.rotation.eulerAngles.x - 13, Mark.transform.rotation.eulerAngles.y - 3, 0);
        torchePos = torchePoint.transform.position;        

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (viewObject != null)
            {
                if(viewObject.CompareTag("Chair")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.CompareTag("Door") && selectedObject == null || viewObject.CompareTag("Door") && selectedObject != null && !selectedObject.CompareTag("Chair")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Flashlight") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "FuseBox") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.name == "RecordPlayer_LOD0" || viewObject.name == "Turntable_LOD0") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.CompareTag("Window")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Drawer" || viewObject.name == "Door_Bottom") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Toaster") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.CompareTag("Toast")) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "BreadPlate_A_LOD0") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "DartsTarget" && _flechette != null && !_flechette.done) _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Washer") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.name == "Dryer") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.name == "PileOfClothes") _interactionType.HandleInteraction(InteractionType.interactionType.Click);
                if (viewObject.name == "Fireplace") _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                if (viewObject.name == "TaskPaper") _interactionType.HandleInteraction(InteractionType.interactionType.Click);


                if (viewObject.CompareTag("Door") && selectedObject != null && selectedObject.CompareTag("Chair"))
                {
                    if(_door != null && _door.isIn && !_door.isOpen && _door.anim[_door.openingAnim].time <= 0)
                    {
                        _interactionType.HandleInteraction(InteractionType.interactionType.Hold);
                        if (_chairIndication != null) _chairIndication.ActivateRenderer();
                    }
                }
            }
        }

        if (viewObject == null || selectedObject == null|| _door != null && !_door.isIn)
        {
            if (_chairIndication != null) _chairIndication.DesactivateRenderer();
        }

        crosshair.SetActive(true);
        crosshairInteract.SetActive(false);
        crosshairLevel.SetActive(false);

        crosshairLevel.GetComponent<Image>().fillAmount = _interactionType.holdTime / _interactionType.timeNeeded;

        if(_chair != null && _chair.isTake) _chair.transform.SetPositionAndRotation(chairPos, chairRot);
        if(_torche != null && _torche.isTake) _torche.transform.SetPositionAndRotation(torchePos, torcheRot);      
        if(_toast != null && _toast.isTake) _toast.transform.position = torchePos;
        if(_clothes != null && _clothes.isTake) _clothes.transform.SetPositionAndRotation(chairPos, chairRot);

        if(viewObject != null)
        {
            if (viewObject.name == "Fireplace") _interactionType.timeNeeded = 5f;
            else _interactionType.timeNeeded = 1f;

        }

        if (_toaster != null)
        {
            if (viewObject != null && viewObject.name == "Toaster") _toaster.instructionCanvas.SetActive(true);
            else if (viewObject != null && viewObject.name != "Toaster") _toaster.instructionCanvas.SetActive(false);
            else if (viewObject == null) _toaster.instructionCanvas.SetActive(false);
        }

        if(_washer != null)
        {
            if (viewObject != null && viewObject.name == "Washer") _washer.instructionCanvas.SetActive(true);
            else if (viewObject != null && viewObject.name != "Washer") _washer.instructionCanvas.SetActive(false);
            else if (viewObject == null) _washer.instructionCanvas.SetActive(false);
        }

        ScriptReferences();
    }


    void ScriptReferences()
    {
        WhatISee();

        if(viewObject != null)
        {
            _chair = viewObject.GetComponent<Chair>();
            _door = viewObject.GetComponentInParent<Door>();
            _chairIndication = viewObject.GetComponentInChildren<ChairIndication>();
            _levier = viewObject.GetComponent<Levier>();
            _torche = viewObject.GetComponent<Torche>();
            _recordPlayer = viewObject.GetComponentInParent<RecordPlayer>();
            _toaster = viewObject.GetComponent<Toaster>();
            _toast = viewObject.GetComponent<Toast>();
            _breadPlate = viewObject.GetComponentInParent<BreadPlate>();
            _flechette = viewObject.GetComponentInParent<Flechette>();
            _washer = viewObject.GetComponent<Washer>();
            _dryer = viewObject.GetComponent<Dryer>();
            _clothes = viewObject.GetComponent<PileOfClothes>();
            _fireplace = viewObject.GetComponent<Fireplace>();
            _taskManager = viewObject.GetComponent<TaskManager>();
        }
        else
        {
            _chair = null;
            _door = null;
            //_chairIndication = null;
            _levier = null;
            _torche = null;
            _recordPlayer = null;
            _toast = null;
            //_washer = null;
            _dryer = null;
            _clothes = null;
            //_breadPlate = null;
            //_fireplace = null;
        }

        if (selectedObject != null && selectedObject.CompareTag("Chair") || selectedObject != null && selectedObject.name == "Flashlight" || selectedObject != null && selectedObject.CompareTag("Toast") || selectedObject != null && selectedObject.name == "PileOfClothes")
        {
            _chair = selectedObject.GetComponent<Chair>();
            _torche = selectedObject.GetComponent<Torche>();
            _toast = selectedObject.GetComponent<Toast>();
            _clothes = selectedObject.GetComponent<PileOfClothes>();
        }
    }

    void WhatISee()
    {
        if(hit.transform != null)
        {
            
            if (hit.transform.CompareTag("Chair") || hit.transform.CompareTag("Door") || hit.transform.name == "Flashlight" || hit.transform.name == "FuseBox" || hit.transform.name == "RecordPlayer_LOD0" || hit.transform.name == "Turntable_LOD0" || hit.transform.CompareTag("Window") || hit.transform.name == "Drawer" || hit.transform.name == "Door_Bottom" || hit.transform.name == "Toaster" || hit.transform.CompareTag("Toast") || hit.transform.name == "BreadPlate_A_LOD0" || hit.transform.name == "DartsTarget" || hit.transform.name == "PileOfClothes" || hit.transform.name == "Washer" || hit.transform.name == "Dryer" || hit.transform.name == "Fireplace" || hit.transform.name == "TaskPaper")
            {
                viewObject = hit.transform.gameObject;
                crosshair.SetActive(false);
                crosshairInteract.SetActive(true);
                crosshairLevel.SetActive(true);

                _outlineObject = viewObject.GetComponent<OutlineObject>();

                if (_outlineObject == null){
                    _outlineObject = viewObject.GetComponentInChildren<OutlineObject>();

                    if (_outlineObject == null){
                        _outlineObject = viewObject.GetComponentInParent<OutlineObject>();
                    }
                }

                var outlineObjects = GameObject.FindObjectsOfType<OutlineObject>();

                for (int i = 0; i < outlineObjects.Length; i++){
                    if (outlineObjects[i] != viewObject && outlineObjects[i].GetComponent<OutlineObject>().isActiveAndEnabled){
                        outlineObjects[i].GetComponent<OutlineObject>().enabled = false;
                    }
                }

                if (_outlineObject != null){
                    _outlineObject.enabled = true;
                }
            }
            else
            {
                viewObject = null;

                if (_outlineObject != null){
                    _outlineObject.enabled = false;
                    _outlineObject = null;
                }

            }
        }

    }

    public void Interacte()
    {

        if (viewObject != null && viewObject.CompareTag("Chair") && selectedObject == null)
        {

            Fix();
            if (!_chair.isTake)
            {
                _chair.isTake = true;
                viewObject.GetComponent<MeshCollider>().enabled = false;
                viewObject.GetComponent<Rigidbody>().useGravity = false;
                selectedObject = viewObject;

                if (_chair.onDoor)
                {
                    _chair.onDoor = false;
                }
            }
        }

        if (viewObject != null)
        {
            if(viewObject.CompareTag("Door"))
            {
                if(_door != null && !_door.isLocked && selectedObject == null || _door != null && !_door.isLocked && selectedObject != null && !selectedObject.CompareTag("Chair"))
                {
                    if (!_door.isOpen)
                    {
                        _door.isOpen = true;

                        _door.anim[_door.openingAnim].time = _door.openStartAt;
                        _door.anim[_door.openingAnim].speed = _door.openingSpeed;
                        _door.anim.Play();
                    }
                    else
                    {
                        _door.isOpen = false;

                        _door.anim[_door.openingAnim].time = _door.closeStartAt;
                        _door.anim[_door.openingAnim].speed = _door.closingSpeed;
                        _door.anim.Play();
                    }
                }

                if (_door.isLocked) 
                {
                    _door.audio.clip = _door.Locked;
                    _door.audio.Play();
                } 
            }

            if(viewObject.CompareTag("Door") && selectedObject != null && selectedObject.CompareTag("Chair"))
            {
                if(_door != null && _door.isIn && !_door.isOpen && _door.anim[_door.openingAnim].time <= 0)
                {
                    _chair.isTake = false;
                    _chair.transform.SetPositionAndRotation(_chairIndication.transform.position, _chairIndication.transform.rotation);

                    _chair.onDoor = true;
                    _door.chairOnDoor = selectedObject;

                    selectedObject.GetComponentInChildren<MeshCollider>().enabled = true;
                    selectedObject.GetComponentInChildren<MeshCollider>().isTrigger = true;
                    selectedObject = null;
                    _chairIndication.DesactivateRenderer();
                }
            }

            if(viewObject.name == "Flashlight" && selectedObject == null)
            {
                Fix();
                if (!_torche.isTake)
                {
                    _torche.isTake = true;
                    viewObject.GetComponent<BoxCollider>().enabled = false;
                    viewObject.GetComponent<Rigidbody>().useGravity = false;
                    selectedObject = viewObject;
                }
            }

            if(viewObject.name == "FuseBox")
            {
                if (!_levier.isOn)
                {
                    _levier.isOn = true;
                    _levier.anim.SetBool("isOn", true);

                    _levier.ActivateLight();
                }
                else
                {
                    _levier.isOn = false;
                    _levier.anim.SetBool("isOn", false);

                    _levier.DesactivateLight();

                }
            }

            if(viewObject.name == "Turntable_LOD0" || viewObject.name == "RecordPlayer_LOD0")
            {
                _recordPlayer.done = true;

                if (_recordPlayer.isOn)
                {
                    _recordPlayer.isOn = false;
                    _recordPlayer.audio.Stop();
                    _recordPlayer.anim.SetBool("isOn", false);
                }
                else
                {
                    _recordPlayer.isOn = true;
                    _recordPlayer.audio.Play();
                    _recordPlayer.anim.SetBool("isOn", true);
                }
            }

            if (viewObject.CompareTag("Window") || viewObject.name == ("Drawer") || viewObject.name == ("Door_Bottom"))
            {
                if (!_door.isOpen)
                {
                    _door.isOpen = true;

                    _door.anim[_door.openingAnim].time = _door.openStartAt;
                    _door.anim[_door.openingAnim].speed = _door.openingSpeed;
                    _door.anim.Play();
                }
                else
                {
                    _door.isOpen = false;

                    _door.anim[_door.openingAnim].time = _door.closeStartAt;
                    _door.anim[_door.openingAnim].speed = _door.closingSpeed;
                    _door.anim.Play();
                }
            }

            if(viewObject.name == "TaskPaper")
            {
                _taskManager.onIt = true;
            }

        }


        Task();
    }

    void Task()
    {
        if (viewObject.CompareTag("Toast"))
        {
            Fix();

            _toast.isTake = true;
            _toast.GetComponent<Rigidbody>().useGravity = false;
            _toast.GetComponent<MeshCollider>().enabled = false;
            if (_toast.GetComponent<MeshCollider>().isTrigger == true) _toast.GetComponent<MeshCollider>().isTrigger = false;
            selectedObject = viewObject;
        }

        if (viewObject.name == "Toaster")
        {
            if (selectedObject != null && selectedObject.CompareTag("Toast") && !_toast.toasted)
            {
                _toaster.timertext.fontSize = 0.1f;
                _toast.isTake = false;
                if (_toaster.Toast1 == null)
                {
                    _toast.transform.SetPositionAndRotation(_toaster.ToastPosition1.transform.position, _toaster.ToastPosition1.transform.rotation);
                    _toaster.Toast1 = selectedObject;
                    StartCoroutine(WaitForNull());
                }
                else if (_toaster.Toast1 != null && _toaster.Toast2 == null)
                {
                    _toast.transform.SetPositionAndRotation(_toaster.ToastPosition2.transform.position, _toaster.ToastPosition2.transform.rotation);
                    _toaster.Toast2 = selectedObject;
                    StartCoroutine(WaitForNull());
                }
                else if (_toaster.Toast1 != null && _toaster.Toast2 != null)
                {
                    _toaster.isOn = true;
                }
            }
            if (selectedObject == null && _toaster.Toast1 != null && _toaster.Toast2 != null) 
            {
                if(!_toaster.Toast1.GetComponent<Toast>().toasted && !_toaster.Toast2.GetComponent<Toast>().toasted)
                {
                    _toaster.isOn = true;
                }
                if(_toaster.Toast1.GetComponent<Toast>().toasted && !_toaster.Toast2.GetComponent<Toast>().toasted)
                {
                    _toaster.timertext.fontSize = 0.04f;
                    _toaster.timertext.text = "Veuillez retirer le toast grill� !";
                    _toaster.timerCanvas.SetActive(true);

                    StartCoroutine(WaitForFalse());
                }
                if (!_toaster.Toast1.GetComponent<Toast>().toasted && _toaster.Toast2.GetComponent<Toast>().toasted)
                {
                    _toaster.timertext.fontSize = 0.04f;
                    _toaster.timertext.text = "Veuillez retirer le toast grill� !";
                    _toaster.timerCanvas.SetActive(true);

                    StartCoroutine(WaitForFalse());
                }
                if (_toaster.Toast1.GetComponent<Toast>().toasted && _toaster.Toast2.GetComponent<Toast>().toasted)
                {
                    _toaster.timertext.fontSize = 0.04f;
                    _toaster.timertext.text = "Veuillez retirer le toast grill� !";
                    _toaster.timerCanvas.SetActive(true);

                    StartCoroutine(WaitForFalse());
                }
            } 
            if (selectedObject == null && _toaster.Toast1 == null && _toaster.Toast2 == null || selectedObject != null && !selectedObject.CompareTag("Toast") && _toaster.Toast1 == null && _toaster.Toast2 == null || selectedObject == null && _toaster.Toast1 != null && _toaster.Toast2 == null || selectedObject == null && _toaster.Toast1 == null && _toaster.Toast2 != null)
            {
                _toaster.timertext.fontSize = 0.05f;
                _toaster.timertext.text = "Il vous faut des toasts !";
                _toaster.timerCanvas.SetActive(true);

                StartCoroutine(WaitForFalse());
            }
            if (selectedObject != null && selectedObject.CompareTag("Toast") && _toast.toasted)
            {
                _toaster.timertext.fontSize = 0.04f;
                _toaster.timertext.text = "Il vous faut des toasts non grill�s !";
                _toaster.timerCanvas.SetActive(true);

                StartCoroutine(WaitForFalse());
            }

            if (_toaster.isOn)
            {
                _toaster.timerTextPause = true;
                _toaster.timerCanvas.SetActive(true);
                _toaster.timertext.fontSize = 0.03f;
                _toaster.timertext.text = "Vous devez patienter";
                StartCoroutine(WaitForFalseWait());
            }

            IEnumerator WaitForNull()
            {
                yield return new WaitForSeconds(0.5f);
                selectedObject = null;
            }

            IEnumerator WaitForFalseWait()
            {
                yield return new WaitForSeconds(2f);
                _toaster.timerTextPause = false;
            }

            IEnumerator WaitForFalse()
            {
                yield return new WaitForSeconds(2f);
                _toaster.timerCanvas.SetActive(false);
            }
        }

        if (viewObject.name == "BreadPlate_A_LOD0" && selectedObject != null && selectedObject.CompareTag("Toast") && _toast.toasted)
        {
            if(_breadPlate.Toast1 == null && _breadPlate.Toast2 == null && _breadPlate.Toast3 == null && _breadPlate.Toast4 == null)
            {
                _toast.isTake = false;
                _toast.transform.SetPositionAndRotation(_breadPlate.ToastPosition1.transform.position, _breadPlate.ToastPosition1.transform.rotation);
                _toast.GetComponent<MeshCollider>().enabled = false;
                _toast.GetComponent<Rigidbody>().isKinematic = true;
                _breadPlate.Toast1 = selectedObject;
                selectedObject = null;
                _breadPlate.numberOfToast = 1;
            }
            else if(_breadPlate.Toast1 != null && _breadPlate.Toast2 == null && _breadPlate.Toast3 == null && _breadPlate.Toast4 == null)
            {
                _toast.isTake = false;
                _toast.transform.SetPositionAndRotation(_breadPlate.ToastPosition2.transform.position, _breadPlate.ToastPosition2.transform.rotation);
                _toast.GetComponent<MeshCollider>().enabled = false;
                _toast.GetComponent<Rigidbody>().isKinematic = true;
                _breadPlate.Toast2 = selectedObject;
                selectedObject = null;
                _breadPlate.numberOfToast = 2;
            }
            else if(_breadPlate.Toast1 != null && _breadPlate.Toast2 != null && _breadPlate.Toast3 == null && _breadPlate.Toast4 == null)
            {
                _toast.isTake = false;
                _toast.transform.SetPositionAndRotation(_breadPlate.ToastPosition3.transform.position, _breadPlate.ToastPosition3.transform.rotation);
                _toast.GetComponent<MeshCollider>().enabled = false;
                _toast.GetComponent<Rigidbody>().isKinematic = true;
                _breadPlate.Toast3 = selectedObject;
                selectedObject = null;
                _breadPlate.numberOfToast = 3;
            }
            else if(_breadPlate.Toast1 != null && _breadPlate.Toast2 != null && _breadPlate.Toast3 != null && _breadPlate.Toast4 == null)
            {
                _toast.isTake = false;
                _toast.transform.SetPositionAndRotation(_breadPlate.ToastPosition4.transform.position, _breadPlate.ToastPosition4.transform.rotation);
                _toast.GetComponent<MeshCollider>().enabled = false;
                _toast.GetComponent<Rigidbody>().isKinematic = true;
                _breadPlate.Toast4 = selectedObject;
                selectedObject = null;
                _breadPlate.numberOfToast = 4;
            }
        }

        if(viewObject.name == "DartsTarget")
        {
            _flechette.player.SetActive(false);
            _flechette.flechetteCamera.SetActive(true);
            _flechette.onIt = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            _flechette.pointsGO.SetActive(true);
            _flechette.GUI[0].SetActive(true);
        }

        if(viewObject.name == "PileOfClothes")
        {
            Fix();

            _clothes.isTake = true;
            selectedObject = viewObject;
            _clothes.GetComponent<Rigidbody>().useGravity = false;
            _clothes.GetComponent<BoxCollider>().enabled = false;
        }

        if(viewObject.name == "Washer")
        {
            if (selectedObject == null || selectedObject != null && selectedObject.name != "PileOfClothes")
            {
                _washer.Text.SetActive(true);
                _washer.timertext.text = "Il vous faut du linge !";
                _washer.timertext.fontSize = 0.08f;
                StartCoroutine(WaitForFalseWaserAndDryer(_washer, null));
            }
            else if (selectedObject != null && selectedObject.name == "PileOfClothes" && !_clothes.isWashed && !_clothes.isDryed)
            {
                _washer.isOn = true;
                _washer.clothes = selectedObject;
                selectedObject.transform.SetPositionAndRotation(_washer.clothesPos.transform.position, _washer.clothesPos.transform.rotation);
                selectedObject.GetComponent<PileOfClothes>().isTake = false;
                selectedObject.GetComponent<BoxCollider>().enabled = false;
                selectedObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                _washer.Text.SetActive(true);
                _washer.timertext.fontSize = 0.2f;
                selectedObject = null;
            }
            else if(selectedObject != null && selectedObject.name == "PileOfClothes" && _clothes.isWashed)
            {
                _washer.Text.SetActive(true);
                _washer.timertext.text = "Le linge est d�j� lav� !";
                _washer.timertext.fontSize = 0.08f;
                StartCoroutine(WaitForFalseWaserAndDryer(_washer, null));
            }
        }

        if(viewObject.name == "Dryer")
        {
            if (selectedObject == null || selectedObject != null && selectedObject.name != "PileOfClothes")
            {
                _dryer.timertext.text = "Il vous faut du linge � s�ch�!";
                _dryer.timertext.fontSize = 0.08f;
                _dryer.Text.SetActive(true);
                StartCoroutine(WaitForFalseWaserAndDryer(null, _dryer));
            }
            else if (selectedObject != null && selectedObject.name == "PileOfClothes" && _clothes.isWashed && !_clothes.isDryed)
            {
                _dryer.isOn = true;
                _dryer.clothes = selectedObject;
                selectedObject.transform.SetPositionAndRotation(_dryer.clothesPos.transform.position, _dryer.clothesPos.transform.rotation);
                selectedObject.GetComponent<PileOfClothes>().isTake = false;
                selectedObject.GetComponent<BoxCollider>().enabled = false;
                selectedObject.GetComponentInChildren<MeshRenderer>().enabled = false;
                _dryer.Text.SetActive(true);
                _dryer.timertext.fontSize = 0.2f;
                selectedObject = null;
            }
            else if(selectedObject != null && selectedObject.name == "PileOfClothes" && _clothes.isWashed && _clothes.isDryed)
            {
                _dryer.Text.SetActive(true);
                _dryer.timertext.text = "Le linge est d�j� s�ch� !";
                _dryer.timertext.fontSize = 0.08f;
                StartCoroutine(WaitForFalseWaserAndDryer(null, _dryer));
            }
        }

        if(viewObject.name == "Fireplace")
        {
            _fireplace.fireParticle.startSize = 1;
            _fireplace.done = true;
        }


        IEnumerator WaitForFalseWaserAndDryer(Washer wash, Dryer dry)
        {
            yield return new WaitForSeconds(2f);
            if(wash != null) wash.Text.SetActive(false);
            if(dry != null) dry.Text.SetActive(false);
        }
    }

    public void Fix()
    {
        if(selectedObject != null)
        {
            if(selectedObject.name == "Flashlight")
            {
                _torche.isTake = false;
                _torche.GetComponent<BoxCollider>().enabled = true;
                _torche.rb.useGravity = true;
                _torche.rb.AddForce(transform.forward * 10, ForceMode.Impulse);

                selectedObject = null;

            }
            if(selectedObject.CompareTag("Toast"))
            {
                _toast.isTake = false;
                _toast.GetComponent<MeshCollider>().enabled = true;
                _toast.GetComponent<Rigidbody>().useGravity = true;
                _toast.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);

                selectedObject = null;
            }
            if (selectedObject.CompareTag("Chair"))
            {
                _chair.isTake = false;
                _chair.GetComponent<MeshCollider>().enabled = true;
                _chair.GetComponent<MeshCollider>().isTrigger = false;
                _chair.rb.useGravity = true;
                _chair.rb.AddForce(transform.forward * 5, ForceMode.Impulse);

                selectedObject = null;

            }
            if(selectedObject.name == "PileOfClothes")
            {
                _clothes.isTake = false;
                _clothes.GetComponent<BoxCollider>().enabled = true;
                _clothes.rb.useGravity = true;
                _clothes.rb.AddForce(transform.forward * 10, ForceMode.Impulse);

                selectedObject = null;
            }

        }
    }

}
