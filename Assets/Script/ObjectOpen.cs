using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;


public class ObjectOpen : MonoBehaviour
{
    private Player_Inventory inventaire;

    [Header("Configuration Ouverture")]
    [SerializeField] private string clefRequise; //Clef nécessaire pour ouvrir cette porte, modulable pour chaque porte dans l'Inspector du GameObject 
    private Animator doorAnimator;
    [SerializeField] private string nomTriggerOpen;
    [SerializeField] private KeyCode actionKey;

    [Header("Son")]
    [SerializeField] private AudioClip openSound; // Le clip audio du son d'ouverture de la porte
    private AudioSource audioSource;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text messageUI;
    [SerializeField] private string messageInteract = "[E] pour ouvrir"; // Message interactif
    [SerializeField] private string messageNoKey = "Vous ne possédez pas la clef requise"; // Message d'erreur
    [SerializeField] private float messageDuration = 2f; // Durée d'affichage des messages

    public bool estOuvert { get; private set; }

    private bool isPlayerInRange = false;


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
        if (isPlayerInRange && Input.GetKeyDown(actionKey))
        {
            TryOpenObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player") && !estOuvert)
            {
                isPlayerInRange = true;
                ShowMessage(messageInteract);
            }

            /*if (inventaire != null && inventaire.HasItem(clefRequise))
            {
                if (doorAnimator != null)
                {
                    doorAnimator.SetTrigger(nomTriggerOpen);
                    inventaire.RemoveItem(clefRequise);
                    PlayOpenSound();

                    estOuvert = true;
                }
            }
            else
            {
                
                Debug.Log("Le joueur ne possède pas la clef requise");
            }*/
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ShowMessage("");
        }
    }

    private void TryOpenObject()
    {
        if (estOuvert) return;

        if (inventaire != null && inventaire.HasItem(clefRequise))
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger(nomTriggerOpen);
                PlayOpenSound();
                estOuvert = true;
                Debug.Log("Ouverture effectuée");
                ShowMessage("");
                inventaire.RemoveItem(clefRequise);
            }
        }
        else
        {
            ShowMessage(messageNoKey);
        }
    }



    private void PlayOpenSound()
    {
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    private void ShowMessage(string message)
    {
        if (messageUI != null)
        {
            messageUI.text = message;
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        ShowMessage("");
    }

    private void ClearMessage()
    {
        if (messageUI != null)
        {
            messageUI.text = string.Empty;
            messageUI.gameObject.SetActive(false);
        }
    }
}
