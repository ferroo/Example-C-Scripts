using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitWasteland1 : MonoBehaviour {

    public Text loadingText;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            loadingText.enabled = true;
            SceneManager.LoadScene("Wasteland3", LoadSceneMode.Single);
        }
    }
}
