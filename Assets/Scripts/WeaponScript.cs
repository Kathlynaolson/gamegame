using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public float shotSpeed;
    public Vector3 shotOffset = Vector3.zero;

    public Transform shotPrefab;

    public float shootingRate = 0.25f;

    private float shootCooldown = 0f;

    // Update is called once per frame
    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position + shotOffset;
            

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }

            Destroy(shotTransform.gameObject, 2);
            Vector2 mousePos = Input.mousePosition;
            
            Vector2 objPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = (mousePos.x - objPos.x);
            mousePos.y = (mousePos.y - objPos.y);

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            Vector3 rotation = new Vector3(0, 0, angle);

            //shotTransform.rotation = Quaternion.Euler(rotation);
            shotTransform.transform.RotateAround(transform.position, new Vector3(0, 0, 1), angle);
            shotTransform.GetComponent<Rigidbody2D>().AddForce(shotTransform.transform.right.normalized * shotSpeed);

        }
    }

    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
