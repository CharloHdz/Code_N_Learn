using UnityEngine;

public class DeleteArea_UI : MonoBehaviour
{
    public RectTransform rectTransform;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsObjectInsidePanel(GameObject obj)
    {
        RectTransform objRectTransform = obj.GetComponent<RectTransform>();
        if (objRectTransform == null) return false;

        // Verificar si el punto está dentro del RectTransform del panel
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, objRectTransform.position, canvas.worldCamera);
    }

    public void ClosePanel(){
        gameObject.SetActive(false);
    }
}
