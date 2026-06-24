using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonStart : MonoBehaviour
{
    public string nombreEscenaJuego = "ScenajuegoCartas";

    private void OnMouseDown()
    {
        Debug.Log("Start pulsado");
        SceneManager.LoadScene(nombreEscenaJuego);
    }
}