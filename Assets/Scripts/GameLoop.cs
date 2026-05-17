using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    [Header("Cantidad de tiempo por ronda")]
    public float tiempoDeBatalla = 30f;

    private void Start()
    {
        Invoke("VolverARondas", tiempoDeBatalla);
    }

    public void VolverARondas()
    {
        SceneManager.LoadScene("ScenajuegoCartas");
    }
}