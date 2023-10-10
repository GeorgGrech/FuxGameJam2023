using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneLife : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Lifetime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0);
    }
}
