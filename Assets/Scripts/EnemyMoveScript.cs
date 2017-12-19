using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveScript : MonoBehaviour {


    public Vector2 speed = new Vector2(10, 10);
    public float bottomBound = -2;
    public float topBound = 2;
    public float leftBound = -4;
    

    private Vector2 direction = new Vector2(-1, -1);
    
    private float yPos;
    private Vector2 movement;

    private void Update()
    {
        yPos = transform.position.y;

        if (yPos > topBound)
        { 
            direction.y = -1;
            
        }
        if (yPos < bottomBound)
        {
            direction.y = 1;
            
        }

        movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        transform.Translate(movement * Time.deltaTime);

        if (transform.position.x < leftBound)
            Destroy(gameObject);
    }


}

