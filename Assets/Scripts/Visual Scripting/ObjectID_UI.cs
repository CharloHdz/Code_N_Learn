using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  // Añadir la referencia correcta para Image

public class ObjectID_UI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Datos del objeto")]
    public int ID;
    public bool instruccionCompletada;
    private RectTransform rectTransform;
    private Canvas canvas;
    [SerializeField] private Image image;
    private Lienzo_UI lienzoUI;  // Referencia al lienzo para verificar la "entrada" del objeto
    [SerializeField] private List<Sprite> BlockImage;
    [SerializeField] private List<Sprite> MiniBlockImage;
    [SerializeField] private RectTransform BlockRectTransform;
    private Vector2 originalPosition;
    private DeleteArea_UI deleteArea;
    [SerializeField] private GameObject blockCopy;

    [Header("Extras")]
    [SerializeField] private bool firstMove = false;

    [SerializeField] private TextMeshProUGUI TypeText;

    // Para detectar cambios en tipoBloque
    [SerializeField]private TipoBloque _tipoBloque;

    public TipoBloque tipoBloque
    {
        get { return _tipoBloque; }
        set
        {
            if (_tipoBloque != value)
            {
                _tipoBloque = value;
                TypeText.text = _tipoBloque.ToString();
                UpdateBlockSprite();
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
        TypeText = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
        TypeText.text = tipoBloque.ToString();  // Inicialización de texto según el tipo de bloque
        BlockRectTransform = GetComponent<RectTransform>();
        UpdateBlockSprite();
    }

    void Update()
    {
        UpdateObjectInLienzo();
    }

    private void UpdateObjectInLienzo()
    {
        // Si el objeto está dentro del panel, agregarlo a la lista, si no, removerlo
        if (lienzoUI.IsObjectInsidePanel(gameObject))
        {
            if (!lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Add(gameObject);
            }

            //Cambiar el Sprite del bloque a mini 
            UpdateMiniBlockSprite();
        }
        else
        {
            if (lienzoUI.ObjectIDList.Contains(gameObject))
            {
                lienzoUI.ObjectIDList.Remove(gameObject);
            }
        }
    }

    private void UpdateMiniBlockSprite(){
        TypeText.gameObject.SetActive(false);
        switch (_tipoBloque){
            case TipoBloque.Avanzar:
                image.sprite = MiniBlockImage[0];
            break;
            case TipoBloque.AvanzarNum:
                image.sprite = MiniBlockImage[0];
            break;
            case TipoBloque.Saltar:
                image.sprite = MiniBlockImage[1];
            break;
            case TipoBloque.Disparar:
                image.sprite = MiniBlockImage[2];
            break;
        }
        BlockRectTransform.sizeDelta = new Vector2(60, 60);
    }

    private void UpdateBlockSprite()
    {
        switch (_tipoBloque)
        {
            case TipoBloque.Avanzar:
                image.sprite = BlockImage[0];
                break;
            case TipoBloque.AvanzarNum:
                image.sprite = BlockImage[0];
                break;
            case TipoBloque.Saltar:
                image.sprite = BlockImage[1];
                break;
            case TipoBloque.Disparar:
                image.sprite = BlockImage[2];
                break;
            case TipoBloque.Agacharse:
                image.sprite = BlockImage[3];
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (deleteArea.IsObjectInsidePanel(gameObject) && firstMove == false)
        {
            blockCopy = Instantiate(gameObject, transform.parent);
            blockCopy.transform.SetParent(canvas.transform); // Añadirlo al canvas principal
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
                Player.Instance.PlayerRB.AddForce(transform.up * 300);
                Player.Instance.AnimJump();
                break;
            case TipoBloque.Agacharse:
                Debug.Log("Agachar");
                break;
            case TipoBloque.Avanzar:
                Rigidbody rb = Player.Instance.PlayerRB;
                if (rb != null)
                {
                    rb.AddForce(Vector2.right * 300);
                    Debug.Log("Avanzar");
                }
                Player.Instance.AnimRun();
                break;
            case TipoBloque.AvanzarNum:
                Debug.Log("AvanzarNum");
                break;
            case TipoBloque.Disparar:
                Debug.Log("Disparar");
                Player.Instance.Disparar();
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


