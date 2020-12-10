using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranieTalkingBehavior : MonoBehaviour
{
    public GameObject uranieBoss;
    public DialogueManager dm;

    void Start()
    {
        //dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (dm.endDialogue)
		{
            //uranieBoss.SetActive(true);
            uranieBoss.GetComponent<Uranie>().enabled = true;
            Destroy(this.gameObject);
		}
    }
}
