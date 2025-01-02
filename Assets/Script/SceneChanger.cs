using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private SceneTransitionWithVideo transitionManager;
    public GameObject portail;

    public int targetSceneIndex; // Indice de la scène à charger

    void Start()
    {
        // Récupère le gestionnaire de transition
        transitionManager = FindObjectOfType<SceneTransitionWithVideo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet qui entre dans le trigger est le joueur
        if (other.CompareTag("Player"))
        {
            // Déclenche la transition avec l'indice de la scène
            transitionManager.StartTransition(targetSceneIndex);
            portail.SetActive(false);
        }
    }
}
