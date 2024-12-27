using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioClip footstepSound; // Le clip audio des bruits de pas
    private AudioSource audioSource;
    private CharacterController characterController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(footstepSound);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
