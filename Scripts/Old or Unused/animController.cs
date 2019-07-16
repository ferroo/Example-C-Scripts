using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animController : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
    if (Input.GetKey("w"))
    {
        anim.Play("RUN00_F");
    }

    else if (Input.GetKey("s"))
    {
        anim.Play("RUN00_F");
    }

    else if (Input.GetKey("a"))
    {
        anim.Play("RUN00_F");
    }

    else if (Input.GetKey("d"))
    {
        anim.Play("RUN00_F");
    }

    else if (Input.GetKey("t"))
    {
        anim.Play("UMATOBI00");
    }

    else if (Input.GetKey("v"))
    {
        anim.Play("JUMP00");
    }

        else
    {
        anim.Play("WAIT00");
    }
    }
}
