using UnityEngine;

public class ControladorPausa : MonoBehaviour
{
    [Header("MenuPausaUI")]
    public GameObject menuPausaUI;

   
    public static bool elJuegoEstaPausado = false;

    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (elJuegoEstaPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false); 
        Time.timeScale = 1f;         
        elJuegoEstaPausado = false;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true); 
        Time.timeScale = 0f;          
        elJuegoEstaPausado = true;
    }
}