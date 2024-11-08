using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int score = 0;
    public int lives = 3;
    public int kunais = 20;

    public int posiones = 0;
    public int llaves = 0;

    public bool comioHongo = true;


    public bool llaveCalabozo = false;

    public bool tieneEspada = true;
    public bool tieneDobleSalto = false;
    public bool tieneDash = false;
    public bool tieneDobleDanio = false;
    public bool tieneDobleVida = false;

}
