using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCS : MonoBehaviour
{
    [SerializeField] float limiteVerticalSuperior;
    [SerializeField] float limiteVerticalInferior;
    [SerializeField] float limiteHorizontal; 
    [SerializeField] GameObject prefab; 
    float offset; 
    public static float velocity = -3;
    bool spawnedPlataform = false; 
    bool newPlataform; 
     
    void Start()
    {
        if(GameManagerCS.getPlayer() != null)
            offset = GameManagerCS.getPlayer().transform.localScale.y/2; 
        newPlataform = true; 
    }

    void spawnPlataform()
    {
        float value = 0;
        float offset = 3; 
        if(Random.value > 0.5)
            value = 3;
        else
            value -= 3;
        
        if(this.transform.position.x + value > limiteHorizontal || this.transform.position.x + value < -limiteHorizontal)
            value *= - 1; 


        Instantiate(prefab, new Vector3(this.transform.position.x + value, transform.position.y + offset, transform.position.z), Quaternion.identity, this.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, velocity * Time.deltaTime, 0);

        if(GameManagerCS.getPlayerPos() - offset < this.transform.position.y)
        {
            this.GetComponent<BoxCollider>().isTrigger = true; 
        }
        else
        {
            this.GetComponent<BoxCollider>().isTrigger = false; 
        }
        if(transform.position.y < limiteVerticalSuperior && !spawnedPlataform)
        {
            spawnPlataform();
            spawnedPlataform = true; 
        }
        
        if(transform.position.y < limiteVerticalInferior)
        {
            /* while(this.gameObject.transform.childCount > 0)
            {
                this.gameObject.transform.GetChild(0).SetParent(this.gameObject.transform.parent);
            } */

            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(newPlataform)
        {
            GameManagerCS.pontuation++;
            newPlataform = false;
        }
    }
}
