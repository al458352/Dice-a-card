using UnityEngine;
using UnityEngine.SceneManagement;

public class Rondas : MonoBehaviour
{
    [Header("Configuración de Escenas")]
    public string escenaBatallas = "Batallas"; 

    public string escenaVictoria = "Victoria";

    [Header("Gestión de Rondas y Enemigos")]
    public CartaEnemigo[] listaDeEnemigos;
    public int rondaActual = 0;

    [Header("Display de Rondas (Sprites dibujados)")]
    public SpriteRenderer displayRonda;
    public Sprite[] spritesDeRondas;

    private CartaEnemigo EnemigoActual => listaDeEnemigos[rondaActual];

    private void Start()
    {
        ActualizarMesa();
    }

    public void EvaluarPuntajeYPasarRonda()
    {
        if (listaDeEnemigos == null || listaDeEnemigos.Length == 0) return;

        int minPermitido = EnemigoActual.puntuacionMinima;
        int maxPermitido = EnemigoActual.puntuacionMaxima;
        int puntosJugador = GameManager.Instance.puntuacionTotal;


        if (puntosJugador < minPermitido || puntosJugador > maxPermitido)
        {
            Debug.Log("¡Puntuación fuera de rango! Entrando en combate...");
            SceneManager.LoadScene(escenaBatallas);
        }
        else
        {
            Debug.Log("¡Ronda superada pacíficamente!");
            AvanzarRonda();
        }
    }

    private void AvanzarRonda()
    {
        rondaActual++;

        if (rondaActual >= listaDeEnemigos.Length)
        {
            SceneManager.LoadScene(escenaVictoria);
        }
        else
        {
            GameManager.Instance.puntuacionTotal = 0;

            ActualizarMesa();
        }
    }

    private void ActualizarMesa()
    {

        for (int i = 0; i < listaDeEnemigos.Length; i++)
        {
            listaDeEnemigos[i].gameObject.SetActive(false);
        }
        listaDeEnemigos[rondaActual].gameObject.SetActive(true);

        if (displayRonda != null && spritesDeRondas.Length > rondaActual)
        {
            displayRonda.sprite = spritesDeRondas[rondaActual];
        }
    }
}