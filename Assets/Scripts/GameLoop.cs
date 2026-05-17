using UnityEngine;

public class GameLoop : MonoBehaviour
{
    private void Start()
    {
        Invoke("VolverARondas", 30f);
    }
    public void VolverARondas()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("ScenaJuegoCartas");
    }
}
