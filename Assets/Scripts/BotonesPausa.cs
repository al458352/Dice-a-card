using UnityEngine;

public class BotonesPausa : MonoBehaviour
{
    [Header("ControladorPausa")]
    public ControladorPausa controladorPausa;

  
    public void BotonContinuar()
    {
        if (controladorPausa != null)
        {
            controladorPausa.Reanudar();
        }
    }

   
    public void BotonSalir()
    {
        Debug.Log("Salir");

     
        Application.Quit();
    }
}