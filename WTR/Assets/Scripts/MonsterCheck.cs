using System.Collections;
using UnityEngine;

public class MonsterCheck : MonoBehaviour
{
    public bool HesHere;
    private GameObject monsterScript;

    void Start(){
        monsterScript = GameObject.FindGameObjectWithTag("MonsterScript");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Monster(Clone)") != null && !HesHere)
        {
            
            monsterScript.GetComponent<MonsterScript>().enabled = false;

            HesHere = true;
            StartCoroutine(TimeDespawn());
        }
    }

    IEnumerator TimeDespawn() 
    {
        yield return new WaitForSeconds(5f);

        monsterScript.GetComponent<MonsterScript>().enabled = true;
        HesHere = false;
        Destroy(GameObject.FindWithTag("Monster"));

    }
}
