using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    // Şarkılar için isimlerin ve dosya yollarının bulunduğu bir dizi
    public AudioClip[] songs;

    // Level butonları
    public Button level2Button;
    public Button level3Button;

    void Start()
    {
        // Level butonlarını kontrol et ve gerektiğinde etkinleştir
        if (PlayerPrefs.HasKey("Level2Unlocked"))
        {
            level2Button.interactable = true;
        }

        if (PlayerPrefs.HasKey("Level3Unlocked"))
        {
            level3Button.interactable = true;
        }

        // Level seçim menüsünün bir örneği var mı kontrol et
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelSelection");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    
    // Buton tıklama işlevleri
    public void SelectLevel1()
    {
        
        // "Dance" sahnesine geçiş yap
        SceneManager.LoadScene("Dance");

        // Seçilen şarkıyı çal
        PlaySong(0);
       
    }

    public void SelectLevel2()
    {
        
        SceneManager.LoadScene("Dance");
        PlaySong(1);
        PlayerPrefs.SetInt("Level3Unlocked", 1); // Level 2 tamamlandığında level 3'ü kilitle

       
    }

    public void SelectLevel3()
    {
        SceneManager.LoadScene("Dance");
        PlaySong(2);
        PlayerPrefs.SetInt("Level4Unlocked", 1); // Level 3 tamamlandığında level 4'ü kilitle

    }

    public void SelectLevel4()
    {
        SceneManager.LoadScene("Dance");
        PlaySong(3);
    }


    // Şarkıyı çalacak fonksiyon
    void PlaySong(int index)
    {
        // İndexe göre şarkıyı seç
        AudioClip selectedSong = songs[index];

        // Şarkıyı çalacak olan Audio kaynağını bul
        AudioSource audioSource = FindObjectOfType<AudioSource>();

        // AudioSource bileşenini kontrol et
        if (audioSource != null)
        {
            // Şarkıyı çalacak olan ses kaynağına şarkıyı atayıp çal
            audioSource.clip = selectedSong;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Dance sahnesinde bir AudioSource bulunamadı.");
        }
    }
}



