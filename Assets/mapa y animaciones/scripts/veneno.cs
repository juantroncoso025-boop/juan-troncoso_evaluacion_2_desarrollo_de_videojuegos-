using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class veneno : MonoBehaviour
{
    public int vidas =3;

    public puntos puntos;

    // Start is called before the first frame update
    void Start()
    {
        puntos = GameObject.Find("Vidas").GetComponent<puntos>();
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            puntos.ptos = puntos.ptos -1;
            Debug.Log("Vidas restantes : "+puntos.ptos);
            if(puntos.ptos <= 0){
                Destroy(other.gameObject);
            }
            
        }
    }
}
