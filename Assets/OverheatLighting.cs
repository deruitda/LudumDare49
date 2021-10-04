using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class OverheatLighting : MonoBehaviour
{


    [SerializeField]
    private ThunderBolt _bolt;

    [SerializeField]
    private Color _coolColor = Color.yellow;

    [SerializeField]
    private Color _overheatColor = Color.red;

    [SerializeField]
    private Gradient gradient;

    [SerializeField]
    private float _minHaloSize = 0f;

    [SerializeField]
    private float _maxHaloSize = 5f;

    [SerializeField]
    private Light _light;

    // Start is called before the first frame update
    void Start()
    {

        gradient = new Gradient();
        var colorKey = new GradientColorKey[2];
        colorKey[0].color = _coolColor;
        colorKey[0].time = 0.0f;

        colorKey[1].color = _overheatColor;
        colorKey[1].time = 1.0f;

        var alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        SetColor();
    }

    private float GetMetricByIntensity(float min, float max, float overheatIntensity)
    {
        float sub = max - min;
        return (sub * overheatIntensity) + min;
    }

    private void SetColor()
    {
        float overheatMetric = _bolt.GetOverheatMetric();
        Color newColor = gradient.Evaluate(overheatMetric);

        float size = GetMetricByIntensity(_minHaloSize, _maxHaloSize, overheatMetric);

        _light.color = newColor;
        _light.intensity = size;

        //SerializedObject halo = new SerializedObject(GetComponent("Halo"));
        //float size = GetMetricByIntensity(_minHaloSize, _maxHaloSize, overheatMetric);

        //halo.FindProperty("m_Size").floatValue = size;
        //halo.FindProperty("m_Color").colorValue = newColor;
        //halo.ApplyModifiedProperties();
    }
}
