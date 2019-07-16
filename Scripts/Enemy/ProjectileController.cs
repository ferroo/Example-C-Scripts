using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectileController : MonoBehaviour {


    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }

}

