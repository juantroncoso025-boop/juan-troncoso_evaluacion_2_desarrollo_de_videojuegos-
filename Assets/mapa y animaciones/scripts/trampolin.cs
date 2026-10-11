using UnityEngine;

public class trampolin : MonoBehaviour
{
    public Animator animator;
    public float fuerzaSalto = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            animator.Play("trampolin_jump");
        }
    }



}
