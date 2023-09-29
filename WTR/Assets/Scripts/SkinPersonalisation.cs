using UnityEngine;
using UnityEngine.UI;

public class SkinPersonalisation : MonoBehaviour, IDataPersistence<PlayerStyleData>
{ 

    [Header("Materials")]
    public Material[] bodyMaterials;
    public Material[] headMaterials;
    public Material[] eyesMaterials;
    public Material[] shoeMaterials;
    public Material[] topMaterials;
    public Material[] bottomMaterials;
    public Material[] hairMaterial;
    public bool easterEggs;

    [Space(5)]
    [Header("Meshs")]
    public Mesh[] shoeMesh;
    public Mesh[] topMesh;
    public Mesh[] bottomMesh;
    public Mesh[] headMesh;
    public Mesh[] bodyMesh;
    public Mesh[] hairMesh;

    [Space]
    [Header("Color")]
    public Color[] hairColor;

    [Header("GameObjects")]
    public GameObject body;
    public GameObject head;
    public GameObject eyesLeft;
    public GameObject eyesRight;
    public GameObject haut;
    public GameObject bas;
    public GameObject chaussures;
    public GameObject hair;

    [Header("Skinned Mesh Renderer")]
    private SkinnedMeshRenderer bodyRenderer, headRenderer, eyesLeftRenderer, eyesRightRenderer, topRenderer, bottomRenderer, shoesRenderer, hairRenderer;

    private int headIndex, bodyIndex, bodyColorIndex, eyesColorIndex, hairIndex, hairColorIndex, topIndex, bottomIndex, shoesIndex = 0;



    // Start is called before the first frame update
    void Awake()
    {

        bodyRenderer = body.GetComponent<SkinnedMeshRenderer>();
        eyesLeftRenderer = eyesLeft.GetComponent<SkinnedMeshRenderer>();
        eyesRightRenderer = eyesRight.GetComponent<SkinnedMeshRenderer>();
        hairRenderer = hair.GetComponent<SkinnedMeshRenderer>();
        topRenderer = haut.GetComponent<SkinnedMeshRenderer>();
        bottomRenderer = bas.GetComponent<SkinnedMeshRenderer>();
        shoesRenderer = chaussures.GetComponent<SkinnedMeshRenderer>();
        headRenderer = head.GetComponent<SkinnedMeshRenderer>();

        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Start(){
        DataPersistenceManager.dataHandler = new FileDataHandler(Application.persistentDataPath, "player_style_data", DataPersistenceManager.doIUseEncryption);
        DataPersistenceManager.instance.LoadPlayerStyle();
    }

    public void LoadData(PlayerStyleData data){
        easterEggs = data.easterEggs;

        headRenderer.sharedMesh = headMesh[data.headIndex];
        headRenderer.sharedMaterial = headMaterials[data.headIndex];

        bodyRenderer.sharedMesh = bodyMesh[data.bodyIndex];
        bodyRenderer.sharedMaterial = bodyMaterials[data.bodyColorIndex];

        eyesLeftRenderer.sharedMaterial = eyesMaterials[data.eyesColorIndex];
        eyesRightRenderer.sharedMaterial = eyesMaterials[data.eyesColorIndex];

        hairRenderer.sharedMesh = hairMesh[data.hairIndex];
        hairRenderer.sharedMaterial = hairMaterial[data.hairIndex];
        if (!data.easterEggs) hairRenderer.sharedMaterial.color = hairColor[data.hairColorIndex];
        else hairRenderer.sharedMaterial = hairMaterial[12];

        topRenderer.sharedMesh = topMesh[data.topIndex];
        topRenderer.sharedMaterial = topMaterials[data.topIndex];

        bottomRenderer.sharedMesh = bottomMesh[data.bottomIndex];
        bottomRenderer.sharedMaterial = bottomMaterials[data.bottomIndex];

        shoesRenderer.sharedMesh = shoeMesh[data.shoesIndex];
        shoesRenderer.sharedMaterial = shoeMaterials[data.shoesIndex];
    }

    public void SaveData(ref PlayerStyleData data){
        data.headIndex = headIndex;
        data.bodyIndex = bodyIndex;
        data.bodyColorIndex = bodyColorIndex;
        data.eyesColorIndex = eyesColorIndex;
        data.hairIndex = hairIndex;
        data.hairColorIndex = hairColorIndex;
        data.topIndex = topIndex;
        data.bottomIndex = bottomIndex;
        data.shoesIndex = shoesIndex;
        data.bodyIndex = bodyIndex;
        data.easterEggs = easterEggs;
    }

    void Update(){
        if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.M)) && !easterEggs){
            hairRenderer.sharedMaterial = hairMaterial[12];
            easterEggs = true;
        }
    }

    public void ChangeHead(int index){
        headRenderer.sharedMesh = headMesh[index];
        headIndex = index;
    }

    public void ChangeHeadColor(Slider slider){
        headRenderer.sharedMaterial = headMaterials[(int)slider.value];
        bodyRenderer.sharedMaterial = bodyMaterials[(int)slider.value];

        bodyColorIndex = (int)slider.value;
    }

    public void ChangeHair(int index){
        hairRenderer.sharedMesh = hairMesh[index];
        hairRenderer.sharedMaterial = hairMaterial[index];

        hairIndex = index;
    }

    public void ChangeHairColor(Slider slider){
        for (int i = 0; i < (hairMaterial.Length - 1); i++){
            hairMaterial[i].color = hairColor[(int)slider.value];
        }

        if (easterEggs){
            easterEggs = false;
            hairRenderer.sharedMaterial = hairMaterial[0];
        }

        hairColorIndex = (int)slider.value;
    }

    public void ChangeEyesColor(Slider slider){
        eyesRightRenderer.sharedMaterial = eyesMaterials[(int)slider.value];
        eyesLeftRenderer.sharedMaterial = eyesMaterials[(int)slider.value];

        eyesColorIndex = (int)slider.value;
    }

    public void ChangeTop(int index){
        topRenderer.sharedMesh = topMesh[index];
        topRenderer.sharedMaterial = topMaterials[index];

        topIndex = index;
        ChangeBody();
    }

    public void ChangeBottom(int index){
        bottomRenderer.sharedMesh = bottomMesh[index];
        bottomRenderer.sharedMaterial = bottomMaterials[index];

        bottomIndex = index;
        ChangeBody();
    }

    public void ChangeBody(){

        if (bottomRenderer.sharedMesh == bottomMesh[9] || bottomRenderer.sharedMesh == bottomMesh[10]){
            bodyRenderer.sharedMesh = bodyMesh[4];  
            bodyIndex = 4;
        } 
        else { 
            bodyRenderer.sharedMesh = bodyMesh[1];  
            bodyIndex = 1;
        }

        
        
    }

    public void ChangeShoes(int index){
        shoesRenderer.sharedMesh = shoeMesh[index];
        shoesRenderer.sharedMaterial = shoeMaterials[index];

        shoesIndex = index;
    }

    public void ChangeScene(){
        DataPersistenceManager.instance.SavePlayerStyle();
    }


}
