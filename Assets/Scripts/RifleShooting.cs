using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooting : MonoBehaviour {

    public GameObject bullet;
    public float fireRate;

    private float nextShot;
    private float shotTimer;
    private Light shotLight;

    // Use this for initialization
    void Awake () {
		shotLight = GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        shotTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && shotTimer > nextShot)
        {
            Shoot();
        }
        else
        {
            PlayerController.animator.SetBool("isShootingSingle", false);
            shotLight.enabled = false;
        }
    }

    void Shoot()
    {
        nextShot = Time.time + fireRate;
        Instantiate(bullet, transform.position, transform.rotation);
        shotLight.enabled = true;
        PlayerController.animator.SetBool("isShootingSingle", true);
    }
}
