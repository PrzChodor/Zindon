﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public int damage;
    public float spread;
    public float fireRate;
    public int currentAmmo;
    public int maxAmmo;

    public bool isShooting;
    public Transform firePoint;
    public bool isReloading;

    private Animator animator;
    public UnityEvent onReloaded;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        firePoint = transform.GetChild(0).transform;
        onReloaded = new UnityEvent();
    }
    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public IEnumerator Shoot(float angle)
    {
        isShooting = true;

        animator.SetTrigger("Shoot");
        angle = Random.Range(angle - spread, angle + spread);
        var rotation = Quaternion.Euler(0, 0, angle);
        var fired = Instantiate(bullet, firePoint.position, rotation);
        fired.GetComponent<Rigidbody2D>().AddForce(rotation * Vector2.left * bulletSpeed);
        fired.GetComponent<Bullet>().damage = damage;
        currentAmmo--;

        yield return new WaitForSeconds(1/fireRate);

        isShooting = false;
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(2f);
        currentAmmo = maxAmmo;
        onReloaded.Invoke();

        isReloading = false;
    }
}
