using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour 
{    
    [Header("Scenes")]
    [SerializeField] private string gameScene;
    [SerializeField] private string optionsScene;
    
    [Header("Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Buttons")]
    [SerializeField] private Button gameStartButton;
    [SerializeField] private Button gameReturnButton;

    
    public void Start()
    {
        if (SoundManager.instance == null)
        {
            Debug.Log("You do not have a Sound Manager");
            return;
        }
        
        Time.timeScale = 0;
        GetMusicVolume();
        GetSfxVolume();
        WhichGameButton();
    }

    public void SetMusicVolume(float value)
    {
        SoundManager.instance.MusicVolume = value;
        musicVolumeSlider.value = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void GetMusicVolume()
    {        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume"); 
        }
        else
        {
            SetMusicVolume(0.5f);
        }
    }
    
    public void SetSxfVolume(float value)
    {
        SoundManager.instance.SfxVolume = value;
        sfxVolumeSlider.value = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void GetSfxVolume()
    {        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume"); 
        }
        else
        {
            SetSxfVolume(0.5f);
        }
    }

    private void WhichGameButton ()
    {
        if (SceneManager.GetSceneByName(gameScene).IsValid())
        {
            gameReturnButton.gameObject.SetActive(true);
            gameStartButton.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            gameReturnButton.gameObject.SetActive(false);
            gameStartButton.gameObject.SetActive(true);
        }
    }

    public void LoadGameScene ()
    {        
        if (!SceneManager.GetSceneByName(gameScene).IsValid())
            SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void QuitGame ()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void UnloadSceneAdd ()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync(optionsScene);
    }
}