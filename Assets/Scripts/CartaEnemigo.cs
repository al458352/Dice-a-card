using UnityEngine;

[System.Serializable]
public class IntervaloDaño
{
    public int dañoMinimo;
    public int dañoMaximo;
}

    public class CartaEnemigo : MonoBehaviour
{
    [Header("Información del Enemigo")]
    public string nombreEnemigo = "Nuevo Enemigo"; 


    [Header("Condición de Victoria (Intervalo)")]
    [Tooltip("La puntuación mínima que necesita el jugador para pasar.")]
    public int puntuacionMinima = 0;

    [Tooltip("La puntuación máxima que el jugador NO debe superar.")]
    public int puntuacionMaxima = 0;

    [Header("Daño al jugador")]
    public IntervaloDaño[] DañoAlJugador;
}
