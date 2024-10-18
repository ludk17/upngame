using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManagerController : MonoBehaviour
{

    private GameData gData;
    private GameDataRepository gDataRepository;

    // private int score = 0;
    // private int lives = 3;

    private TMP_Text scoreText;
    private TMP_Text livesText;
    private TMP_Text kunaisText;

    // Start is called before the first frame update
    void Start()
    {
        gDataRepository = new GameDataRepository();
        gData = gDataRepository.LoadGame();

        scoreText = GameObject.Find("PuntajeText").GetComponent<TextMeshProUGUI>();
        livesText = GameObject.Find("VidasText").GetComponent<TextMeshProUGUI>();
        kunaisText = GameObject.Find("KunaisText").GetComponent<TextMeshProUGUI>();
    }

    public void AddScore(int scoreToAdd)
    {
        gData.score += scoreToAdd;
        gDataRepository.SaveGame(gData);
    }

    public void RemoveLife()
    {
        gData.lives--;
        gDataRepository.SaveGame(gData);
    }

    public void ReduceKunai()
    {
        gData.kunais--;
        gDataRepository.SaveGame(gData);
    }

    public int getKunais()
    {
        return gData.kunais;
    }

    public void AddKunai(int kunais)
    {
        gData.kunais += kunais;
        gDataRepository.SaveGame(gData);
    }

    public GameData GetData()
    {
        return gData;
    }

    void Update()
    {
        scoreText.text = "Puntaje: " + gData.score;

        if (gData.lives > 0)
        {
            livesText.text = "Vidas: " + "".PadRight(gData.lives, '♥');
        }

        kunaisText.text = "Kunais: " + gData.kunais;
        if (gData.lives == 0)
        {
            Debug.Log("Game Over");
        }
    }
}