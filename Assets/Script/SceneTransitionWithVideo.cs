using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransitionWithVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Référence au Video Player
    public int nextSceneIndex;       // Indice de la prochaine scène
    private bool isTransitioning = false; // Vérifie si la transition est en cours
    public float videoDuration = 12f;    // Durée d'attente en secondes (correspondant à la durée de la vidéo)

    public void StartTransition(int sceneIndex)
    {
        if (!isTransitioning)
        {
            nextSceneIndex = sceneIndex;
            StartCoroutine(PlayVideoAndChangeScene());
        }
    }

    private IEnumerator PlayVideoAndChangeScene()
    {
        isTransitioning = true;

        // Active l'objet du Video Player (au cas où il est désactivé par défaut)
        videoPlayer.gameObject.SetActive(true);

        // Démarre la lecture de la vidéo
        videoPlayer.Play();

        // Attend le temps de lecture spécifié (12 secondes ici)
        yield return new WaitForSeconds(videoDuration);

        // Charge la nouvelle scène une fois le temps écoulé
        SceneManager.LoadScene(nextSceneIndex);
    }

}
