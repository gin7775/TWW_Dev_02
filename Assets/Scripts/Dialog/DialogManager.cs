using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public PlayerStats player;
    public DialogueTrigger currentTrigger;
    public static DialogManager dialogManager;
    private Queue<string> sentences;
    public GameObject textHolder;
    public TextMeshProUGUI textComponent,nameText;
    public float textspeed;
    private int textIndex;
    public string currentSentence;
    public Animator textAnim;
    

    private void Awake()
    {
               
            if (dialogManager == null)
            {
                dialogManager = this;
                DontDestroyOnLoad(this);

            }
            else if (dialogManager != this)
            {
                Destroy(gameObject);

            }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        textHolder = GameObject.FindGameObjectWithTag("TextHolder");
        Debug.Log("Esta ocurriendo el start");
        textAnim = textHolder.gameObject.GetComponent<Animator>();
        sentences = new Queue<string>();
        textComponent = GameObject.FindGameObjectWithTag("TextComponent").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.FindGameObjectWithTag("TextName").GetComponent<TextMeshProUGUI>();

    }

    public void StartDialogue(Dialogue dialogue)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        textHolder = GameObject.FindGameObjectWithTag("TextHolder");
        Debug.Log("Esta ocurriendo el start");
        textAnim = textHolder.gameObject.GetComponent<Animator>();
        sentences = new Queue<string>();
        textComponent = GameObject.FindGameObjectWithTag("TextComponent").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.FindGameObjectWithTag("TextName").GetComponent<TextMeshProUGUI>();
        Debug.Log("inicia el dialogo");

        textspeed = currentTrigger.dialogSpeed;
        textIndex = 0;
        nameText.text = dialogue.name;
        sentences.Clear();
        textAnim.SetTrigger("Activate");

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        currentSentence = sentence;
        currentTrigger.sentenceCount++;
        //Cambiar camara aqui 
        /*
        if (currentcamera != currentTrigger.cameraIndexes[currentTrigger.sentenceCount])
        {
         currentcamera = currentTrigger.cameraIndexes[currentTrigger.sentenceCount]
         
        }*/
        Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }
    public void EndDialog()
    {
        currentTrigger.startedDialog = false;
        StopAllCoroutines();
        textAnim.SetTrigger("DeActivate");
        textComponent.text = "";
        if (currentTrigger.healAtEnd == true)
        {
            player.increaseHeal();
        }
        Debug.Log("it ends dialog");
    }

    IEnumerator TypeSentence(string sentence)
    {
        textComponent.text = "";

        foreach (char letter in sentence.ToCharArray()) 
        {
            textComponent.text += letter;

            yield return new WaitForSeconds(textspeed);

        }

    }
    public void skipDialog()
    {
        if(textComponent.text == currentSentence)
        {
            DisplayNextSentence();
            Debug.Log("NextDialog");
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = currentSentence;
            Debug.Log("FillDialog");


        }
    }
   
}
