using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void GoToSongsScene()
    {
       
        SceneManager.LoadScene("Songs"); 
    }

    

}
