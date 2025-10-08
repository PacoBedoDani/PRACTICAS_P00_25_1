using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private float umbralCaida = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool EstaCaido()
    {
        float angulo = Vector3.Angle(transform.up, Vector3.up);

        return angulo > umbralCaida;
    }
}
