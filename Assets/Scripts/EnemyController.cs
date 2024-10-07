using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject loseTextObject;
    private Rigidbody rb;
    public float speed;

    void Start()
    {
        loseTextObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed);
        Vector3 direction = (Player.transform.position - transform.position).normalized;

        print(direction);

        rb.AddForce(direction * speed);

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            loseTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
