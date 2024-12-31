using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

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

            // Retirer les ingr�dients de l'inventaire
            foreach (string ingredient in ingredientsRequis)
            {
                inventaire.RemoveItem(ingredient);
            }

            marmiteRemplie = true;
            ShowMessage("[F] pour boire le bouillon");
        }
    }

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
