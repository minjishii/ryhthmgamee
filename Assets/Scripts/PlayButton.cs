using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        // Butonun onClick eventine OnClick fonksiyonunu ekleyin
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // Oyun sahnesini yükle
        SceneManager.LoadScene("Game");
    }
}