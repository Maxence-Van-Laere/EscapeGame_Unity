using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject objetAActiver;
    private ObjectOpen objetOuvert;

    void Start()
    {
        objetOuvert = GetComponent<ObjectOpen>();
        if (objetAActiver == null)
        {
            Debug.Log("L'objet � activer n'est pas assign�");
        }
        else
        {
            objetAActiver.SetActive(false);
        }
    }

    void Update()
    {
        if (objetOuvert.estOuvert)
        {
            objetAActiver.SetActive(true);
        }  
    }
}

