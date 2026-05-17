using UnityEngine;

public class BotonAccion : MonoBehaviour
{
  
    public enum TipoAccion { Tirar, Pasar }

    [Header("¿Qué hace este botón?")]
    public TipoAccion tipoDeBoton;

    [Header("Conexion al Manejo de rondas")]
    public Rondas scriptRondas;

    void OnMouseDown()
    {
 
        if (GameManager.Instance.dadoSeleccionado == null)
        {
            Debug.Log("¡Debes seleccionar un dado primero!");
            return; 
        }

        if (tipoDeBoton == TipoAccion.Tirar)
        {         
            int resultadoTirada = GameManager.Instance.dadoSeleccionado.TirarDado();

            Debug.Log("¡Has tirado el dado y has sacado un " + resultadoTirada + "!");

            GameManager.Instance.AñadirPuntuacion(resultadoTirada);

            GameManager.Instance.dadoSeleccionado.Deseleccionar();
        }

        else if (tipoDeBoton == TipoAccion.Pasar)
        {
            Debug.Log("Has pasado el turno. (Aquí irá la lógica del enemigo más adelante)");

            GameManager.Instance.dadoSeleccionado.Deseleccionar();

            if (scriptRondas != null)
            {
                scriptRondas.EvaluarPuntajeYPasarRonda();
            }


        }
    }
}