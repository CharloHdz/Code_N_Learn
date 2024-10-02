using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Apartados_Pestañas : MonoBehaviour, IPointerEnterHandler
{
    public TipoApartado tipoApartado;
    public List<GameObject> Apartados;
    // Start is called before the first frame update
    void Start()
    {
        CloseApartados();
        Apartados[2].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Mouse entered" + tipoApartado);

        switch(tipoApartado){
            case TipoApartado.Media:
                CloseApartados();
                Apartados[0].SetActive(true);
                break;
            case TipoApartado.Math:
                CloseApartados();
                Apartados[1].SetActive(true);
                break;
            case TipoApartado.Input:
                CloseApartados();
                Apartados[2].SetActive(true);
                break;
            case TipoApartado.Function:
                CloseApartados();
                Apartados[3].SetActive(true);
                break;
            case TipoApartado.UI:
                CloseApartados();
                Apartados[4].SetActive(true);
                break;
        }
    }

    public void CloseApartados(){
        foreach (var item in Apartados)
        {
            item.SetActive(false);
        }
    }
    
}

public enum TipoApartado
{
    Media,
    Math,
    Input,
    Function,
    UI
}
