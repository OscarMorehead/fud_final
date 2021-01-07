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
    public string bone;
    public float range = 10f;
    public float radius = 1f;
    public LayerMask layerMask;

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

        var hits = Physics.SphereCastAll(shootPos.position, radius, transform.forward, range, layerMask);

        List<GameObject> bolts = new List<GameObject>();

        foreach (var hit in hits) {

            print(hit.collider.transform.root);

            Transform boneTransform = FindDeepChild(hit.collider.transform.root, bone);

            if (boneTransform == null) {
                canShoot = true;

                yield break;
            } 

            GameObject newLighting = Instantiate(lightningPrefab, shootPos);
            LightningBoltScript bolt = newLighting.GetComponent<LightningBoltScript>();

            bolt.StartObject.transform.position = shootPos.position;
            bolt.EndObject.transform.position = boneTransform.position;

            bolts.Add(newLighting);

            NavWander robot = hit.collider.transform.root.GetComponent<NavWander>();

            robot.Die();
        }

        canShoot = true;

        yield return new WaitForSeconds(boltTime);

        foreach (var bolt in bolts) {
            Destroy(bolt);
        }
    }

    public Transform FindDeepChild(Transform aParent, string aName) {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0) {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }
}
