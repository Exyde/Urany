using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    //If we need to review this.
    //https://www.youtube.com/watch?v=_nRzoTzeyxU

    private Queue<string> sentences;

    public Text npcNameText;
    public Text dialogueText;
    public Image npcImage;

    public Animator anim;
    public float typeSpeed = .2f;
    public bool isTalking;
    public bool textIsWriting;

    public bool endDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
        isTalking = textIsWriting = false;
    }

    public void StartDialogue(Dialogue dialogue)
	{
        if (isTalking)
		{
            return;
		} else
		{
            anim.SetBool("isOpen", true);
            FindObjectOfType<Movement>().canMove = false;
            isTalking = true;
            npcNameText.text = dialogue.npcName;
            npcImage.sprite = dialogue.npcSprite;
            sentences.Clear();
            endDialogue = false;

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

	}

    public void DisplayNextSentence()
	{
        if (sentences.Count == 0)
		{
            EndDialogue();
            return;
		}

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
	}

    IEnumerator TypeSentence(string sentence)
	{
        dialogueText.text = "";
        textIsWriting = true;

        foreach (char c in sentence.ToCharArray())
		{
            dialogueText.text += c;
            AudioManager.instance.TypoFX();
            yield return new WaitForSeconds (typeSpeed);

		}

        textIsWriting = false;
	}
    public void EndDialogue()
	{
        anim.SetBool("isOpen", false);
        FindObjectOfType<Movement>().canMove = true;
        isTalking = false;
        endDialogue = true;
    }

    private void Update()
	{
		if (TalkingInput() && isTalking && !textIsWriting)
		{
            DisplayNextSentence();
		}
	}

    bool TalkingInput()
    {
        return (Input.GetKeyDown(KeyCode.P) ^ Input.GetButtonDown("Attack"));
    }

}
