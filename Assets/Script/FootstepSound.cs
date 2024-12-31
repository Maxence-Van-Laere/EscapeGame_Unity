using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepSound; // Le clip audio des bruits de pas
    private AudioSource audioSource;
    private CharacterController characterController;

    public float stopDelay = 0.1f;

    private float stopTimer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();

        audioSource.clip = footstepSound;
        audioSource.loop = true;
    }

    void Update()
    {
        // Vérifie si le personnage est au sol et en mouvement
        if (characterController.velocity.magnitude > 0.0f)
        {
            // Réinitialise le timer et joue le son si ce n'est pas déjà fait
            stopTimer = 0f;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Incrémente le timer si le personnage est immobile
            stopTimer += Time.deltaTime;

            // Arrête le son si le personnage est immobile depuis assez longtemps
            if (stopTimer >= stopDelay && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
