using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uranie : MonoBehaviour
{
    [Header ("Components")]
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
}
