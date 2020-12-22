using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;

    public bool isDialogueActive;
    public string[] dialogueLines;
    public int currentLine;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.C))
        {
            currentLine++;
        }
        if(currentLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            isDialogueActive = false;
            currentLine = 0;
        }
        dialogueText.text = dialogueLines[currentLine];
    }

    //public void ShowDialogueBox(string dialogue)
    //{
    //    isDialogueActive = true;
    //    dialogueBox.SetActive(true);
    //    dialogueText.text = dialogue;
    //}

    public void ShowDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);

    }
}
