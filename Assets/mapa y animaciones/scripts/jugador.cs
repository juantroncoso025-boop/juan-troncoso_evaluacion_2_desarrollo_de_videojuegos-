using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class jugador : MonoBehaviour
{
    Rigidbody2D cuerpo;
    public float velocidad = 5.0f;
    public int salto = 20;
    public puntos puntos;
    public AudioSource controlAudio;
    public AudioClip sonidoSalto;
    public AudioClip sonidoDaño;

    // Variables de vida y corazones
    public int vida = 3;
    public GameObject[] corazones;
    public int saltosMaximos = 2; // Número máximo de saltos (doble salto)
    private int saltosRestantes;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        controlAudio = GetComponent<AudioSource>();
        saltosRestantes = saltosMaximos;
    
    }

    void Update()
    {
        float movH = Input.GetAxis("Horizontal");
        cuerpo.linearVelocity = new Vector2(movH * velocidad, cuerpo.linearVelocity.y);

        if (Input.GetKeyDown("space"))
        {
            cuerpo.AddForce(Vector2.up * salto * 1.7f);

            if (Input.GetKeyDown("space") && saltosRestantes > 0)
            {
                // Opcional: resetea la velocidad vertical para que el segundo salto impulse bien
                cuerpo.linearVelocity = new Vector2(cuerpo.linearVelocity.x, 0f);

                cuerpo.AddForce(Vector2.up * salto * 1.7f);

                // Reproduce el sonido de salto
                if (controlAudio != null && sonidoSalto != null)
                {
                    controlAudio.PlayOneShot(sonidoSalto);
                }

                saltosRestantes--; // Resta un salto disponible
            }
        }

        if (movH != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movH), 1, 1);
        }
        
        // Si el jugador cae por debajo de la línea (Y = -46)
        if (transform.position.y < -46f)
        {
            RecibirDaño(1); // Le quita 1 de vida (o puedes reiniciar directo)
            transform.position = new Vector2(0f, 0f); // Coordenadas (X, Y) de reaparición inicial
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo") || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            saltosRestantes = saltosMaximos;
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector2(2, 0);
        }

        if (other.gameObject.CompareTag("Fruta"))
        {
            // Si tienes menos de 3 vidas, al tocar la fruta recuperas 1 de vida
            if (vida < 3)
            {
                vida += 1;
                ActualizarCorazones(); // Actualiza los corazones en pantalla
            }

            Debug.Log("Vidas actuales: " + vida);
            Destroy(other.gameObject); // Destruye la fruta al tocarla
        }
    }

    // Función para recibir daño desde el enemigo
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Vidas restantes: " + vida);
        if (controlAudio != null && sonidoDaño != null)
        {
            controlAudio.PlayOneShot(sonidoDaño);
        }

        ActualizarCorazones();

        if (vida <= 0)
        {
            transform.position = new Vector2(2, 0); // Reubica al jugador
            vida = 3; // Restablece las vidas
            ActualizarCorazones();
        }
    }

    // Función para mostrar u ocultar los corazones en pantalla
    void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < vida)
            {
                corazones[i].SetActive(true); // Muestra el corazón
            }
            else
            {
                corazones[i].SetActive(false); // Oculta el corazón si perdió esa vida
            }
        }
    }
}