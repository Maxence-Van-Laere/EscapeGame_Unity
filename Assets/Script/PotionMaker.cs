using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class PotionMaker : MonoBehaviour
{
    [SerializeField] private GameObject marmite1;
    [SerializeField] private GameObject marmite2; 
    [SerializeField] private string[] ingredientsRequis;
    [SerializeField] private GameObject clefCachee;
    
    /*private Vector3 marmite1Position; 
    private Quaternion marmite1Rotation;*/

    [SerializeField] private TMP_Text messageText; 
    private Player_Inventory inventaire; 
    private bool marmiteRemplie = false;
    private bool joueurDansZone = false; 
    

    void Start()
    {
        // R�cup�rer l'inventaire du joueur
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
            Debug.LogError("Aucun GameObject avec le tag 'Player' trouv� dans la sc�ne.");
        }

        if (marmite2 != null)
        {
            marmite2.SetActive(false);
        }
        if (clefCachee != null)
        {
            clefCachee.SetActive(false);
        }

        if (messageText != null)
        {
            messageText.text = "";
        }

    }

    void Update()
    {
        // V�rifier si le joueur jette les ingr�dients dans la marmite
        if (joueurDansZone && Input.GetKeyDown(KeyCode.E) && marmite1 != null && !marmiteRemplie)
        {
            TryAddIngredients();
        }

        // V�rifier si le joueur boit la potion
        if (joueurDansZone && Input.GetKeyDown(KeyCode.F) && marmiteRemplie)
        {
            DrinkPotion();
        }
    }


    //NE MET PAS A JOUR LES ITEMS UN A UN
    private void TryAddIngredients()
    {
        if (inventaire != null)
        {
            // V�rifier si le joueur a tous les ingr�dients requis
            string ingredientsManquants = "";
            foreach (string ingredient in ingredientsRequis)
            {
                if (!inventaire.HasItem(ingredient))
                {
                    ingredientsManquants += ingredient + ", ";
                }
            }

            if (!string.IsNullOrEmpty(ingredientsManquants))
            {
                // Retirer la derni�re virgule et espace
                ingredientsManquants = ingredientsManquants.TrimEnd(',', ' ');

                // Afficher les ingr�dients manquants
                ShowMessage($"Il manque : {ingredientsManquants}");
                return;
            }

            foreach (string ingredient in ingredientsRequis)
            {
                inventaire.RemoveItem(ingredient);
            }

            marmiteRemplie = true;
            ShowMessage("[F] pour boire le bouillon");
        }
    }


    //SUPPRIME LES OBJETS UN A UN MAIS BESOIN DE METTRE TOUS LES INGREDIENTS EN MEME TEMPS POUR QUE CA MARCHE
    /*private void TryAddIngredients()
    {
        if (inventaire != null)
        {
            // Initialisation de la liste des ingr�dients manquants
            List<string> ingredientsManquants = new List<string>();

            foreach (string ingredient in ingredientsRequis)
            {
                // V�rifie si l'ingr�dient est dans l'inventaire
                if (inventaire.HasItem(ingredient))
                {
                    // Retire imm�diatement l'ingr�dient s'il est disponible
                    inventaire.RemoveItem(ingredient);
                    Debug.Log($"{ingredient} ajout� � la marmite et retir� de l'inventaire.");
                }
                else
                {
                    // Ajoute � la liste des ingr�dients manquants
                    ingredientsManquants.Add(ingredient);
                }
            }

            // Si des ingr�dients manquent, on affiche un message
            if (ingredientsManquants.Count > 0)
            {
                string message = "Il manque : " + string.Join(", ", ingredientsManquants);
                ShowMessage(message);
            }
            else
            {
                // Tous les ingr�dients sont ajout�s avec succ�s
                marmiteRemplie = true;
                ShowMessage("[F] pour boire le bouillon");
            }
        }
    }*/

    private void DrinkPotion()
    {
        /*marmite1Position = marmite1.transform.position;
        marmite1Rotation = marmite1.transform.rotation; */    //MARMITE DEJA POSITIONNEE AU BON ENDROIT

        if (marmite1 != null)
        {
            marmite1.SetActive(false); 
        }

        if (marmite2 != null)
        {
            /*marmite2.transform.position = marmite1Position;
            marmite2.transform.rotation = marmite1Rotation;*/
            marmite2.SetActive(true);
            clefCachee.SetActive(true);
        }

        ShowMessage("Vous avez bu la potion !");
        CancelInvoke("ClearMessage"); 
        Invoke("ClearMessage", 2f);
    }

    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }
    }

    private void ClearMessage()
    {
        if (messageText != null)
        {
            messageText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = true;

            if (!marmiteRemplie)
            {
                ShowMessage("[E] pour jeter les ingr�dients");
            }
            else ShowMessage("[F] pour boire le bouillon");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurDansZone = false;
            ClearMessage();
        }
    }
}
