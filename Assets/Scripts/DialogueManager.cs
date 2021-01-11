using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private Text _dialogueText;
    [SerializeField] private float _textSpeed;

    private bool _isPrinting;
    private bool _dialogueDone;
    private int _dialogueLineIndex;
    private int _previousLetterIndex = 0;
    private string[] dialogueLines;
    private AudioSource _audioPlayer;
    


    // Start is called before the first frame update
    void Start()
    {
        _dialogueLineIndex = 0;
        _isPrinting = false;
        _dialogueText.text = "";
        _audioPlayer = GetComponent<AudioSource>();
    }

    public void SetDialogue(string[] dialogue)
    {
        _dialogueDone = false;
        _dialogueLineIndex = 0;
        dialogueLines = dialogue;
    }

    public void PrintDialogue()
    {
        if (_isPrinting)
        {
            _isPrinting = false;
        }
        else
        {
            _isPrinting = true;
            StartCoroutine(PrintLine(dialogueLines[_dialogueLineIndex]));
            _dialogueLineIndex++;
        }
        if(_dialogueLineIndex == dialogueLines.Length)
        {
            _dialogueDone = true;
        }
    }

    private IEnumerator PrintLine(string lineToPrint)
    {
        float startTime = Time.time;
        string currentString;

        while (_isPrinting)
        {
            float elapsedTime = Time.time - startTime;
            int lettersToPrint = (int) (elapsedTime / _textSpeed);
            if(lettersToPrint >= lineToPrint.Length)
            {
                _isPrinting = false;
                break;
            }
            currentString = lineToPrint.Substring(0, lettersToPrint);

            if(_previousLetterIndex != lettersToPrint) _audioPlayer.Play();

            _dialogueText.text = currentString;
            _previousLetterIndex = lettersToPrint;
            yield return null;
        }
        _audioPlayer.Play();

        _dialogueText.text = lineToPrint;
    }

    public void ToggleDialogueBox(bool active)
    {
        _dialogueBox.SetActive(active);
    }

    public bool IsDone()
    {
        return _dialogueDone;
    }

    public void ResetDialogue()
    {
        _dialogueDone = false;
        _isPrinting = false;
        _dialogueLineIndex = 0;
    }
}
