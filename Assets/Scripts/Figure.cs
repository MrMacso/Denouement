using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : InteractableObject, ITalkable
{
    [SerializeField] DialogueText _dialogueText;
    [SerializeField] DialogueController _dialogueController;
    public override void Interact()
    {
        if(!_dialogueController.GetConversationStarted())
            Talk(_dialogueText);
    }
    public void Talk(DialogueText dialogueText)
    {
        _dialogueController.StartConversation(dialogueText);
    }


}
