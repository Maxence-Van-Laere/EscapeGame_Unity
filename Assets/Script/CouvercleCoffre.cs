using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private ObjectOpen cadenas;
    [SerializeField] public GameObject clefDansCoffre; 
    [SerializeField] private Animator coffreAnimator; 
    [SerializeField] private string triggerOuvertureCoffre;



    private bool aEteOuvert = false;

    // Start is called before the first frame update
    void Start()
    {
       clefDansCoffre.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!aEteOuvert && cadenas != null && cadenas.estOuvert)
        {
            ouvertureCoffre();
        }
    }

    private void ouvertureCoffre()
    {
        if (coffreAnimator != null && !string.IsNullOrEmpty(triggerOuvertureCoffre))
        {
            coffreAnimator.SetTrigger(triggerOuvertureCoffre);
            clefDansCoffre.SetActive(true);
            aEteOuvert = true; // Empêche de rouvrir le couvercle
            Debug.Log("Couvercle du coffre ouvert.");
        }
    }
}
