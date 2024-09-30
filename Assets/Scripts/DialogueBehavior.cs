using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBehavior : MonoBehaviour
{
    public string DialogueToShow;
    public bool endDialogue;

    private Coroutine dialogueCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.dialogueBox.SetActive(true);
            // Se já houver uma coroutine em execução, pare-a antes de iniciar uma nova
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
            }
            dialogueCoroutine = StartCoroutine(ShowDialogue(DialogueToShow));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (endDialogue)
        {
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine); // Para a coroutine em execução
                dialogueCoroutine = null; // Limpa a referência
            }
            DialogueManager.Instance.dialogueText.text = ""; // Limpa o texto
            DialogueManager.Instance.dialogueBox.SetActive(false); // Oculta a caixa de diálogo
        }
    }

    private IEnumerator ShowDialogue(string dialogue)
    {
        Text dialogueText = DialogueManager.Instance.dialogueText;
        dialogueText.text = ""; // Limpa o texto anterior

        foreach (char letter in dialogue)
        {
            dialogueText.text += letter; // Adiciona uma letra de cada vez
            yield return new WaitForSeconds(0.02f); // Ajuste o tempo para exibir a letra
        }
    }
}
