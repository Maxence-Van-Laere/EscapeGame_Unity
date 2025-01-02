using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmePorte : MonoBehaviour
{
    [Header("Configuration Ouverture")]
    private Animator doorAnimator;
    [SerializeField] private string nomTriggerOpen;
    public Torche[] torchesToBeLit; 
    public Torche[] torchesToBeUnlit;

    [Header("Son")]
    [SerializeField] private AudioClip openSound;
    private AudioSource audioSource;

    public bool estOuvert { get; private set; } = false;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!estOuvert && verifTorches())
        {
            OpenDoor();
        }
    }

    private bool verifTorches()
    {
        foreach (Torche torch in torchesToBeLit)
        {
            if (torch == null || !torch.EstAllume())
            {
                Debug.Log($"La torche {torch.name} doit �tre allum�e mais ne l'est pas.");
                return false;
            }
        }

        foreach (Torche torch in torchesToBeUnlit)
        {
            if (torch == null || torch.EstAllume())
            {
                Debug.Log($"La torche {torch.name} doit �tre �teinte mais est allum�e.");
                return false;
            }
        }

        return true;
    }


    private void OpenDoor()
    {
        doorAnimator.SetTrigger(nomTriggerOpen);
        PlayOpenSound();
        estOuvert = true;
        Debug.Log("Ouverture porte sous-sol effectu�e");
    }

    private void PlayOpenSound()
    {
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }
}
