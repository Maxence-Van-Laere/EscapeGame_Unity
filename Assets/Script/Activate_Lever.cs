using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Lever : MonoBehaviour
{
    [Header("Levier")]
    private GameObject levier;
    [SerializeField] private string nomTriggerLevier;
    [SerializeField] private Animator levierAnimator;

    [Header("Herse")]
    [SerializeField] private string nomTriggerOpenHerse;
    [SerializeField] private Animator herseAnimator;

    [Header("Param�tres")]
    [SerializeField] private KeyCode actionKey = KeyCode.E;
    [SerializeField] private float activeHerseDelai;

    private bool joueurProche = false;
    private bool levierActive = false;


    void Start()
    {
        
        /*levierAnimator = GetComponent<Animator>();
        herseAnimator = GetComponent<Animator>();
        GameObject joueur = GameObject.FindWithTag("Player");*/
    }

    void Update()
    {
        if (joueurProche && Input.GetKeyDown(actionKey) && !levierActive)
        {
            ActiveLevier();
        }
    }

    private void ActiveLevier()
    {
        levierActive = !levierActive;


        if (levierAnimator != null)
        {
            levierAnimator.SetTrigger(nomTriggerLevier);
            Debug.Log("Levier activ�");
        }
        else
        {
            Debug.LogError("Aucun Animator assign� au levier d�tect�");
        }

        // Lance le d�lai pour ouvrir la herse
        Invoke(nameof(OpenHerseGate), activeHerseDelai);
    }

    private void OpenHerseGate()
    {
        if (herseAnimator != null)
        {
            herseAnimator.SetTrigger(nomTriggerOpenHerse);
            Debug.Log("Herse ouverte !");
        }
        else
        {
            Debug.LogError("Aucun Animator assign� � la herse d�tect�");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            joueurProche = true;
            Debug.Log("Le joueur entre dans la zone du levier");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            joueurProche = false;
            Debug.Log("Le joueur a quitt� la zone du levier.");
        }
    }
}
