using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{

    public Transform Player;
    //public Transform spawnPosition;
    int walkSpeed = 4;
    int MaxDist = 10;
    int MinDist = 5;
    int LostDist = 25;
    public Rigidbody Projectile;
    public float speed = 10f;
    public float fireRate = 0.5F;
    public float nextFire = 0.0F;
    public float startingHealth = 100;
    public float currentHealth;
    public Text enemyHealthText;
    bool isDead;
    public int LOS = 0;



    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = startingHealth;
        //ShowHealth();
    }

    void Update()
    {
        //ShowHealth();
        if (LOS == 1)
        {

            //transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) >= MinDist)
            {

                transform.position += transform.forward * walkSpeed * Time.deltaTime;
                transform.LookAt(Player);

            }

            while (Vector3.Distance(transform.position, Player.position) <= MinDist && Time.time > nextFire)
            {
                transform.LookAt(Player);
                nextFire = Time.time + fireRate;
                FireRocket();
            }
        }

        if (Vector3.Distance(transform.position, Player.position) >= LostDist)
        {
            //if (Vector3.Distance(transform.position, Player.position) >= LostDist)
            //{
            LOS = 0;
            gameObject.GetComponent<Patrol>().enabled = true;
            //}
        }
    }

    //the bullets don't get deleted if they do not hit the player, needs to be fixed ideally
    //------fixed via projectilecontroller script
    void FireRocket()
    {
        Rigidbody rocketClone = (Rigidbody)Instantiate(Projectile, transform.position, transform.rotation);
        rocketClone.velocity = transform.forward * speed;
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    //------use for debugging
    //void ShowHealth()
    //{
    //    enemyHealthText.text = "Health: " + currentHealth;
    //}

    void EnemyDie()
    {
        //GameObject.Find(gameObject.name + ("spawn point")).GetComponent<Enemyrespawn>().Death = true;
        Destroy(gameObject);
    }


    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        gameObject.GetComponent<Patrol>().enabled = false;
        LOS = 1;
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            LOS = 1;
            gameObject.GetComponent<Patrol>().enabled = false;
        }
    }

}
