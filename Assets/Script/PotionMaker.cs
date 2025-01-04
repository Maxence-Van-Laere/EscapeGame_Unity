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
        // Récupérer l'inventaire du joueur
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
        // Vérifier si le joueur jette les ingrédients dans la marmite
        if (joueurDansZone && Input.GetKeyDown(KeyCode.E) && marmite1 != null && !marmiteRemplie)
        {
            TryAddIngredients();
        }

        // Vérifier si le joueur boit la potion
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
            // Vérifier si le joueur a tous les ingrédients requis
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
                // Retirer la dernière virgule et espace
                ingredientsManquants = ingredientsManquants.TrimEnd(',', ' ');

                // Afficher les ingrédients manquants
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
            // Initialisation de la liste des ingrédients manquants
            List<string> ingredientsManquants = new List<string>();

            foreach (string ingredient in ingredientsRequis)
            {
                // Vérifie si l'ingrédient est dans l'inventaire
                if (inventaire.HasItem(ingredient))
                {
                    // Retire immédiatement l'ingrédient s'il est disponible
                    inventaire.RemoveItem(ingredient);
                    Debug.Log($"{ingredient} ajouté à la marmite et retiré de l'inventaire.");
                }
                else
                {
                    // Ajoute à la liste des ingrédients manquants
                    ingredientsManquants.Add(ingredient);
                }
            }

            // Si des ingrédients manquent, on affiche un message
            if (ingredientsManquants.Count > 0)
            {
                string message = "Il manque : " + string.Join(", ", ingredientsManquants);
                ShowMessage(message);
            }
            else
            {
                // Tous les ingrédients sont ajoutés avec succès
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
                ShowMessage("[E] pour jeter les ingrédients");
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
