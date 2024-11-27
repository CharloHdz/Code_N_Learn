using TMPro;
using UnityEngine;

public class Translator : MonoBehaviour
{
    [Header("Español - ES")]
    [TextArea(7,5)]
    public string ES_Text;
    [Header("Inglés - EN")]
    [TextArea(7,5)]
    public string EN_Text;

    private TextMeshProUGUI Text;

    void Start() {
        Text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        switch (GameManager.instance.Idioma)
        {
            case "Es_Español":
                GameManager.instance.Idioma = "Es_Español";
                Text.text = ES_Text;
                break;
            case "En_English":
                GameManager.instance.Idioma = "En_English";
                Text.text = EN_Text;
                break;
        }
    }
    
}
