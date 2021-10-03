using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject _blackScreen;
    [SerializeField]
    private GameObject _gameOverText;
    [SerializeField]
    private float _fadeSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeToBlack(true, _fadeSpeed));
    }

    private IEnumerator FadeToBlack(bool fadeToBlack = true, float fadeSpeed = 5)
    {
        Color objColor = _blackScreen.GetComponent<Image>().color;
        Color textColor = _gameOverText.GetComponent<Text>().color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while(_blackScreen.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objColor.a + (fadeSpeed * Time.deltaTime);

                objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
                textColor = new Color(textColor.r, textColor.g, textColor.b, fadeAmount);

                _blackScreen.GetComponent<Image>().color = objColor;
                _gameOverText.GetComponent<Text>().color = textColor;

                yield return null;
            }
        }
    }
}
