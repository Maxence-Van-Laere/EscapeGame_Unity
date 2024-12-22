using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Key_Pickup : MonoBehaviour
{
    private Player_Inventory inventaire;
    private GameObject clefProche;
    public TMP_Text messageText;

    // Start is called before the first frame update
    void Start()
    {
        inventaire = GetComponent<Player_Inventory>();
        if (inventaire == null)
        {
            Debug.LogError("Le composant 'Player_Inventory' est introuvable");
        }
        
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(clefProche != null && Input.GetKeyDown(KeyCode.E))
        {
            inventaire.AddItem(clefProche.name);
            Destroy(clefProche);

            clefProche = null;
            ClearMessage();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            //Debug.Log("Clé à proximité.");
            messageText.text = "Appuyez sur [E] pour ramasser la clé";
            clefProche = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            //Debug.Log("Vous vous êtes éloigné de la clé.");
            messageText.text = ""; 
            clefProche = null;
        }
    }

    private void ClearMessage()
    {
        messageText.text = "";
    }

}
