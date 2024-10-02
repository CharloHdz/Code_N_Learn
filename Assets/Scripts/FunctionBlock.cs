using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBlock : MonoBehaviour
{
    public RectTransform centerPart;  // Referencia a la parte central que se estirará
    public RectTransform blockContainer;  // Contenedor de los bloques internos
    public float minWidth = 100f;  // Ancho mínimo del bloque de función
    public float blockWidth = 50f;  // Ancho de cada bloque que se coloca dentro

    private List<GameObject> innerBlocks = new List<GameObject>();

    // Método para agregar un bloque dentro de la función
    public void AddBlock(GameObject block)
    {
        // Agregar el bloque a la lista y como hijo del contenedor
        innerBlocks.Add(block);
        block.transform.SetParent(blockContainer, false);

        // Ajustar el tamaño de la parte central
        AdjustSize();
    }

    // Método para remover un bloque dentro de la función
    public void RemoveBlock(GameObject block)
    {
        // Remover el bloque de la lista
        innerBlocks.Remove(block);
        Destroy(block);

        // Ajustar el tamaño de la parte central
        AdjustSize();
    }

    void Update()
    {
        AdjustSize();
    }

    // Ajustar el tamaño del bloque de función basado en la cantidad de bloques internos
    void AdjustSize()
    {
        // Calcular el nuevo ancho basado en el número de bloques
        float newWidth = minWidth + (innerBlocks.Count * blockWidth);

        // Aplicar el nuevo tamaño a la parte central del bloque de función
        centerPart.sizeDelta = new Vector2(newWidth, centerPart.sizeDelta.y);
    }
}
