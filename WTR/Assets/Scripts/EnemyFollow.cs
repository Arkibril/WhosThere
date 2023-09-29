using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyFollow : MonoBehaviour
{
    public GameObject Target;

    public NavMeshAgent myAgent;
    private Animator anim;

    public int rangeWalk = 30;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float dist = Vector3.Distance(this.transform.position, Target.transform.position);

        if (dist < rangeWalk)
        {
            myAgent.SetDestination(Target.transform.position);
            anim.SetBool("Walk", true);
        }
    }
}
