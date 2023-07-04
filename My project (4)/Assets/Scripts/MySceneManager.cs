using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    
        public static int sceneNumber;


    // Start is called before the first frame update
    void Start()
    {
        if (sceneNumber == 0)
        {
            StartCoroutine(ToMainMenu());
        }

        

        IEnumerator ToMainMenu()
        {
            yield return new WaitForSeconds(3);
            sceneNumber = 1;
            SceneManager.LoadScene(1);
        }
    }

}

    

