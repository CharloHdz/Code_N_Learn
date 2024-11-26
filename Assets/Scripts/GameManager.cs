using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Lienzo_UI lienzoUI;

    [Header("Cameras")]
    public Camera CenterCamera;
    public Camera PlayerCamera;

    [Header("UI")]
    public EstadosJuego estadoJuego;
    public GameObject MenuPanel;
    public GameObject GamePanel;
    public GameObject PausePanel;
    public GameObject ConfigPanel;

    [Header("Temas")]
    public UnityEngine.UI.Image TemasImg;
    public List<TemaSO> TemaSO; // Lista de ScriptableObjects tipo Tema
    public TextMeshProUGUI TemasTxt;
    public int Temasindex = 0;

    void Start()
    {
        lienzoUI = FindObjectOfType<Lienzo_UI>();
        CloseEv();
        MenuPanel.SetActive(true);
        UpdateTemaUI();
        estadoJuego = EstadosJuego.Menu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && estadoJuego == EstadosJuego.Juego || Input.GetKeyDown(KeyCode.Escape) && estadoJuego == EstadosJuego.Pausa)
        {
            PausarJuego();
        }
    }

    public void ChangeCamera()
    {
        CenterCamera.enabled = !CenterCamera.enabled;
        PlayerCamera.enabled = !PlayerCamera.enabled;
    }

    public void PlayGame()
    {
        StartCoroutine(lienzoUI.PlayGame());
    }

    // UI
    public void CloseEv()
    {
        MenuPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        ConfigPanel.SetActive(false);
    }

    public void TemaSig()
    {
        Temasindex++;
        if (Temasindex >= TemaSO.Count)
        {
            Temasindex = 0;
        }
        UpdateTemaUI();
    }

    public void TemaAnt()
    {
        Temasindex--;
        if (Temasindex < 0)
        {
            Temasindex = TemaSO.Count - 1;
        }
        UpdateTemaUI();
    }

    private void UpdateTemaUI()
    {
        // Actualiza UI y Skybox
        TemasImg.sprite = TemaSO[Temasindex].Sprite;
        RenderSettings.skybox = TemaSO[Temasindex].Material;
        DynamicGI.UpdateEnvironment(); // Actualiza iluminación
    }

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

    public void GotoMenu()
    {
        Time.timeScale = 1;
        estadoJuego = EstadosJuego.Menu;
    }

    public void GoToGame(){
        Time.timeScale = 1;
        estadoJuego = EstadosJuego.Juego;
    }

    public void GoToConfig(){
        Time.timeScale = 0;
        estadoJuego = EstadosJuego.Config;
    }

    public void ReturnToPause(){
        Time.timeScale = 0;
        estadoJuego = EstadosJuego.Pausa;
    }
}

public enum EstadosJuego{
    Menu,
    Juego,
    Pausa,
    Config,
    GameOver
}

