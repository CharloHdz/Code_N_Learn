using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ObjectID_UI : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public int ID;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Lienzo_UI lienzoUI;  // Referencia al lienzo para verificar la "entrada" del objeto

    public Variable1 var;
    private Vector2 originalPosition;
    DeleteArea_UI deleteArea;

    [SerializeField] private TextMeshProUGUI TypeText;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        lienzoUI = FindObjectOfType<Lienzo_UI>();  // Obtenemos referencia al Lienzo en la escena
        originalPosition = rectTransform.anchoredPosition;  // Guardamos la posición original
        deleteArea = FindObjectOfType<DeleteArea_UI>();
        TypeText = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        // Si el objeto está dentro del panel, agregarlo a la lista, si no, removerlo
        if (lienzoUI.IsObjectInsidePanel(gameObject))
        {
            if (!lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Add(gameObject);
            }
        }
        else
        {
            if (lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Remove(gameObject);
            }
        }

        TypeText.text = var.ToString();
    }

    // Evento cuando se empieza a arrastrar el objeto
    public void OnPointerDown(PointerEventData eventData)
    {
        //originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Movemos el objeto según la posición del puntero
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out movePos);
        rectTransform.anchoredPosition = movePos;

        if(deleteArea.IsObjectInsidePanel(gameObject)){
            deleteArea.gameObject.SetActive(true);
        } else {
            deleteArea.gameObject.SetActive(false);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Verificar si el objeto está dentro del panel de eliminación
        if (deleteArea.IsObjectInsidePanel(gameObject))
        {
            Destroy(gameObject);
            deleteArea.ClosePanel();
        }
    }

    // Método para ejecutar una instrucción
    public void Instruction()
    {

        switch(var){
            case Variable1.Saltar:
                Player.Instance.PlayerRB.AddForce(transform.up * 300);
            break;
            case Variable1.Agacharse:
                print("Agachar");
            break;
            case Variable1.Avanzar:
                Rigidbody rb = Player.Instance.PlayerRB;
                if (rb != null)
                {
                    rb.AddForce(Vector2.right * 200);
                    print("Avanzar");
                }
                print("Avanzar");
            break;
            case Variable1.Disparar:
                print("Disparar");
            break;
        }
    }
}

public enum Variable1{
    Avanzar,
    Saltar,
    Disparar,
    Agacharse
}


