using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranie : MonoBehaviour
{

    public Rigidbody2D rb;
    public SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }
}
