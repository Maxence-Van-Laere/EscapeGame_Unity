using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public string[] inventory = new string[10];
    private int nbrItems = 0;
    //private GameObject itemDrop;
    

    public void AddItem(string nomItem)
    {
        if (nbrItems < inventory.Length) 
        {
            inventory[nbrItems] = nomItem;
            nbrItems++;
            Debug.Log($"{nomItem} a été ajouté à l'inventaire.");
        }
        else
        {
            Debug.Log("L'inventaire est plein. Impossible d'ajouter l'objet : " + nomItem);
        }

        ShowInventory();
    }

    public bool HasItem(string nomItem)
    {
        for (int i = 0; i < nbrItems; i++)
        {
            if (inventory[i] == nomItem)
            {
                return true;
            }
        }
        return false;
    }


    public void RemoveItem(string nomItem)
    {
        for (int i = 0; i < nbrItems; i++)
        {
            if (inventory[i] == nomItem)
            {
                // Décale tous les éléments suivants pour combler l'espace
                for (int j = i; j < nbrItems - 1; j++)
                {
                    inventory[j] = inventory[j + 1];
                }
                inventory[nbrItems - 1] = null; 
                nbrItems--;
                Debug.Log($"{nomItem} a été retiré de l'inventaire.");
                ShowInventory();
                return;
            }
        }
        Debug.Log($"L'objet {nomItem} n'est pas dans l'inventaire.");

        
    }

    /*public void DropItem(string nomItem)
    {
        if (!HasItem(nomItem))
        {
            return;
        }

        RemoveItem(nomItem);

        if (itemDrop != null)
        {
            Vector3 spawnPosition = transform.position * 2;
            Instantiate(itemDrop, spawnPosition, Quaternion.identity);
            Debug.Log($"L'objet {nomItem} a été lâché devant le joueur.");
        }
        else Debug.LogError($"Aucun objet n'a pu être laché");
    }*/

    public void ShowInventory()
    {
        Debug.Log("L'inventaire contient:");
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == "")
            {
                Debug.Log($"Pas d'item dans le slot {i + 1}");
            }
            else Debug.Log($"- slot {i + 1}: {inventory[i]}\n");
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
