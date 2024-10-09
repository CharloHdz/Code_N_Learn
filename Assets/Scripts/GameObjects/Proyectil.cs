using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad;
    public float tiempoVida;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del proyectil
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        //Destrucción del proyectil
        tiempoVida -= Time.deltaTime;
        if(tiempoVida <= 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemigo o Muerte")){
            transform.position = Lienzo_UI.Instance.InitPos;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Enemigo o Muerte"){
            transform.position = Lienzo_UI.Instance.InitPos;
        }
    }
}
