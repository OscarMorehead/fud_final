using DigitalRuby.LightningBolt;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float chargeTime = 0.35f;
    public float boltTime = 0.35f;
    public AudioSource audioSource;
    public bool canShoot = true;

    public Transform shootPos;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    public void Shoot () {
        if (!canShoot) return;

        canShoot = false;

        StartCoroutine(ShootCoroutine());
    }

    IEnumerator ShootCoroutine () {
        audioSource.Play();

        yield return new WaitForSeconds(chargeTime);

        GameObject newLighting = Instantiate(lightningPrefab, shootPos);
        LightningBoltScript bolt = newLighting.GetComponent<LightningBoltScript>();

        bolt.StartObject.transform.position = shootPos.position;
        bolt.EndObject.transform.position = shootPos.position + transform.forward * 10f;

        canShoot = true;

        yield return new WaitForSeconds(boltTime);

        Destroy(newLighting);
    }
}
