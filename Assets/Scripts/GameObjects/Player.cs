using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody PlayerRB;
    [SerializeField] private GameObject Proyectil;
    [SerializeField] private Transform ProyectilSpawn;
    public static Player Instance { get; private set; }

    public Transform SpawnPoint;

    public Animator animator;

    [Header("Animaciones de movimiento")]
    public float posX;
    
    private void Awake()
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody>();
        SpawnPoint = GameObject.Find("SpawnPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Animaciones del jugador
    public void AnimRun(){
        animator.SetTrigger("Running");
    }

    public void AnimJump(){
        animator.SetTrigger("Jump");
    }

    public void AnimIdle(){
        animator.SetTrigger("Idle");
    }

    //Accion del jugador

    public void Disparar(){
        Instantiate(Proyectil, ProyectilSpawn.transform.position, Quaternion.identity);
    }
}
