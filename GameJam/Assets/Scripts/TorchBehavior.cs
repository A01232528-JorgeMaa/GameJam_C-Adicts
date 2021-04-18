using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehavior : MonoBehaviour
{
    public Light Spot;
    public float battery = 10f;
    public int blinkStart = 4;

    public float parpadeo = .07f;
    public int randomizer = 0;
    bool blinking;
    bool on;

    public void BatteryRefill()
    {
        battery += 3;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            if (randomizer == 0)
            {
                Spot.enabled = false;
            }
            else
            {
                Spot.enabled = true;
            }
            randomizer = Random.Range(0, 15);
            yield return new WaitForSeconds(parpadeo);
        }
    }

    private void Start()
    {
        if (battery > 0)
        {
            on = Spot.enabled = true;
            blinking = false;
        }
        else
        {
            on = blinking = Spot.enabled = false;
        }
    }

    void Update()
    {
        // Verificacion de bateria sin estado
        if(battery <= 0)
        {
            Spot.enabled = false;
            battery = 0;
            StopAllCoroutines();
        }

        // Verificacion de bateria con estado
        if(battery > 0 && on)
        {
            Spot.enabled = true;
        }

        // Parpadeo de la linterna
        if (battery <= blinkStart && battery > 0 && on)
        {
            StartCoroutine(Blink());
            blinking = true;
        }

        // Encendido y apagado de la linterna
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (on || blinking)
            {
                if (blinking)
                {
                    StopAllCoroutines();
                    blinking = false;
                }
                Spot.enabled = false;
                on = false;
            }
            else
            {
                Spot.enabled = true;
                on = true;
                Debug.Log("Linterna encendida con " + battery + " unidades de bateria");
            }
        }

        // Consumo de bateria
        if(battery > 0 && on)
        {
            battery -= Time.deltaTime / 4;
        }


        // Aumento de bateria
        if(Input.GetKeyDown("r"))
        {
            BatteryRefill();
        }

        //Debug.Log("Bateria: " + battery);
    }
}
