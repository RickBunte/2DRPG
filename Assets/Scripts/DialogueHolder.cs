using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    //public string dialogue;
    private DialogueManager dialogueManager;
    public string[] dialogueLines;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "player")
        {
            if (Input.GetKeyUp(KeyCode.C))
            {
                //dialogueManager.ShowDialogueBox(dialogue);

                if (!dialogueManager.isDialogueActive)
                {
                    dialogueManager.dialogueLines = dialogueLines;
                    dialogueManager.currentLine = 0;
                    dialogueManager.ShowDialogue();
                }
            }
        }
    }
}
