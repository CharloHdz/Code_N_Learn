using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contenedor_Pestañas : MonoBehaviour
{
    public List<GameObject> pestañas;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Dependiendo de la cantidad de pestañas, se ajusta el tamaño del grid
        if (pestañas.Count > 0)
        {
            gridLayoutGroup.constraintCount = pestañas.Count;
        }
    }
}
