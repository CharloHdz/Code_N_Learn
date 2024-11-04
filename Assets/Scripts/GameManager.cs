using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Lienzo_UI lienzoUI;

    [Header("Cameras")]
    public Camera CenterCamera;
    public Camera PlayerCamera;
    // Start is called before the first frame update
    void Start()
    {
        lienzoUI = FindObjectOfType<Lienzo_UI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            StopAllCoroutines();
            print("L pressed");
        }
    }

    public void ChangeCamera()
    {
        if (CenterCamera.enabled)
        {
            CenterCamera.enabled = false;
            PlayerCamera.enabled = true;
        }
        else
        {
            CenterCamera.enabled = true;
            PlayerCamera.enabled = false;
        }
    }

    public void PlayGame()
    {
        StartCoroutine(lienzoUI.PlayGame());
    }
}
