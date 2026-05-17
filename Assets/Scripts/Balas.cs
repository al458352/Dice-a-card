using UnityEngine;

// Esto le dice a Unity: "¡Oye, no me dejes poner este script si no hay Rigidbody y Collider!"
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BalaInerciaFisica : MonoBehaviour
{
    [HideInInspector] public Transform objetivo;
    [HideInInspector] public float velocidadBase;

    [Header("Configuración del Giro (Inercia)")]
    [Tooltip("A mayor número, más rápido gira hacia el jugador (más difícil)")]
    public float velocidadGiro = 200f; // Al usar físicas, a veces hay que subir un poco este valor

    private Rigidbody2D rb;

    private void Start()
    {
        // Guardamos el Rigidbody al nacer
        rb = GetComponent<Rigidbody2D>();
    }

    // IMPORTANTÍSIMO: Las físicas SIEMPRE se mueven aquí, no en Update()
    private void FixedUpdate()
    {
        // 1. Si el jugador muere o desaparece, la bala sigue recta
        if (objetivo == null)
        {
            rb.linearVelocity = transform.up * velocidadBase; // Nota: En Unity 6 se usa linearVelocity (antes era solo velocity)
            return;
        }

        // 2. Calculamos la dirección hacia el jugador
        Vector2 direccionAlObjetivo = (Vector2)objetivo.position - rb.position;
        direccionAlObjetivo.Normalize();

        // 3. Calculamos el ángulo matemático
        float anguloObjetivo = Mathf.Atan2(direccionAlObjetivo.y, direccionAlObjetivo.x) * Mathf.Rad2Deg - 90f;

        // 4. EL TRUCO DE LA INERCIA (VERSIÓN FÍSICA)
        // MoveTowardsAngle gira el Rigidbody de forma suave y constante
        float anguloSuavizado = Mathf.MoveTowardsAngle(rb.rotation, anguloObjetivo, velocidadGiro * Time.fixedDeltaTime);
        rb.MoveRotation(anguloSuavizado);

        // 5. Aplicamos el empuje (motor) hacia donde está mirando la bala
        rb.linearVelocity = transform.up * velocidadBase;
    }

    // Así detectas si le ha dado al jugador usando el BoxCollider2D
    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("¡PUM! Has golpeado al jugador.");

            // Aquí llamarías al script de vida del jugador
            // otro.GetComponent<VidaJugador>().RecibirDaño(1);

            // Destruimos la bala tras el impacto
            Destroy(gameObject);
        }
    }
}