using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCS : MonoBehaviour
{
    //TODO LIST:
    //Ter contato com a plataforma, e poder pular quando estiver EM PÉ em cima dela
    //Sincronizar a velocidade do player coma  velocidade das plataforams
    //Adicionar colisão (corretamente)
    //Mover para os lados
    [SerializeField] float horizontalVelocity = 3; 
    [SerializeField] float gravity; 
    [SerializeField] float jump = 10; 
    //float velocity = -3; 
    bool canJump = false; 

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Rigidbody>().gravity = gravity; 
        GameManagerCS.setPlayer(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //jump = 5 - PlataformaCS.velocity; 
        float x = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(horizontalVelocity * x, GetComponent<Rigidbody>().velocity.y ,0f);
        
        if(canJump)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jump, 0f);  
        }
         
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.contacts[0].normal.y == 1 && this.GetComponent<Rigidbody>().velocity.y <= 0)
            canJump = true; 
    }

    void OnCollisionExit(Collision other)
    {
        canJump = false; 
    }
}