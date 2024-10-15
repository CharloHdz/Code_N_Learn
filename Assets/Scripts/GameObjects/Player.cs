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
    public Vector3 InitPos;

    [SerializeField] Animator animator;
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
    }

    // Update is called once per frame
    void Update()
    {
        //Animar mocimiento del jugador
        if(Input.GetAxis("Horizontal") != 0){
            animator.SetTrigger("Running");
        }else{
            animator.SetTrigger("Idle");
        }

        if(Input.GetAxis("Vertical") != 0){
            animator.SetTrigger("Jump");
        }
    }

    //Animaciones del jugador
    public void AnimRun(){
        animator.SetTrigger("Running");
    }

    public void AnimIdle(){
        animator.SetTrigger("Idle");
    }

    public void AnimJump(){
        animator.SetTrigger("Jump");
    }

    //Acciones del jugador

    public void Avanzar(){
        
    }

    public void saltar(){

    }

    public void Disparar(){
        Instantiate(Proyectil, ProyectilSpawn.transform.position, Quaternion.identity);
    }
}
