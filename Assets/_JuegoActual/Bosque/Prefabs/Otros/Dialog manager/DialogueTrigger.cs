using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public bool startedDialog,dialogLocker,healAtEnd;
    public float dialogSpeed;
    public int sentenceCount;
    public int[] cameraIndexes;
    private void Start()
    {
        dialogLocker = false;
        startedDialog = false;
    }

   
    public void TriggerDialogue() 
    {
        if(dialogLocker == false)
        {
            dialogLocker = true;
            FindObjectOfType<DialogManager>().currentTrigger = this;
            startedDialog = true;
            FindObjectOfType<DialogManager>().StartDialogue(dialogue);

        }
       

    }
    public void OnSkip(InputValue value)
    {
        if(startedDialog == true)
        {
            FindObjectOfType<DialogManager>().skipDialog();
        }

        Debug.Log("Input works");

    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
    }

}
