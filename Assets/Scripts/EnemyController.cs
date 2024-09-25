using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public GameObject loseTextObject;
    public float speed;

    void Start()
    {
        loseTextObject.SetActive(false);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, Player.transform.position, speed);
        // Create a 3D movement vector using the X and Y inputs.
        //Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        //rb.AddForce(movement * speed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            print("test");
            loseTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
