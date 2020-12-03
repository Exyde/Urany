using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDisplayer : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Y()
	{
        anim.SetTrigger("y");
	}

    public void X()
    {
        anim.SetTrigger("x");
    }

    public void B()
    {
        anim.SetTrigger("b");
    }

    public void A()
    {
        anim.SetTrigger("a");
    }
    public void LB()
    {
        anim.SetTrigger("lb");
    }
    public void RB()
    {
        anim.SetTrigger("rb");
    }
    public void Side()
    {
        anim.SetTrigger("side");
    }
    public void UpDown()
    {
        anim.SetTrigger("updown");
    }
    public void Empty()
    {
        anim.SetTrigger("empty");
    }
}
