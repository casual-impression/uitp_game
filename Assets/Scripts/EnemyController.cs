using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5f;
    Vector3 total_speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();    
        Enemy enemyRef = gameObject.GetComponent<Enemy>();
        if (enemyRef) {
            speed = enemyRef.getSpeed;
        }
    }

    void MoveForward() {
        total_speed = 1f * Time.fixedDeltaTime * speed * Vector3.forward;
        transform.Translate(total_speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveForward();
    }
}
