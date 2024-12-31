using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakObject : MonoBehaviour
{
    [SerializeField] private TMP_Text messageUI;

    [Header("Coffre Settings")]
    [SerializeField] private int health = 5; // Nombre de coups n�cessaires pour casser le coffre
    //[SerializeField] private GameObject chestBrokenPrefab; // Pr�fabriqu� pour l'apparence du coffre cass�
    //[SerializeField] private AudioClip sonCoup; // Son jou� lorsque le coffre est frapp�
    //[SerializeField] private AudioClip sonCasse; // Son jou� lorsque le coffre se casse
    private AudioSource audioSource;
    [SerializeField] private KeyCode actionKey;

    public GameObject clefDansCoffre;

    [Header("Joueur Settings")]
    [SerializeField] private string playerTag = "Player"; // Tag du joueur
    [SerializeField] private string requiredItemName = "Ep�e";

    private Player_Inventory playerInventory; // R�f�rence � l'inventaire du joueur
    private bool playerInRange = false; // Indique si le joueur est dans la zone de collision

    void Start()
    {
        /*audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Aucun AudioSource trouv� sur l'objet. Ajoutez-en un si vous voulez du son.");
        }*/
        clefDansCoffre.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(actionKey)) 
        {
            if (playerInventory != null && playerInventory.HasItem(requiredItemName))
            {
                TakeDamage();
            }
            else
            {
                Debug.Log("Vous n'avez pas l'objet requis pour casser ce coffre !");
                ShowMessage("Vous n'avez pas l'objet requis pour casser ce coffre !");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            ShowMessage($"Appuyez sur {actionKey.ToString()} pour frapper le coffre.");
            playerInventory = other.GetComponent<Player_Inventory>();
            if (playerInventory == null)
            {
                Debug.LogError("Aucun composant Player_Inventory trouv� sur le joueur.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            playerInventory = null;
            ShowMessage("");
        }
    }

    private void TakeDamage()
    {
        health--;
        //PlaySound(hitSound);
        
        if (health <= 0)
        {
            BreakChest();
        }
    }

    private void BreakChest()
    {
        //PlaySound(breakSound);

        /*if (chestBrokenPrefab != null)
        {
            Instantiate(chestBrokenPrefab, transform.position, transform.rotation);
        }*/

        // D�truire l'objet coffre
        Destroy(gameObject);
        clefDansCoffre.SetActive (true);
        ShowMessage("");
    }

    /*private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }*/

    private void ShowMessage(string message)
    {
        if (messageUI != null)
        {
            messageUI.text = message;
        }
    }

}
