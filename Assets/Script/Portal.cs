using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private ObjectOpen objetAdetecter;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objetAdetecter.estOuvert)
        {
            gameObject.SetActive(true);
        }
    }
}
