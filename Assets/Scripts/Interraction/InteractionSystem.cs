using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Examine Fields")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamining;

    [Header ("Detection settings")]
    public Transform interractionPoint;
    public float interractionRadius;
    public LayerMask interractionLayer;

    [Header ("Item Info")]
    public GameObject currentObject;

    [Header("Temporary Inventory")]
    public List<GameObject> pickedItems = new List<GameObject>();

    public UIManager ui;

    void Start()
    {
        currentObject = null;
    }

    void Update()
    {
        if (DetectObject()) //If in range
		{
			if (InterractInput()) // and player input
			{
                currentObject.GetComponent<Interactable>().Interact();
			}
		} else
		{
            if (isExamining)
			{
                CloseExamineWindow();
                print("out of range)");
			}
		}
    }

    bool InterractInput()
	{
        return (Input.GetKeyDown(KeyCode.A) ^ Input.GetButtonDown("Fire1"));
    }

    bool DetectObject()
	{
        Collider2D coll = Physics2D.OverlapCircle(interractionPoint.position, interractionRadius, interractionLayer);
        
        if(coll == null)
		{
            currentObject = null;
            return false;
		} else
		{
            currentObject = coll.gameObject;
            return true;
		}
	}

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(interractionPoint.position, interractionRadius); 
 	}

    public void PickUpItem(GameObject item)
	{
        pickedItems.Add(item);
	}
    public void ExamineItem(Interactable item)
    {
        if (isExamining)
		{
            CloseExamineWindow();
        }
        else
		{
            OpenExamineWindow(item);
        }
    }

    public void OpenExamineWindow(Interactable item)
	{
        isExamining = true;
        GetComponent<Movement>().canMove = false;


        examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        examineImage.color = item.GetComponent<SpriteRenderer>().color;
        examineText.text = item.descriptionText;

        examineWindow.SetActive(true);
    }

    public void CloseExamineWindow()
	{
        examineWindow.SetActive(false);
        isExamining = false;
        GetComponent<Movement>().canMove = true;
    }
}
