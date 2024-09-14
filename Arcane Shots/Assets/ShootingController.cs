using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bullePrefab;

    private float bulletForce = 20f;

    [SerializeField]
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        };
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bullePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
