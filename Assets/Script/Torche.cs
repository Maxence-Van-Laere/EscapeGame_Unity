using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torche : MonoBehaviour
{
    private bool estAllume;
    public GameObject lumiereTorche;

    public void Toggle()
    {
        estAllume = !estAllume;
        Debug.Log($"La torche est maintenant {(estAllume ? "allumée" : "éteinte")}");

        if (lumiereTorche != null)
        {
            lumiereTorche.SetActive(estAllume);
        }
        else
        {
            Debug.LogWarning("TorchLight n'est pas assigné dans le script Torch.");
        }
    }

    void Start()
    {
        if (lumiereTorche.activeSelf)
        {
            estAllume=true;
            Debug.Log("L'objet est actif.");
        }
        else
        {
            estAllume=false;
            Debug.Log("L'objet n'est pas actif.");
        }
    }

    public bool EstAllume()
    {
        return estAllume;
    }
}
