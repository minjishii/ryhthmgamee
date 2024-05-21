using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    public Text playerScoreText; // Oyuncu skorunu gösterecek metin nesnesi
    public Text AIScoreText; // Yapay zeka skorunu gösterecek metin nesnesi

    void Update()
    {
        // Skor tablosunu güncelle
        UpdateScoreTable();
    }

    void UpdateScoreTable()
    {
        // Oyuncu skorunu göster
        playerScoreText.text = "Score: " + GameManager.Instance.score.ToString();

        // Yapay zeka skorunu göster
        AIScoreText.text = "Score: " + GameManager.Instance.AIscore.ToString();
    }
}

