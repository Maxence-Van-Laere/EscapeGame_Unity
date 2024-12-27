using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Open_Door : MonoBehaviour
{
    private Player_Inventory inventaire;
    [SerializeField] private string clefRequise; //Clef nécessaire pour ouvrir cette porte, modulable pour chaque porte dans l'Inspector du GameObject 
    private Animator doorAnimator;
    [SerializeField] private string nomTriggerOpen;
    [SerializeField] private AudioClip openSound; // Le clip audio du son d'ouverture de la porte
    private AudioSource audioSource;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GameObject joueur = GameObject.FindWithTag("Player");
        if (joueur != null)
        {
            inventaire = joueur.GetComponent<Player_Inventory>();
            if (inventaire == null)
            {
                Debug.LogError("Le joueur n'a pas de composant Player_Inventory.");
            }
        }
        else
        {
            Debug.LogError("Aucun GameObject avec le tag 'Player' trouvé dans la scène.");
        }

        /*if (doorAnimator != null)
        {
            doorAnimator.SetFloat("openAngle", rotationAngle);
            Debug.Log($"openAngle défini à {rotationAngle}");
        }
        else Debug.LogError("L'animator n'a pas pu être trouvé sur cet objet");*/
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inventaire != null && inventaire.HasItem(clefRequise))
            {
                if (doorAnimator != null)
                {
                    doorAnimator.SetTrigger(nomTriggerOpen);
                    inventaire.RemoveItem(clefRequise);
                    PlayOpenSound();
                }
            }
            else Debug.Log("Le joueur ne possède pas la clef requise");
            
        }

    }

    private void PlayOpenSound()
    {
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    /*private void doorAnimation()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }

    private System.Collections.IEnumerator OpenDoor()
    {
        estOuverte = true;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, rotationAngle, 0);

        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time);
            yield return null;
        }

        // Assurez-vous que la porte termine exactement à l'angle souhaité
        transform.rotation = targetRotation;
    }*/
}
