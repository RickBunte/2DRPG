using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    [SerializeField] private string[] _dialogueLines;
    private DialogueManager _dialogueManager;
    private PlayerController _playerController;
    private bool _playerNearby;
    
    void Start()
    {
        _dialogueManager = FindObjectOfType<DialogueManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C) && _playerNearby)
        {
            if (!_dialogueManager.IsDone())
            {
                _dialogueManager.ToggleDialogueBox(true);
                _dialogueManager.PrintDialogue();
            }
            else
            {
                _dialogueManager.ToggleDialogueBox(false);
                _dialogueManager.ResetDialogue();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            _dialogueManager.SetDialogue(_dialogueLines);
            _playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerNearby = false;
        }
    }
}
