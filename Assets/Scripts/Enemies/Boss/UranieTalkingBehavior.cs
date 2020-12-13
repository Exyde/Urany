using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UranieTalkingBehavior : MonoBehaviour
{
    public Transform uranieBoss;
    public DialogueManager dm;

    void Start()
    {
        //dm = FindObjectOfType<DialogueManager>();
    }

    void Update()
    {
        if (dm.endDialogue)
		{
            uranieBoss.gameObject.SetActive(true);
            AudioManager.instance.StopMusic();
            //uranieBoss.GetComponent<Uranie>().enabled = true;
            Destroy(this.gameObject);
		}
    }
}
