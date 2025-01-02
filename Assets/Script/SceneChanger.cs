using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private SceneTransitionWithVideo transitionManager;
    public GameObject portail;

    public int targetSceneIndex; // Indice de la sc�ne � charger

    void Start()
    {
        // R�cup�re le gestionnaire de transition
        transitionManager = FindObjectOfType<SceneTransitionWithVideo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet qui entre dans le trigger est le joueur
        if (other.CompareTag("Player"))
        {
            // D�clenche la transition avec l'indice de la sc�ne
            transitionManager.StartTransition(targetSceneIndex);
            portail.SetActive(false);
        }
    }
}
