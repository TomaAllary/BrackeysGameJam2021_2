using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    public AudioSource mainAudio;
    public AudioSource storyAudio;
    bool storyOn;
    public AudioClip audioClipStory;
    // Start is called before the first frame update
    void Start()
    {
        storyOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playStory()
    {
        if (!storyOn)
        {
            mainAudio.volume = .1f;
            storyAudio.PlayOneShot(audioClipStory);
            storyOn = true;
            StartCoroutine(WaitForStory());
        }
        else
        {
            storyAudio.Stop();
            mainAudio.volume = 1f;
            storyOn = false;
        }
    }
    IEnumerator WaitForStory()
    {
        yield return new WaitUntil(() => storyAudio.isPlaying == false);
        mainAudio.volume = 1.0f;
        storyOn = false;
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void playGame()
    {
        SceneManager.LoadScene("InGame");
    }

}
