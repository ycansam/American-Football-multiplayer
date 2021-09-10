using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallScrpt : MonoBehaviour
{
    private Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }
   
}
