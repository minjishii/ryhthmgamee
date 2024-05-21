using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScoreManager : MonoBehaviour
{
    
    // Puan değeri
    public int scoreValue = 10;

    // Yakınlık eşik değeri
    public float proximityThreshold = 50f;

    // Puan alındığında ekranda gösterilecek metin nesnesi
    public Text feedbackText;

    // Puan alındığında gösterilecek metin
    public string feedbackMessage = "Miss!";

    // Okun nesnesi
    public GameObject arrowObject;

    // Tuş kodu
    public KeyCode arrowKey;

    // Sabit ok nesnesi
    public GameObject staticArrow;

    // Skor yöneticisinin puan ekleyen fonksiyonu
    public void AddScore()
    {
        GameManager.Instance.AddScore(scoreValue);
        ShowFeedback();
        Destroy(arrowObject);
    }

    // Skor yöneticisinin geri bildirimi ekranda gösteren fonksiyon
    void ShowFeedback()
    {
        feedbackText.text = feedbackMessage;
        StartCoroutine(HideFeedback());
    }

    // Geri bildirimi belirli bir süre sonra gizleyen coroutine
    IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2f);
        feedbackText.text = "";
    }

    void Update()
    {
        // Eğer belirli bir tuşa basıldıysa ve sabit oka yakınsa
        if (Input.GetKeyDown(arrowKey) && IsStaticArrowNearby())
        {
            // Puan ekleme işlemi
            AddScore();
        }
    }

    // Sabit oka yakınlığı kontrol eden fonksiyon
    bool IsStaticArrowNearby()
    {
        float distanceToStaticArrow = Vector3.Distance(transform.position, staticArrow.transform.position);
        return distanceToStaticArrow < proximityThreshold;
    }
}