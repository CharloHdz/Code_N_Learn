using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Referencias UI")]
    private Lienzo_UI lienzoUI;

    [Header("Cámaras")]
    public Camera CenterCamera;
    public Camera PlayerCamera;

    [Header("UI Panels")]
    public GameObject MenuPanel;
    public GameObject GamePanel;
    public GameObject TutorialPanel;
    public GameObject PausePanel;
    public GameObject ConfigPanel;

    [Header("Temas")]
    public Image TemasImg;
    public List<TemaSO> TemaSO; // Lista de ScriptableObjects tipo Tema
    public TextMeshProUGUI TemasTxt;
    public int TemasIndex;

    [Header("Variables Generales")]
    public bool FirstTime = true;
    public EstadosJuego estadoJuego;
    public string Idioma;
    public string Resolucion;

    [Header ("Elementos Tutorial")]
    public List<GameObject> TutorialElements;
    public GameObject DialogueGlobe;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        lienzoUI = FindObjectOfType<Lienzo_UI>();
        CloseEv(); // Cerrar todos los paneles inicialmente
        MenuPanel.SetActive(true);
        estadoJuego = EstadosJuego.Menu;

        GetPreferences();

        UpdateTemaUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (estadoJuego == EstadosJuego.Juego || estadoJuego == EstadosJuego.Pausa))
        {
            PausarJuego();
        }
        
        SavePreferences();
    }

    // Cambia entre cámaras
    public void ChangeCamera()
    {
        CenterCamera.enabled = !CenterCamera.enabled;
        PlayerCamera.enabled = !PlayerCamera.enabled;
    }

    // Comienza el juego con animaciones desde el lienzo
    public void PlayGame()
    {
        StartCoroutine(lienzoUI.PlayGame());
    }

    // Cierra todos los paneles de UI
    public void CloseEv()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        ConfigPanel.SetActive(false);
        TutorialPanel.SetActive(false);
    }

    // Avanza al siguiente tema
    public void TemaSig()
    {
        TemasIndex++;
        if (TemasIndex >= TemaSO.Count)
        {
            TemasIndex = 0;
        }
        UpdateTemaUI();
    }

    // Regresa al tema anterior
    public void TemaAnt()
    {
        TemasIndex--;
        if (TemasIndex < 0)
        {
            TemasIndex = TemaSO.Count - 1;
        }
        UpdateTemaUI();
    }

    // Actualiza la UI y el Skybox con el tema seleccionado
    private void UpdateTemaUI()
    {
        TemasImg.sprite = TemaSO[TemasIndex].Sprite;
        RenderSettings.skybox = TemaSO[TemasIndex].Material;
        DynamicGI.UpdateEnvironment(); // Actualiza iluminación global
    }

    // Pausa o reanuda el juego
    public void PausarJuego()
    {
        if (estadoJuego == EstadosJuego.Juego)
        {
            CloseEv();
            Time.timeScale = 0;
            PausePanel.SetActive(true);
            estadoJuego = EstadosJuego.Pausa;
        }
        else if (estadoJuego == EstadosJuego.Pausa)
        {
            CloseEv();
            Time.timeScale = 1;
            GamePanel.SetActive(true);
            estadoJuego = EstadosJuego.Juego;
        }
    }

    // Cambia al menú principal
    public void GotoMenu()
    {
        Time.timeScale = 1;
        CloseEv();
        MenuPanel.SetActive(true);
        estadoJuego = EstadosJuego.Menu;
        SceneManager.LoadScene("Game");
    }

    // Cambia al estado de juego
    public void GoToGame()
    {
        Time.timeScale = 1;
        CloseEv();
        GamePanel.SetActive(true);
        estadoJuego = EstadosJuego.Juego;
    }

    // Cambia al panel de configuración
    public void GoToConfig()
    {
        Time.timeScale = 0;
        CloseEv();
        ConfigPanel.SetActive(true);
        estadoJuego = EstadosJuego.Config;
    }

    // Regresa al menú de pausa
    public void ReturnToPause()
    {
        Time.timeScale = 0;
        CloseEv();
        PausePanel.SetActive(true);
        estadoJuego = EstadosJuego.Pausa;
    }

    // Comienza el juego mostrando un tutorial si es la primera vez
    public void StartGame()
    {
        if (FirstTime)
        {
            CloseEv();
            TutorialPanel.SetActive(true);
            GamePanel.SetActive(true);
            estadoJuego = EstadosJuego.Juego;
            FirstTime = false;
        }
        else
        {
            GoToGame();
        }
    }

    public void CambiarIdioma()
    {
        Idioma = PlayerPrefs.GetString("Idioma", Idioma);
        switch (Idioma)
        {
            case "Es_Español":
                Idioma = "En_English";
                break;
            case "En_English":
                Idioma = "Es_Español";
                break;
        }
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt("FirstTime", FirstTime ? 1 : 0);
        PlayerPrefs.SetInt("TemaIndex", TemasIndex);
        PlayerPrefs.SetString("Idioma", Idioma);
        PlayerPrefs.Save();
    }

    void GetPreferences(){
        FirstTime = PlayerPrefs.GetInt("FirstTime", FirstTime ? 1 : 0) == 1;
        TemasIndex = PlayerPrefs.GetInt("TemaIndex", 1);
        Idioma = PlayerPrefs.GetString("Idioma", "Es_Español");
    }

}

// Enum para representar los diferentes estados del juego
public enum EstadosJuego
{
    Menu,
    Juego,
    Pausa,
    Config,
    GameOver
}