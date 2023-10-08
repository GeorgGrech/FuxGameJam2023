using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryDisplay : MonoBehaviour
{

    [SerializeField] List<GameObject> panels;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowPanels());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowPanels()
    {
        foreach (GameObject panel in panels)
        {
            yield return new WaitForSeconds(1);
            panel.SetActive(true);
        }
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(2);
    }
}
