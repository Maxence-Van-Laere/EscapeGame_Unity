using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class Object_Pickup : MonoBehaviour
{
    private Player_Inventory inventaire;
    private GameObject objetProche;
    public TMP_Text messageUI;

    // Start is called before the first frame update
    void Start()
    {
        inventaire = GetComponent<Player_Inventory>();
        if (inventaire == null)
        {
            Debug.LogError("Le composant 'Player_Inventory' est introuvable");
        }
        
        if (messageUI != null)
        {
            ShowMessage("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objetProche != null && Input.GetKeyDown(KeyCode.E))
        {
            if (objetProche.CompareTag("Torche"))
            {
                // Allume ou éteint la torche
                Torche torchScript = objetProche.GetComponent<Torche>();
                if (torchScript != null)
                {
                    torchScript.Toggle();
                }
            }
            else
            {
                inventaire.AddItem(objetProche.name);
                Destroy(objetProche);

            }
            objetProche = null;
            ShowMessage("");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            ShowMessage("[E] pour ramasser la clé");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Pomme de terre"))
        {
            ShowMessage("[E] pour ramasser la pomme de terre");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Pain"))
        {
            ShowMessage("[E] pour ramasser le pain");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Carotte"))
        {
            ShowMessage("[E] pour ramasser la carotte");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Epée"))
        {
            ShowMessage("[E] pour ramasser l'épée");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Viande"))
        {
            ShowMessage("[E] pour ramasser la viande");
            objetProche = other.gameObject;
        }
        else if (other.CompareTag("Torche"))
        {
            /*Torche torch = other.GetComponent<Torche>();
            if (torch != null)
            {
                string message = torch.EstAllume()
                ? "[E] pour éteindre la torche"
                : "[E] pour allumer la torche";
                ShowMessage(message);

                objetProche = other.gameObject;
            }*/
            ShowMessage("torche");
            objetProche = other.gameObject;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        ShowMessage("");
        objetProche = null;
        /*if (other.CompareTag("Key"))
        {
            messageText.text = ""; 
            objetProche = null;
        }*/
    }

    private void ShowMessage(string message)
    {
        if (messageUI != null)
        {
            messageUI.text = message;
        }
    }

}
