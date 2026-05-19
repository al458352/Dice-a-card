using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonStart : MonoBehaviour
{
    public string nombreEscenaJuego = "ScenajuegoCartas";

    private void OnMouseDown()
    {
        Debug.Log("¡Botón Start pulsado! Cargando el juego...");
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}