using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private Slider soundsSlider;


    private Dictionary<int, float> soundsVolume = new Dictionary<int, float>();

    // Start is called before the first frame update
    void Start()
    {
        var sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audio in sources) {
            soundsVolume.Add(audio.GetInstanceID(), soundsSlider.value);
        }

        soundsSlider.onValueChanged.AddListener(HandleSoundVolumeChanged);
    }

    private void HandleSoundVolumeChanged(float volume) {
        var sources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audio in sources) {
            if (soundsVolume.ContainsKey(audio.GetInstanceID()))
                audio.volume = soundsVolume[audio.GetInstanceID()] * soundsSlider.value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit() {
        SceneManager.LoadScene("Intro");
    }

    public void ToggleOptionPanel() {
        optionPanel.SetActive(!optionPanel.activeSelf);

        if (optionPanel.activeSelf) {
            Time.timeScale = 0.0f;
        }
        else {
            Time.timeScale = 1.0f;
        }
    }
}
