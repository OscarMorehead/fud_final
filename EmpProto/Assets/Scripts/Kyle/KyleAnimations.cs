using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KyleAnimations : MonoBehaviour
{
    public Animator animator;

    public void Walk () {
        animator.Play("Walk");
    }

    public void Idle() {
        animator.Play("Idle");
    }
}
