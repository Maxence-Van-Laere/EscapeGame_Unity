using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInventoryManager : MonoBehaviour
{
    public Player_Inventory playerInventory;
    public TextMeshProUGUI inventoryText;
    public string messageDefault;

    // Start is called before the first frame update
    void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player_Inventory n'est pas assigné !");
        }

        if (inventoryText == null)
        {
            Debug.LogError("TextMeshProUGUI n'est pas assigné !");
        }

        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (playerInventory == null || inventoryText == null) return;

        // Construire le texte de l'inventaire
        inventoryText.text = "Inventaire :\n";
        for (int i = 0; i < playerInventory.nbrItems; i++)
        {
            inventoryText.text += $"- {playerInventory.inventory[i]}\n";
        }

        if (playerInventory.nbrItems == 0)
        {
            inventoryText.text += messageDefault;
        }
    }

    void Update()
    {

    }


}
