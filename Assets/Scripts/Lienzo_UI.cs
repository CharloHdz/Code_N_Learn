using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class Lienzo_UI : MonoBehaviour
{
    public List<GameObject> ObjectIDList;  // Lista de objetos UI
    public RectTransform panelRectTransform;  // Referencia al área del lienzo (Panel)
    public Canvas canvas;  // Referencia al Canvas, necesario para calcular las posiciones en pantalla
    public Player player;

    [SerializeField] private GameObject PlayBtn;
    [SerializeField] private List<Sprite> PlayBtnState;
    private Image PlayBtnImage;

    //Singleton
    public static Lienzo_UI Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ObjectIDList = new List<GameObject>();  // Inicializamos la lista de objetos
        PlayBtnImage = PlayBtn.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        //Revisar si el objeto está dentro del panel
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            if (!IsObjectInsidePanel(ObjectIDList[i]))
            {
                ObjectIDList.RemoveAt(i);
            }
        }

        //Ordena la lista de objetos por su altura
        Sort();

    }
    // Método para correr el script que contiene el lienzo 

    public void ResetBlock(){
        //player.transform.position = player.InitPos;
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
        }
    }

    public void Play()
    {
        StartCoroutine(PlayGame());
    }

    public string EstadoJuego;
    public IEnumerator PlayGame()
    {
        EstadoJuego = "Leyendo";
        PlayBtnImage.sprite = PlayBtnState[1];
        player.transform.position = Player.Instance.SpawnPoint.position;
        Player.Instance.posX = player.transform.position.x;
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            // Llamar a la instrucción de cada objeto si está dentro del panel
            if (IsObjectInsidePanel(ObjectIDList[i]))
            {
                ObjectIDList[i].GetComponent<ObjectID_UI>().Instruction();
                ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
            }

            // Esperar 1 segundo antes de pasar al siguiente
            yield return new WaitForSeconds(1f);

            //Ejecutar accion cuando se hayan completado todas las instrucciones
            if(i == ObjectIDList.Count - 1){
                Player.Instance.estado = "Idle";
                EstadoJuego = "Lectura Completada";
                PlayBtnImage.sprite = PlayBtnState[0];
            }
        }
    }

    void Sort(){
        ObjectIDList = ObjectIDList.OrderByDescending(obj => obj.transform.position.x * -1).ToList();
    }

    // Método para verificar si el objeto está dentro del área del panel
    public bool IsObjectInsidePanel(GameObject obj)
    {
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        if (objRectTransform == null) return false;

        // Verificar si el punto está dentro del RectTransform del panel
        return RectTransformUtility.RectangleContainsScreenPoint(panelRectTransform, objRectTransform.position, canvas.worldCamera);
    }

}


