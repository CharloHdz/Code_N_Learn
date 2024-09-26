using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Lienzo_UI lienzoUI;
    // Start is called before the first frame update
    void Start()
    {
        lienzoUI = FindObjectOfType<Lienzo_UI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        StartCoroutine(lienzoUI.PlayGame());
    }
}
