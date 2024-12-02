using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using NUnit.Framework;  // Añadir la referencia correcta para Image

public class ObjectID_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Datos del objeto")]
    public int ID;
    public bool IsBlockInsideCanvas;
    public bool instruccionCompletada;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Image image;
    private Lienzo_UI lienzoUI;  // Referencia al lienzo para verificar la "entrada" del objeto
    [SerializeField] private List<Sprite> TipeBlockImage;
    [SerializeField] private RectTransform BlockRectTransform;
    private Vector2 originalPosition;
    private DeleteArea_UI deleteArea;
    [SerializeField] private GameObject blockCopy;

    [Header("Extras")]
    [SerializeField] private bool firstMove = false;

    // Para detectar cambios en tipoBloque
    [SerializeField]private TipoBloque _tipoBloque;
    [SerializeField] private Transform parentTransform;

    public TipoBloque tipoBloque
    {
        get { return _tipoBloque; }
        set
        {
            if (_tipoBloque != value)
            {
                _tipoBloque = value;
            }
        }
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        lienzoUI = FindObjectOfType<Lienzo_UI>();  // Obtenemos referencia al Lienzo en la escena
        originalPosition = rectTransform.anchoredPosition;  // Guardamos la posición original
        deleteArea = FindObjectOfType<DeleteArea_UI>();
        image = GetComponent<Image>();
        BlockRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        UpdateObjectInLienzo();
        UpdateTipeBlockSprite();
    }

    private void UpdateObjectInLienzo()
    {
        // Si el objeto está dentro del panel, agregarlo a la lista, si no, removerlo
        if (lienzoUI.IsObjectInsidePanel(gameObject))
        {
            if (!lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Add(gameObject);
                IsBlockInsideCanvas = true;
            }
        }
        else
        {
            if (lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Remove(gameObject);
                IsBlockInsideCanvas = false;
            }
        }
    }

    private void UpdateTipeBlockSprite(){
        switch (_tipoBloque){
            case TipoBloque.Avanzar:
                image.sprite = TipeBlockImage[0];
            break;
            case TipoBloque.AvanzarNum:
                image.sprite = TipeBlockImage[0];
            break;
            case TipoBloque.Saltar:
                image.sprite = TipeBlockImage[1];
            break;
            case TipoBloque.Disparar:
                image.sprite = TipeBlockImage[2];
            break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (deleteArea.IsObjectInsidePanel(gameObject) && firstMove == false)
        {
            blockCopy = Instantiate(gameObject, parentTransform); // Crear una copia del objeto
            blockCopy.transform.SetParent(parentTransform.transform); // Añadirlo al canvas principal
            firstMove = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, eventData.pressEventCamera, out movePos))
        {
            rectTransform.anchoredPosition = movePos;
        }

        deleteArea.gameObject.SetActive(deleteArea.IsObjectInsidePanel(gameObject));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (deleteArea.IsObjectInsidePanel(gameObject))
        {
            Destroy(gameObject);
            deleteArea.ClosePanel();
        }
    }

    public void ResetInstruction()
    {
        instruccionCompletada = false;
    }


    public void Instruction()
    {
        switch (tipoBloque)
        {
            case TipoBloque.Saltar:
                Player.Instance.PlayerRB.AddForce(transform.up * 500);
                //Player.Instance.AnimJump();
                Player.Instance.estado = "Saltar";
                break;
            case TipoBloque.Agacharse:
                Debug.Log("Agachar");
                Player.Instance.estado = "Agachar";
                break;
            case TipoBloque.Avanzar:
                Player.Instance.estado = "Avanzar";
                //Player.Instance.AnimRun();
                break;
            case TipoBloque.AvanzarNum:
                Debug.Log("AvanzarNum");
                Player.Instance.estado = "Avanzar";
                break;
            case TipoBloque.Disparar:
                Debug.Log("Disparar");
                Player.Instance.Disparar();
                Player.Instance.estado = "Disparar";
                break;
        }
    }

    public IEnumerator PlayInstruction()
    {
        yield return new WaitForSeconds(3f);
    }

    public void InstruccionCompleta()
    {
        instruccionCompletada = true;
    }
}

public enum TipoBloque
{
    Avanzar,
    AvanzarNum,
    Saltar,
    Disparar,
    Agacharse
}


