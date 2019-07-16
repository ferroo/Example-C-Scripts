using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int health = 100;
    public int score = 0;
    public bool isAlive = true;
    public Text healthText;
    public float restartDelay = 5f;
    public Text gameOverText;
    public Text pickUpText;
    public Text scoreText;
    public NewGun newgun;
    public NewGun newgun2;
    public NewGun newgun3;
    public WeaponSwitcher wep;
    public bool inTrigger;
    public Camera fpsCam;
    public Camera cutsceneCam;
    public ParticleSystem Boom;

    // Use this for initialization
    void Start () {
        ShowHealth();
        ShowScore();
	}
	
	// Update is called once per frame
	void Update () {
        ShowHealth();
        ShowScore();

        if (inTrigger == true && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Explosion());
        }
    }

    private IEnumerator Explosion()
    {
        AudioSource audio = GetComponent<AudioSource>();
        inTrigger = false;
        pickUpText.enabled = false;
        fpsCam.enabled = false;
        cutsceneCam.enabled = true;
        yield return new WaitForSeconds(1f);
        audio.Play();
        Boom.Play();
        Destroy(GameObject.FindWithTag("Fake"));
        Destroy(GameObject.FindWithTag("Activate"));
        Destroy(GameObject.FindWithTag("Pick Up 2"));
        yield return new WaitForSeconds(4f);
        fpsCam.enabled = true;
        cutsceneCam.enabled = false;
        Boom.Stop();
    }


    void ShowHealth()
    {
        healthText.text = "Health: " + health;
    }

    void ShowScore()
    {
        scoreText.text = "Score: " + score;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health -= 10;
            ShowHealth();
            PlayerDie();
        }

        if (col.gameObject.CompareTag("Pick Up"))
        {
            pickUpText.enabled = true;
        }

        if (col.gameObject.CompareTag("AmmoBox"))
        {
            if(wep.selectedWeapon == 0)
            {
                newgun.reserveAmmo += newgun.maxAmmo;
            }
            else if(wep.selectedWeapon == 1)
            {
                newgun2.reserveAmmo += newgun2.maxAmmo;
            }
            else if(wep.selectedWeapon == 2)
            {
                newgun3.reserveAmmo += newgun3.maxAmmo;
            }

            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Activate"))
        {
            inTrigger = true;
            pickUpText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
        pickUpText.enabled = false;
    }

    void PlayerDie()
    {
        if (health <= 0)
        {
            //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
