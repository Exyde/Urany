using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranieTalkingBehavior : MonoBehaviour
{
    public GameObject uranieBoss;
    DialogueManager dm;

    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (dm.endDialogue)
		{
            uranieBoss.SetActive(true);
            Destroy(this.gameObject);
		}
    }
}
