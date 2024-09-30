using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverSound; // Arraste seu AudioClip aqui no Inspector
    public AudioSource audioSource;

    private void Start()
    {
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayHoverSound();
    }

    private void PlayHoverSound()
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
