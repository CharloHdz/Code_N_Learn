using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lienzo : MonoBehaviour
{
    public List<GameObject> ObjectID;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Sort();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlayGame());
        }
    }

    public void Sort()
    {
        // Ordenar los objetos por su posición en el eje Y de mayor a menor (del objeto más alto al más bajo)
        ObjectID = ObjectID.OrderByDescending(obj => obj.transform.position.y).ToList();
    }

    public IEnumerator PlayGame()
    {
        for (int i = 0; i < ObjectID.Count; i++)
        {
            // Llamar a la instrucción de cada objeto
            ObjectID[i].GetComponent<ObjectID>().Instruction();

            // Esperar 1 segundo antes de pasar al siguiente
            yield return new WaitForSeconds(0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ObjectID")
        {
            ObjectID.Add(other.gameObject);
            Sort(); // Volvemos a ordenar al agregar

            // Iniciar una corutina para mostrar el ID de cada objeto con un retraso
            StartCoroutine(PrintObjectIDs());
        }
    }

    IEnumerator PrintObjectIDs()
    {
        for (int i = 0; i < ObjectID.Count; i++)
        {
            // Imprimir el ID del objeto
            print(ObjectID[i].GetComponent<ObjectID>().ID);

            // Esperar 1 segundo antes de imprimir el siguiente
            yield return new WaitForSeconds(1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "ObjectID")
        {
            ObjectID.Remove(other.gameObject);
        }
    }
}


