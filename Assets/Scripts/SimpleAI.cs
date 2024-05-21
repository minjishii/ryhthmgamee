using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAI : MonoBehaviour
{
    // Puan değerleri
    public int goodScoreValue = 10;
    public int perfectScoreValue = 50;

    // Yakınlık eşik değerleri
    public float goodProximityThreshold = 50f;
    public float perfectProximityThreshold = 1f;
    public float missProximityThreshold = 100f;

    // Puan almak için beklenen tuşlar
    public KeyCode[] targetKeys;

    // Puan alındığında ekranda gösterilecek metin nesnesi
    public Text feedbackText;

    // Puan alındığında gösterilecek metinler
    public string goodFeedbackText = "Good!";
    public string perfectFeedbackText = "Perfect!";
    public string missFeedbackText = "Miss!";

    // Okun nesnesi
    public GameObject arrowObject;

    // AI objesi
    public GameObject aiObject;

    // Animator bileşeni
    private Animator animator;

    void Start()
    {
        // Animator bileşenini al
        animator = aiObject.GetComponent<Animator>();

        // Yapay zeka'nın hareketini başlat
        StartCoroutine(AIMovement());
    }

    // Yapay zeka'nın hareket etme coroutine'u
    IEnumerator AIMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f)); // Her 1-3 saniyede bir

            // Rastgele bir tuşa bas
            int randomIndex = Random.Range(0, targetKeys.Length);
            KeyCode randomKey = targetKeys[randomIndex];
            PressKey(randomKey);
        }
    }

    // Belirli bir tuşa basma fonksiyonu
    void PressKey(KeyCode key)
    {
        // Player tag'ine yakınsa
        if (IsPlayerNearby(perfectProximityThreshold))
        {
            // Perfect puanı
            GameManager.Instance.AIAddScore(perfectScoreValue);
            ShowFeedback(perfectFeedbackText);
            animator.SetTrigger("Perfect");
        }
        // Good mesafede ise
        else if (IsPlayerNearby(goodProximityThreshold))
        {
            // Good puanı
            GameManager.Instance.AIAddScore(goodScoreValue);
            ShowFeedback(goodFeedbackText);
            animator.SetTrigger("Good");
        }
        // Miss mesafede ise
        else if (IsPlayerNearby(missProximityThreshold))
        {
            // Miss
            ShowFeedback(missFeedbackText);
            animator.SetTrigger("Miss");
        }
    }

    // Player'a yakınlığı kontrol eden fonksiyon
    bool IsPlayerNearby(float threshold)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < threshold)
            {
                return true;
            }
        }
        return false;
    }

    // Geri bildirimi ekranda gösteren fonksiyon
    void ShowFeedback(string text)
    {
        feedbackText.text = text;
        StartCoroutine(HideFeedback());
    }

    // Geri bildirimi belirli bir süre sonra gizleyen coroutine
    IEnumerator HideFeedback()
    {
        yield return new WaitForSeconds(2f);
        feedbackText.text = "";
    }
}

