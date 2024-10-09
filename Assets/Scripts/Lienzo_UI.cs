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
    public Vector3 InitPos;

    //Singleton
    public static Lienzo_UI Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        ObjectIDList = new List<GameObject>();  // Inicializamos la lista de objetos
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
        player.transform.position = InitPos;
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
        }
    }
    public IEnumerator PlayGame()
    {
        player.transform.position = InitPos;
        for (int i = 0; i < ObjectIDList.Count; i++)
        {
            // Llamar a la instrucción de cada objeto si está dentro del panel
            if (IsObjectInsidePanel(ObjectIDList[i]))
            {
                ObjectIDList[i].GetComponent<ObjectID_UI>().Instruction();
                ObjectIDList[i].GetComponent<ObjectID_UI>().InstruccionCompleta();
            }

            // Esperar 1 segundo antes de pasar al siguiente
            yield return new WaitForSeconds(0.35f);
        }
    }

    public void LeerBloques(){
        //La distancia que recorra el jugador dependerá completamente de los bloques de movimiento que contenga el lienzo
        player.transform.position = InitPos;

    }

    void Sort(){
        ObjectIDList = ObjectIDList.OrderByDescending(obj => obj.transform.position.y).ToList();
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


