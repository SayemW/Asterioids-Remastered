using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the time played on the HUD
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timerText.text = (score).ToString();
    }

    public void addScore()
    {
        score += 100;
        if (score == 5000)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<UFOSpawner>().increase();
        }
        timerText.text = (score).ToString();
    }
}
