using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallSlideSmokeAnimator : MonoBehaviour {

    private Animator anim;
    private Movement move;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        move = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update() {
        anim.SetBool("wallSlide", move.wallSlide);
    }
}
