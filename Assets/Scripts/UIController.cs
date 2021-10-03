using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private Image _blackScreen;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _winText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private float _fadeSpeed = 5;

    private Vector3 _buttonScale;
    private Vector3 _textScale;

    // Start is called before the first frame update
    void Start()
    {
        _buttonScale = _restartButton.transform.localScale;
        _restartButton.transform.localScale = new Vector3(0, 0, 0);

        _textScale = _restartText.transform.localScale;
        _restartText.transform.localScale = new Vector3(0, 0, 0);
    }

    public void FadeToBlackGameOver()
    {
        StartCoroutine(FadeToBlack(_blackScreen, _gameOverText, _fadeSpeed));
    }

    public void FadeToBlackWin()
    {
        StartCoroutine(FadeToBlack(_blackScreen, _winText, _fadeSpeed));
    }

    private IEnumerator FadeToBlack(Image image, Text text, float fadeSpeed = 5)
    {
        Color objColor = image.color;
        Color textColor = text.color;
        float fadeAmount;

        while (_blackScreen.GetComponent<Image>().color.a < 1)
        {
            fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime);

            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            textColor = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);

            _blackScreen.GetComponent<Image>().color = objColor;
            _gameOverText.GetComponent<Text>().color = textColor;

            yield return null;
        }

        _restartText.transform.localScale = _textScale;
        _restartButton.transform.localScale = _buttonScale;
    }
}
