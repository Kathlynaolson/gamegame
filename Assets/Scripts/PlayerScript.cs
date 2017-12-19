using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private WeaponScript weapon;

    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector3 mousePos;
    private Vector3 objPos;

    private float angle;
    private Vector3 rotation;

    private void Start()
    {

        weapon = GetComponent<WeaponScript>();

    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        //if (Input.touches[0].phase == TouchPhase.Began
        //    || Input.touches[0].phase == TouchPhase.Moved)
        {
            mousePos = Input.mousePosition;

            objPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objPos.x;
            mousePos.y = mousePos.y - objPos.y;

            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90;

            rotation = new Vector3(0, 0, angle);
            transform.rotation = Quaternion.Euler(rotation);

            weapon.Attack(false);
            
        }

    }
}
