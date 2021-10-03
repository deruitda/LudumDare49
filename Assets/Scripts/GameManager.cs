using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIController _uiController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        _uiController.FadeToBlackGameOver();
    }

    public void Win()
    {
        _uiController.FadeToBlackWin();
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
