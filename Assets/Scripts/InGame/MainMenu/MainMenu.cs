using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Nombre de la música del menú principal y la música del juego
    public string mainMenuMusic = "MainMenuMusic";
    public string gameMusic = "GameMusic";
    public string sFxStart = "StartButton";
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Start()
    {
        // Reproducir la música del menú principal al inicio
        AudioManager.Instance.PlayMusic(mainMenuMusic);
        
        // Conectar botones si están asignados
        if (playButton != null)
            playButton.onClick.AddListener(PlayGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    public void PlayGame()
    {
        // Detener la música del menú principal
        AudioManager.Instance.StopMusic();

        // Reproducir el efecto de sonido y la música del juego
        AudioManager.Instance.PlaySFX(sFxStart);
        // AudioManager.Instance.PlayMusic(gameMusic);

        // Cambiar la escena a Game
        SceneManager.LoadSceneAsync("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
