using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    //Handling
    public float rotationSpeed = 450;
    public float walkSpeed = 5;
    public float runSpeed = 8;
    private float acceleration = 5;
    public int health = 1000;
    public bool isAlive = true;
    public Text healthText;
    public float restartDelay = 5f;
    public Text gameOverText;
    public Text pickUpText;

    //System
    private Quaternion targetRotation;
    private Vector3 currentVelocityMod;

    //Components
    public Gun gun;
    private CharacterController controller;
    private Camera cam;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        //gameOverText.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMouse();
        //ControlWASD();
        ShowHealth();


        //if (Input.GetButtonDown("Shoot"))
        //{
        //    gun.Shoot();
        //}
        //else if (Input.GetButton("Shoot"))
        //{
        //    gun.ShootContinuous();
        //}
    }

    void ControlMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.transform.position.y - transform.position.y));
        targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x, 0, transform.position.z));
        transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityMod;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);

    }

    void ControlWASD()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (input != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(input);
            transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, rotationSpeed * Time.deltaTime);
        }

        currentVelocityMod = Vector3.MoveTowards(currentVelocityMod, input, acceleration * Time.deltaTime);
        Vector3 motion = currentVelocityMod;
        motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1) ? .7f : 1;
        motion *= (Input.GetButton("Run")) ? runSpeed : walkSpeed;
        motion += Vector3.up * -8;

        controller.Move(motion * Time.deltaTime);

    }

    void ShowHealth()
    {
        healthText.text = "Health: " + health;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            health = health - 10;
            ShowHealth();
            PlayerDie();
        }

        if (col.gameObject.CompareTag("Pick Up"))
        {
            pickUpText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pickUpText.enabled = false;
    }

    void PlayerDie()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

}
