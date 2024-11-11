using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;  // Referencia al Canvas de diálogo
    [SerializeField] private Image characterImage;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private string[] dialogues;
    [SerializeField] private Transform player;  // Referencia al jugador
    [SerializeField] private float interactionDistance = 3.0f;  // Distancia de interacción

    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    protected virtual void Start()
    {
        dialogueCanvas.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        // Calcular la distancia entre el jugador y el NPC
        float distance = Vector3.Distance(player.position, transform.position);

        // Verificar si están dentro de la distancia de interacción y se presiona "F"
        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.F) && !isDialogueActive)
        {
            StartDialogue();
        }

        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceDialogue();
        }
    }

    private void StartDialogue()
    {
        Debug.Log("Iniciando diálogo");
        isDialogueActive = true;
        currentDialogueIndex = 0;
        dialogueCanvas.gameObject.SetActive(true);  // Activa el Canvas
        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentDialogueIndex];
            characterImage.sprite = characterSprite;
        }
        else
        {
            EndDialogue();
        }
    }

    private void AdvanceDialogue()
    {
        currentDialogueIndex++;
        ShowDialogue();
    }

    private void EndDialogue()
    {
        isDialogueActive = false;
        dialogueCanvas.gameObject.SetActive(false);  // Desactiva el Canvas
    }
}
