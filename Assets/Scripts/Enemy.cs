using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyHungerBar hungerBar;

    [SerializeField] int health = 100;
    [SerializeField] int speed = 20;
    [Space]
    [SerializeField] int points = 50;
    EnemyController ec;
    Rigidbody enemy_rb;
    Player player;

    public int getSpeed => speed;
    public int getHealth => health;

    public void DamageReceived(int hitValue) {
        health -= hitValue;
        if (health >= 0) {
            hungerBar.TurnOnVisual();
            hungerBar.SetHungerAmount(health);
        }
        if (health <= 0) {
            player = FindObjectOfType<Player>();
            player.AddScore(points);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy_rb = gameObject.GetComponent<Rigidbody>();
        ec = gameObject.GetComponent<EnemyController>();
        // hungerBar = gameObject.GetComponent<EnemyHungerBar>();
        hungerBar.SetMaxHunger(health);
        hungerBar.TurnOffVisual();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
