using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavWander : MonoBehaviour
{
    [SerializeField]
    bool navWaiting;

    [SerializeField]
    public List<Waypoint> pOI;

    NavMeshAgent kyleTheSickBoi;

    [SerializeField]
    public int currentPOI;

    public bool isWalking;

    [SerializeField]
    public bool isBusy;

    bool forward;

    [SerializeField]
    float waitLength;

    [SerializeField]
    float totalWaitTime;

    [SerializeField]
    KyleAnimations kyleAnimations;

    public void Start()
    {
        kyleTheSickBoi = GetComponent<NavMeshAgent>();
        kyleAnimations = GetComponent<KyleAnimations>();

        currentPOI = Random.Range(0, pOI.Count);
    }
    public void Update()
    {
        CheckDistanceLeft();
    }

    public void Die () {
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine () {
        kyleAnimations.Die();

        while (true) {
            kyleTheSickBoi.isStopped = true;

            yield return new WaitForEndOfFrame();
        }
    }

    private void CheckDistanceLeft()
    {
        if (isWalking && kyleTheSickBoi.remainingDistance <= 1f)
        {
            isWalking = false;
            if (navWaiting)
            {
                isBusy = true;
                waitLength = 0f;

                kyleAnimations.Idle();
            }
            else
            {
                MoveToNextPOI();
                SetDestination();
            }
        }

        if (isBusy)
        {
            //kyleAnimations.Idle();
            waitLength += Time.deltaTime;
            if(waitLength >= totalWaitTime)
            {
                //kyleAnimations.Walk();
                isBusy = false;
                isWalking = true;
                MoveToNextPOI();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(pOI != null)
        {
            Vector3 target = pOI[currentPOI].transform.position;
            kyleTheSickBoi.SetDestination(target);
            isWalking = true;
        }
    }

    private void MoveToNextPOI()
    {
        waitLength = 0f;
        kyleAnimations.Walk();
        currentPOI = Random.Range(0, pOI.Count);

       /* if (UnityEngine.Random.Range(0f, 1f) <= .2f)
        {
            forward = !forward;
        }
        if (forward)
        {
            currentPOI++;
            if(currentPOI  >= pOI.Count)
            {
                currentPOI = Random.Range(0, pOI.Count);
            }
        }
        else
        {
            currentPOI--;
            if(currentPOI < 0)
            {
                currentPOI = pOI.Count - 1;
            }
        }*/
    }
}
