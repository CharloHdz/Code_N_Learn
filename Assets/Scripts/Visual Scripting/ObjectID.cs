using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectID : MonoBehaviour
{
    public ObjectID Instance;
    public int ID;
    public Variable variable;
    public int dist;
    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ID = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Movimiento del bloque

    Vector2 difference = Vector2.zero;

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;

            // Si no tiene padre, simplemente actualizamos la posición en el espacio mundial
            transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    public void Instruction(){
        switch(variable){
            case Variable.Saltar:
                Player.Instance.PlayerRB.AddForce(transform.up * 100);
            break;
            case Variable.Agacharse:
                print("Agachar");
            break;
            case Variable.Avanzar:
                Rigidbody rb = Player.Instance.PlayerRB;
                if (rb != null)
                {
                    rb.AddForce(Vector2.right * 200);
                    print("Avanzar");
                }
                print("Avanzar");
            break;
            case Variable.Disparar:
                print("Disparar");
            break;
        }
    }
}

public enum Variable{
    Avanzar,
    Saltar,
    Disparar,
    Agacharse
}
