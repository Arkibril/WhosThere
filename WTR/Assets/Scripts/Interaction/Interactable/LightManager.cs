using UnityEngine;

public class LightManager : MonoBehaviour
{

    public bool isOn;

    public Material[] lampOn;
    public Material[] lampOff;
    public MeshRenderer[] lamp;

    private void Start(){
        lamp = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInChildren<Light>().enabled == true && !isOn) isOn = true;
        else isOn = false;

        Material();
    }

    void Material()
    {
        if(gameObject.name == "BilliardLampTriple_ON")
        {
            for(int i = 0; i < lamp.Length; i++)
            {
                if(lamp[i].name == "BilliardLampTriple_LOD0" || lamp[i].name == "BilliardLampTriple_LOD" || lamp[i].name == "BilliardLampSingle_LOD0" || lamp[i].name == "BilliardLampSingle_LOD1")
                {
                    if (isOn) lamp[i].material = lampOn[0];
                    else lamp[i].material = lampOff[0];
                }
                if(lamp[i].name == "Bulbs_LOD0" || lamp[i].name == "Bulbs_LOD1")
                {
                    if (isOn) lamp[i].material = lampOn[9];
                    else lamp[i].material = lampOff[9];
                }
            }
        }
        if (gameObject.name == "LampWall_OFF")
        {
            for(int i = 0; i < lamp.Length; i++)
            {
                if(lamp[i].name == "LampWall_LOD0" || lamp[i].name == "LampWall_LOD1")
                {
                    if (isOn) lamp[i].material = lampOn[4];
                    else lamp[i].material = lampOff[4];
                }
            }
        }
        if (gameObject.name == "TableLamp_A_ON" || gameObject.name == "FloorLamp")
        {         
            for (int i = 0; i < lamp.Length; i++)
            {
                if(lamp[i].name == "TableLamp_A_LOD0" || lamp[i].name == "TableLamp_A_LOD1" || lamp[i].name == "TableLamp_A_LOD2" || lamp[i].name == "FloorLamp_LOD0" || lamp[i].name == "FloorLamp_LOD1" || lamp[i].name == "FloorLamp_LOD2")
                {
                    if (isOn) lamp[i].material = lampOn[6];
                    else lamp[i].material = lampOff[6];
                }
                if(lamp[i].name == "BulbGlass_LOD0" || lamp[i].name == "BulbGlass_LOD1" || lamp[i].name == "BulbGlass_LOD2")
                {
                    if (isOn) lamp[i].material = lampOn[5];
                    else lamp[i].material = lampOff[5];
                }
            }
        }
        if (gameObject.name == "Bulb")
        {
            for(int i = 0; i < lamp.Length; i++)
            {
                if(lamp[i].name == "Bulb")
                {
                    if (isOn) lamp[i].material = lampOn[8];
                    else lamp[i].material = lampOff[8];
                }
                if(lamp[i].name == "Bulb_glass")
                {
                    if (isOn) lamp[i].material = lampOn[7];
                    else lamp[i].material = lampOff[7];
                }
            }
        }
        if(gameObject.name == "CeilingLamp_C")
        {
            for(int i = 0; i < lamp.Length; i++)
            {
                if(lamp[i].name == "CeilingLamp_C_LOD0" || lamp[i].name == "CeilingLamp_C_LOD1")
                {
                    if (isOn) 
                    {
                        lamp[i].material = lampOn[10];
                        gameObject.GetComponent<Animation>().Play();
                    } 
                    else 
                    {
                        lamp[i].material = lampOff[10];
                        gameObject.GetComponent<Animation>().Stop();
                    }
                    
                }
            }
        }
        if(gameObject.name == "CeilingLamp_A")
        {
            for(int i = 0; i < lamp.Length; i++)
            {
                if (isOn) lamp[i].material = lampOn[10];
                else lamp[i].material = lampOff[10];
            }
        }
        if(gameObject.name == "CeilingLamp_B")
        {
            for (int i = 0; i < lamp.Length; i++)
            {
                if (lamp[i].name == "CeilingLamp_B_LOD0" || lamp[i].name == "CeilingLamp_B_LOD1")
                {
                    if (isOn) lamp[i].material = lampOn[10];
                    else lamp[i].material = lampOff[10];
                }
            }
        }
        if(gameObject.name == "CeilingLamp_D_ON")
        {
            for (int i = 0; i < lamp.Length; i++)
            {
                if (isOn) lamp[i].material = lampOn[1];
                else lamp[i].material = lampOff[1];
            }
        }
        if(gameObject.name == "DeskLamp_A_OFF")
        {
            for (int i = 0; i < lamp.Length; i++)
            {
                if (lamp[i].name == "DeskLamp_LOD0" || lamp[i].name == "DeskLamp_LOD1")
                {
                    if (isOn) lamp[i].material = lampOn[2];
                    else lamp[i].material = lampOff[2];
                }
            }
        }

    }
}
