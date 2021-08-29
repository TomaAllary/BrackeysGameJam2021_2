using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutroMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("RessourcesCollected").GetComponent<Text>().text = "You gathered " + DataFile.nbBranches + " branches, " + DataFile.nbRocks + " rocks, and " + DataFile.nbHorns + " Cubes of Chaos!";
        GameObject.Find("goatsKilled").GetComponent<Text>().text = "You managed to kill " + DataFile.nbGoats + " goats!";
        GameObject.Find("WavesSurvived").GetComponent<Text>().text = "You survived  " + DataFile.nbWaves + " waves!";
        GameObject.Find("BuildingsConstructed").GetComponent<Text>().text = "You built " + DataFile.nbBuild + " Buildings!";
        GameObject.Find("BuildingsDestroyed").GetComponent<Text>().text = "Out of which the goats destroyed " + DataFile.nbDestroyed+ "!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quit()
    {
        Application.Quit();
    }

    public void loadMainMenu()
    {
        SceneManager.LoadScene("Intro");
    }

    public void playAgain()
    {
        SceneManager.LoadScene("InGame");
    }

    public void seeCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
