using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRagdoll : MonoBehaviour
{
    private Animator animator;
    private CharController charController;
    [SerializeField] private Collider[] AllColliders;
    public bool isOnRagdoll = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponentInParent<CharController>();
        AllColliders = GetComponentsInChildren<Collider>();
        DoRagdoll(false);
    }
    
    public void DoRagdoll(bool isRagdoll){
        // foreach(var col in AllColliders)
        //     col.enabled = isRagdoll;
        charController.enabled = !isRagdoll;
        isOnRagdoll = isRagdoll;
        animator.enabled = !isRagdoll;
        
    }
}
