using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Puan değerleri
    public int goodScoreValue = 10;
    public int perfectScoreValue = 50;

    // Yakınlık eşik değerleri
    public float goodProximityThreshold = 50f;
    public float perfectProximityThreshold = 1f;
    public float missProximityThreshold = 100f;

    // Belirli bir tuş
    public KeyCode targetKey;

    // Puan alındığında ekranda gösterilecek metin nesnesi
    public Text feedbackText;

    // Puan alındığında gösterilecek metinler
    public string goodFeedbackText = "Good!";
    public string perfectFeedbackText = "Perfect!";
    public string missFeedbackText = "Miss!";

    // Okun nesnesi
    public GameObject arrowObject;

    // Mouse objesi
    public GameObject mouseObject;

    // Animator bileşeni
    private Animator animator;

    void Start()
    {
        // Mouse objesinin animator bileşenini al
        animator = mouseObject.GetComponent<Animator>();
    }

    void Update()
    {
        // Eğer belirli bir tuşa basıldıysa
        if (Input.GetKeyDown(targetKey))
        {
            // Player tag'ine yakınsa
            if (IsPlayerNearby(perfectProximityThreshold))
            {
                // Perfect puanı
                GameManager.Instance.AddScore(perfectScoreValue);
                ShowFeedback(perfectFeedbackText);
                Destroy(arrowObject);
                animator.SetTrigger("Perfect");
            }
            // Good mesafede ise
            else if (IsPlayerNearby(goodProximityThreshold))
            {
                // Good puanı
                GameManager.Instance.AddScore(goodScoreValue);
                ShowFeedback(goodFeedbackText);
                Destroy(arrowObject);
                animator.SetTrigger("Good");
            }
            // Miss mesafede ise
            else if (IsPlayerNearby(missProximityThreshold))
            {
                // Miss
                ShowFeedback(missFeedbackText);
                Destroy(arrowObject);
                animator.SetTrigger("Miss");
            }
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
