using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneTransitionWithVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // R�f�rence au Video Player
    public int nextSceneIndex;       // Indice de la prochaine sc�ne
    private bool isTransitioning = false; // V�rifie si la transition est en cours
    public float videoDuration = 12f;    // Dur�e d'attente en secondes (correspondant � la dur�e de la vid�o)

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

        // Active l'objet du Video Player (au cas o� il est d�sactiv� par d�faut)
        videoPlayer.gameObject.SetActive(true);

        // D�marre la lecture de la vid�o
        videoPlayer.Play();

        // Attend le temps de lecture sp�cifi� (12 secondes ici)
        yield return new WaitForSeconds(videoDuration);

        // Charge la nouvelle sc�ne une fois le temps �coul�
        SceneManager.LoadScene(nextSceneIndex);
    }

}
