using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Button startButton;
    
    private void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene(1));
    }
}
