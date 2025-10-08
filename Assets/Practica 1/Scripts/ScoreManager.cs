using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    //texto o letrero UI
    public Text textoPuntaje;

    //variables internas
    private int puntajeActual = 0;

    [SerializeField]
    private Pin[] pinos;
    // Start is called before the first frame update
    void Start()
    {
        textoPuntaje.text = "soy billonario crack";
        //vuscador de objetos tipo pin
        pinos = FindObjectsOfType<Pin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //calculo de puntaje de los pinos
    public void CalcularPuntaje()
    {
        int puntaje = 0;
        //reviar si cada pino si esta caido
        foreach (Pin pin in pinos)
        {
            if (pin.EstaCaido ())
            {
                puntaje++;
            }
        }
        puntajeActual = puntaje;
        //actualizar texto del puntaje
        if (textoPuntaje != null) textoPuntaje.text = "Puntos: " + puntajeActual;
    }
}
