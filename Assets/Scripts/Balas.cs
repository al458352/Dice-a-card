using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BalaInerciaFisica : MonoBehaviour
{
    [HideInInspector] public Transform objetivo;
    [HideInInspector] public float velocidadBase;

    [Header("Configuración del Giro (Inercia)")]
    [Tooltip("A mayor número, más rápido gira hacia el jugador (más difícil)")]
    public float velocidadGiro = 200f; 

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        int rondaActual = Rondas.Instance.rondaActual;
        float duracionBala = FindObjectOfType<SpawnerBalas>().dificultadPorRonda[rondaActual].duracionDeBalaEnSegundos;
        Destroy(gameObject, duracionBala);
    }

    private void FixedUpdate()
    {
        if (objetivo == null)
        {
            rb.linearVelocity = transform.up * velocidadBase;
            return;
        }

        Vector2 direccionAlObjetivo = (Vector2)objetivo.position - rb.position;
        direccionAlObjetivo.Normalize();

        float anguloObjetivo = Mathf.Atan2(direccionAlObjetivo.y, direccionAlObjetivo.x) * Mathf.Rad2Deg - 90f;

        float anguloSuavizado = Mathf.MoveTowardsAngle(rb.rotation, anguloObjetivo, velocidadGiro * Time.fixedDeltaTime);
        rb.MoveRotation(anguloSuavizado);

        rb.linearVelocity = transform.up * velocidadBase;
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            Debug.Log("¡PUM! Has golpeado al jugador. Volviendo a las cartas...");

            int rondaActual = Rondas.Instance.rondaActual;
            CartaEnemigo enemigoActual = Rondas.Instance.listaDeEnemigos[rondaActual];

            if (enemigoActual.DañoAlJugador != null && enemigoActual.DañoAlJugador.Length > 0)
            {
                int dañoMin = enemigoActual.DañoAlJugador[0].dañoMinimo;
                int dañoMax = enemigoActual.DañoAlJugador[0].dañoMaximo;
                int dañoCalculado = Random.Range(dañoMin, dañoMax + 1);

                GameManager.Instance.RecibirDaño(dañoCalculado);
            }

            SceneManager.LoadScene("ScenajuegoCartas");

            Destroy(gameObject);
        }
    }
}