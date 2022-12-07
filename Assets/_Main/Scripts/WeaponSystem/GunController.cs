using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;
    [SerializeField] private Transform exitPoint;

    private float fireTimer = 0;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(fireKey))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time - fireTimer > gun.GunFiringTime / gun.GunFiringRate)
        {
            fireTimer = Time.time;
            gun.Shoot(exitPoint);
        }
    }
}
