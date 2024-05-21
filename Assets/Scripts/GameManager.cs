using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    public GameObject[] arrows; // Sabit oklar için dizi
    public GameObject[] arrowPrefabs; // Ritmik olarak sürüklenen ok için prefab
    public Transform[] arrowSpawnPoints; // Okların başlangıç noktasoı
    public KeyCode[] arrowKeys; // Ok tuşlarınan keyCode'ları
    public Text scoreText; // Skor metni
    public Text AIscoreText; // Skor metni
    public GameObject resultPanel; // Sonuç paneli
    public Text resultText; // Sonuç metni
    public int score = 0; // Skor
    public int AIscore = 0; // Skor
    private float spawnInterval = 2f; // Okların spawn aralığı
    private float nextSpawnTime = 0f; // Bir sonraki okun spawn zamanı
    public Transform[] arrowPoints; // Sabit ok noktası
    public float arrowProximityThreshold = 1000000000000f; // Okun sabit ok noktasına olan mesafe eşik değeri
    private int currentLevel = 1; // Oyuncunun şu anki seviyesi

    // ArrowsSpawnCanvas referansı
    public Canvas arrowsSpawnCanvas;

    private bool spawningEnabled = true; // Spawn işleminin etkin olup olmadığını belirten değişken

    // Function to get the arrow key for a specific arrow
    public KeyCode GetArrowKey(GameObject arrow)
    {
        if (arrow.CompareTag("UpArrow"))
        {
            return KeyCode.UpArrow;
        }
        else if (arrow.CompareTag("DownArrow"))
        {
            return KeyCode.DownArrow;
        }
        else if (arrow.CompareTag("RightArrow"))
        {
            return KeyCode.RightArrow;
        }
        else if (arrow.CompareTag("LeftArrow"))
        {
            return KeyCode.LeftArrow;
        }
        else
        {
            // Herhangi bir özel duruma göre tuş belirleme
            return KeyCode.Space; 
        }
    }

    void Start()
{
    if (_instance != null && _instance != this)
    {
        Destroy(gameObject);
    }
    else
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 100 saniye sonra spawn işlemini durdur
    StartCoroutine(StopSpawningAfterTime(100f));

// Seviyeye göre spawnInterval değerini ayarla
    if (SceneManager.GetActiveScene().name == "Dance")
    {
        if (currentLevel == 1)
        {
            spawnInterval = 2f; // Level 1 için spawnInterval değeri
        }
        else if (currentLevel == 2)
        {
            spawnInterval = 1.5f; // Level 2 için spawnInterval değeri
        }
        else if (currentLevel == 3)
        {
            spawnInterval = 1f; // Level 3 için spawnInterval değeri
        }
        
    }
}


    void Update()
    {
        // Okları ritmik olarak sürükleyen fonksiyon
        if (Time.time >= nextSpawnTime && spawningEnabled)
        {
            SpawnRandomArrow();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    // Rastgele bir yön tuşu seçerek spawn eden fonksiyon
    void SpawnRandomArrow()
    {
        int randomIndex = Random.Range(0, arrowPrefabs.Length);
        // Arrow'ları ArrowsSpawnCanvas altında oluştur
        GameObject newArrow = Instantiate(arrowPrefabs[randomIndex], arrowSpawnPoints[randomIndex].position, Quaternion.identity, arrowsSpawnCanvas.transform);
        Destroy(newArrow, 5f); // 5 saniye sonra oku yok et
    }

    public bool IsMatchingKey(KeyCode key)
    {
        // Tuşun doğru eşleşip eşleşmediğini kontrol et
        KeyCode expectedKey = GetArrowKey(gameObject); // Arrow'a atanmış olan tuşu al
        return key == expectedKey;
    }

    public void AddScore(int points)
    {
        // Skora puan ekle
        score += points;
        UpdateScoreText();
    }

    public void AIAddScore(int points)
    {
        // Skora puan ekle
        AIscore += points;
        UpdateAIScoreText();
    }

    // Skor metnini güncelleyen fonksiyon
    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
    
    // Skor metnini güncelleyen fonksiyon
    void UpdateAIScoreText()
    {
        AIscoreText.text = "Score: " + AIscore.ToString();
    }

    // Spawn işlemini belirli bir süre sonra durduracak coroutine
    IEnumerator StopSpawningAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        spawningEnabled = false;

        // Sonucu göster
        ShowResult();

        // Eğer kazanıldıysa bir sonraki seviye
        if (score > AIscore)
        {
            currentLevel++;
            PlayerPrefs.SetInt("Level" + currentLevel.ToString() + "Unlocked", 1);
        }
    }

    // Sonucu gösteren fonksiyon
    void ShowResult()
    {
        resultPanel.SetActive(true);

        if (score > AIscore)
        {
            resultText.text = "Win";
        }
        else if (score < AIscore)
        {
            resultText.text = "Lose";
        }
        else
        {
            resultText.text = "Draw";
        }
    }
}

