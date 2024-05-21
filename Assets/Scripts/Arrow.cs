using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform targetPoint; // Sabit ok noktası
    public float moveSpeed = 40f; // Okun hareket hızı
    public GameObject arrowImagePrefab; // Arrow'ların Image prefab'ı

    void Start()
    {
        // Arrow Image prefab'ını instantiate et
        GameObject arrowImageObject = Instantiate(arrowImagePrefab, transform.position, Quaternion.identity);

        // Arrow Image prefabın parent'ını ayarla 
        arrowImageObject.transform.SetParent(transform);

        // Arrow Image prefab boyutunu ayarla
        RectTransform rectTransform = arrowImageObject.GetComponent<RectTransform>();
        RectTransform parentRectTransform = GetComponent<RectTransform>(); // Arrow'n RectTransform'ini al
        rectTransform.sizeDelta = parentRectTransform.sizeDelta; // Arrow'n boyutunu ayarla

        // Arrow Image prefab görünürlülüğü
        arrowImageObject.SetActive(true);
    }
    // Okun sabit ok noktasına olan mesafesini kontrol eden fonksiyon
    public bool IsNearby()
    {
        // Okun, sabit ok noktasına olan mesafesi
        for (int i = 0; i < GameManager.Instance.arrowPoints.Length; i++)
        {
            float distanceToArrowPoint = Vector2.Distance(transform.position, GameManager.Instance.arrowPoints[i].position);

            // Mesafe sabit bir değerden küçükse true döner
            if (distanceToArrowPoint < GameManager.Instance.arrowProximityThreshold)
            {
                return true;
            }
        }
        return false;
    }

    // Basılan tuşun okun beklendiği tuşla eşleşip eşleşmediğini kontrol eden fonksiyon
    public bool IsMatchingKey(KeyCode key)
    {
        // Okun beklendiği tuşu
        KeyCode expectedKey = GameManager.Instance.GetArrowKey(gameObject);

        // Basşlan tuş, beklendiği tuşla eşleşiyorsa true döner
        return key == expectedKey;
    }

    void Update()
    {
        // Ok, sabit ok noktasına doğru hareket ediyor
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // Ok, sabit ok noktasına ulaştırdığnda yok edilebilir veya başka bir işlem yapalabilir
        if (transform.position == targetPoint.position)
        {
            // Oku yok et veya başka bir işlem yap
            Destroy(gameObject);
        }
    }
}
