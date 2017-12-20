using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunnelZigZagMoveScript : MonoBehaviour {


    public Vector2 speed = new Vector2(10, 10);
    public float bottomBound;
    public float topBound;
    public float leftBound = -6;
    public float funnelDecrement;
    

    public Vector2 direction = new Vector2(-1, -1);
    
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

