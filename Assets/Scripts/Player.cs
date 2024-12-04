using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [Header("Componentes")]
    public Rigidbody PlayerRB;
    public Animator animator;

    [Header("Proyectil")]
    [SerializeField] private GameObject Proyectil;
    [SerializeField] private Transform ProyectilSpawn;

    [Header("Puntos de Referencia")]
    public Transform SpawnPoint;

    [Header("Estado del Jugador")]
    public float posX; // Posición X para movimiento
    public string estado;

    private void Awake()
    {
        // Configuración Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
    }

    private void Update()
    {
        // Ejecutar acciones basadas en el estado actual
        switch (estado)
        {
            case "Avanzar":
                Mover(5f, "Run");
                break;
            case "Saltar":
                Mover(5f, "Jump");
                break;
            case "Idle":
                SetAnimacion("Idle");
                break;
            case "Disparar":
                SetAnimacion("Idle");
                break;
        }
    }

    private void Mover(float velocidad, string animacion)
    {
        posX += velocidad * Time.deltaTime;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        SetAnimacion(animacion);
    }

    private void SetAnimacion(string trigger)
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger);
        }
    }

    public void Disparar()
    {
        if (Proyectil != null && ProyectilSpawn != null)
        {
            Instantiate(Proyectil, ProyectilSpawn.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Error"))
        {
            StopAllCoroutines();
            estado = "Idle";
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Lava")){
            StopAllCoroutines();
            transform.position = SpawnPoint.position;
            print("Lava");
        }
        
    }

    public void Parar()
    {
        StopAllCoroutines();
        //Limpiar corutinas
        
        estado = "Idle";
    }
}
