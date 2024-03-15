using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Parsed;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NPCNameText;
    [SerializeField] TextMeshProUGUI NPCDialogueText;
    [SerializeField] float TypeSpeed = 1f;

    Queue<string> _paragraphs= new Queue<string>();
    bool _conversationStarted;
    bool _conversationEnded;
    bool _isTyping;

    string p;

    Coroutine _typeDialogueCoroutine;

    const string HTML_ALPHA = "<color=#00000000>";
    const float MAX_TYPE_TIME = 0.1f;

    public void DisplayNextParagraph()
    {
        //if there is nothing int the queue
  
        if (_conversationEnded && !_isTyping && _paragraphs.Count == 0)
        {
            //end the converstion
            EndConversation();
            return;
        }
        

        //if there is something in the queue
        if (!_isTyping)
        {
            p = _paragraphs.Dequeue();
            _typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }
        //conversation ends early
        else
        { 
            FinishParagraphEarly();
        }

        //update conversation ended bool
        if (_paragraphs.Count == 0)
            _conversationEnded = true;
    }
    //NEED TO BE PUBLIC FOR BUTTON!!!
    public void StartConversation(DialogueText dialogueText)
    { 
        //activate gameobject
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);

        _conversationStarted = true;
        NPCNameText.text = dialogueText.SpeakerName;

        //add dialogue text to queue
        for (int i = 0; i < dialogueText.Pharagraphs.Length; i++)
        {
            _paragraphs.Enqueue(dialogueText.Pharagraphs[i]);
        }
        
        p = _paragraphs.Dequeue();
        _typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        
    }
     void EndConversation()
    {
        //clear the queue
        _paragraphs.Clear();

        _conversationEnded = false;
        _conversationStarted = false;

        if(gameObject.activeSelf) 
            gameObject.SetActive(false);
    }

    IEnumerator TypeDialogueText(string p)
    {
        _isTyping = true;
        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            yield return new WaitForSeconds(MAX_TYPE_TIME/ TypeSpeed);
        }

        _isTyping = false;
    }

    void FinishParagraphEarly()
    {
        //stop coroutine
        StopCoroutine(_typeDialogueCoroutine);
        //finish displaying text
        NPCDialogueText.text = p;
        //update isTyping bool
        _isTyping= false;
    }
    public bool GetConversationStarted() 
    {
        return _conversationStarted;
    }
}
