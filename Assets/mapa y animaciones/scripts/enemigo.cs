using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public GameObject Player;
    public float velocidad =1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       if (Player == null)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position,Player.transform.position,velocidad*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Buscamos el script del jugador y le restamos vida
            jugador playerScript = other.gameObject.GetComponent<jugador>();
            if (playerScript != null)
            {
                playerScript.RecibirDaño(1); // Le quita 1 de vida
            }
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
    

