using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class Activate_Lever : MonoBehaviour
{
    [Header("Levier")]
    private GameObject levier;
    [SerializeField] private string nomTriggerLevier;
    [SerializeField] private Animator levierAnimator;

    [Header("Herse")]
    [SerializeField] private string nomTriggerOpenHerse;
    [SerializeField] private Animator herseAnimator;

    [Header("Paramètres")]
    [SerializeField] private KeyCode actionKey = KeyCode.E;
    [SerializeField] private float activeHerseDelai;

    [Header("Audio")]
    [SerializeField] private AudioSource herseAudioSource; // AudioSource pour jouer le son d'ouverture
    [SerializeField] private AudioClip herseOpenClip;      // Clip audio de l'ouverture de la herse
    public float tpsAttente;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text messageUI;
    [SerializeField] private string messageInteract; // Message interactif

    private bool joueurProche = false;
    private bool levierActive = false;

    void Update()
    {
        if (joueurProche && Input.GetKeyDown(actionKey) && !levierActive)
        {
            ActiveLevier();
        }
    }

    private void ActiveLevier()
    {
        levierActive = !levierActive;

        if (levierAnimator != null)
        {
            levierAnimator.SetTrigger(nomTriggerLevier);
            Debug.Log("Levier activé");
            ShowMessage("");
        }
        else
        {
            Debug.LogError("Aucun Animator assigné au levier détecté");
        }

        // Lance le délai pour ouvrir la herse
        Invoke(nameof(OpenHerseGate), activeHerseDelai);
    }

    private void OpenHerseGate()
    {
        if (herseAnimator != null)
        {
            herseAnimator.SetTrigger(nomTriggerOpenHerse);
            Debug.Log("Herse ouverte !");
        }
        else
        {
            Debug.LogError("Aucun Animator assigné à la herse détecté");
        }
    }

    private void PlayHerseOpenSound()
    {
        if (herseAudioSource != null && herseOpenClip != null)
        {
            herseAudioSource.clip = herseOpenClip;
            herseAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource ou AudioClip pour la herse non assigné.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurProche = true;
            if(!levierActive)
            {
                ShowMessage(messageInteract);
                Debug.Log("Le joueur entre dans la zone du levier");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurProche = false;
            Debug.Log("Le joueur a quitté la zone du levier.");
            ShowMessage("");
        }
    }

    private void ShowMessage(string message)
    {
        if (messageUI != null)
        {
            messageUI.text = message;
        }
    }
}
