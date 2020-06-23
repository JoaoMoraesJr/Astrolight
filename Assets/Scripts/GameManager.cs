using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject spaceShip;
    public GameObject scenario;
    public GameObject start;
    public GameObject quit;
    public int nextLevel;
    public bool showTutorial = false;

    // Start is called before the first frame update
    void Start()
    {
        if (showTutorial)
        {
            GetComponent<Animator>().SetBool("showTutorial", true);
        }
        if (nextLevel == 0)
        {
            Invoke("fadeInNextStage", 75);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    public void StartGame()
    {
        this.spaceShip.GetComponent<Animator>().SetBool("Start", true);
        this.scenario.GetComponent<Animator>().SetBool("Start", true);
        start.SetActive(false);
        quit.SetActive(false);
        Invoke("fadeInNextStage", 25);
    }

    public void fadeInNextStage()
    {
        this.GetComponent<Animator>().SetBool("FadeIn", true);
        Invoke("nextStage", 3);
    }

    public void nextStage()
    {
        if (nextLevel == 0)
        {
            SceneManager.LoadScene("Intro");
        }
        else
        {
            SceneManager.LoadScene("Level" + nextLevel);
        }
    }

    public void completeStage()
    {
        GetComponent<Animator>().SetBool("StageCompleted", true);
        Invoke("nextStage", 6);
    }
}
