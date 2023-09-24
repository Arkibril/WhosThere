using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{    
    public class OpenDoor : InteractableBase
    {
        public Animator anim;
        public GameObject AnimatorObject;
        public float OpenTime;

        private void Start()
        {
            anim = AnimatorObject.GetComponent<Animator>();
        }
        public override void OnInteract()
        {
            base.OnInteract();

            anim.SetBool("Open", true);


        }
    }
}
