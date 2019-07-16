using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyrespawn : MonoBehaviour
{
    public bool Death;
    public float Timer;
    public float Cooldown;
    public GameObject Enemy;
    public string EnemyName;
    GameObject LastEnemy;
    // Use this for initialization
    void Start()
    {
        Death = false;
        this.gameObject.name = EnemyName + "spawn point";
    }

    // Update is called once per frame
    void Update()
    {
        if (Death == true)
        {
            Timer += Time.deltaTime;
        }

        if (Timer >= Cooldown)
        {
            Enemy.transform.position = transform.position;

            Instantiate(Enemy);
            LastEnemy = GameObject.Find(Enemy.name + "(Clone)");
            LastEnemy.name = EnemyName;
            Death = false;
            Timer = 0;
        }
    }
}
