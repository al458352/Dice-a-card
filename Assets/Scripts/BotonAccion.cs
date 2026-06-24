using UnityEngine;

public class BotonAccion : MonoBehaviour
{
    public enum TipoAccion { Tirar, Pasar }

    [Header("botón")]
    public TipoAccion tipoDeBoton;

    void OnMouseDown()
    {
        if (tipoDeBoton == TipoAccion.Tirar)
        {
            if (GameManager.Instance.dadoSeleccionado == null)
            {
                Debug.Log("selecciona un dado primero");
                return;
            }

            int resultadoTirada = GameManager.Instance.dadoSeleccionado.TirarDado();
            Debug.Log("¡Has tirado el dado y has sacado un " + resultadoTirada + "!");
            GameManager.Instance.AñadirPuntuacion(resultadoTirada);
        }
        else if (tipoDeBoton == TipoAccion.Pasar)
        {
            Debug.Log("Has pasado el turno.");

            if (GameManager.Instance.dadoSeleccionado != null)
            {
                GameManager.Instance.dadoSeleccionado.Deseleccionar();
            }

            if (Rondas.Instance != null)
            {
                Rondas.Instance.EvaluarPuntajeYPasarRonda();
            }
        }
    }
}