using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatLighting : MonoBehaviour
{
    [SerializeField]
    private ThunderBolt _bolt;
    [SerializeField]
    private Light _light;
    [SerializeField]
    private ParticleSystem _sparks;

    [SerializeField]
    private float _minIntensity = 0.1f;

    [SerializeField]
    private float _maxIntensity = 50f;

    [SerializeField]
    private float _minRange = 20f;

    [SerializeField]
    private float _maxRange = 200f;
    [SerializeField]
    private float _minLifetime = 0.2f;

    [SerializeField]
    private float _maxLifetime = 5f;
    [SerializeField]
    private float _minSpeed = 1f;

    [SerializeField]
    private float _maxSpeed = 30f;


    [SerializeField]
    private Color _coolColor = Color.yellow;

    [SerializeField]
    private Color _overheatColor = Color.red;

    private Gradient gradient;


    // Start is called before the first frame update
    void Start()
    {
        _light.intensity = _minIntensity;
        var main = _sparks.main;
        main.startSpeed = _minSpeed;
        main.startLifetime = _minLifetime;
        main.startColor = _coolColor;

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
        setIntensity();
        setLifetime();
        setSpeed();
        setColor();
    }

    private float GetMetricByIntensity(float min, float max, float overheatIntensity)
    {
        float sub = max - min;
        return (sub * overheatIntensity) + min;
    }

    private void setIntensity()
    {
        float overheatMetric = _bolt.GetOverheatMetric();
        float newIntensity = GetMetricByIntensity(_minIntensity, _maxIntensity, overheatMetric);

        _light.intensity = Mathf.PingPong(_light.intensity, newIntensity);

    }
    private void setLifetime()
    {
        float overheatMetric = _bolt.GetOverheatMetric();
        float newIntensity = GetMetricByIntensity(_minLifetime, _maxLifetime, overheatMetric);
        var main = _sparks.main;

        main.startLifetime = newIntensity;

    }
    private void setSpeed()
    {
        float overheatMetric = _bolt.GetOverheatMetric();
        float newIntensity = GetMetricByIntensity(_minSpeed, _maxSpeed, overheatMetric);
        var main = _sparks.main;
        main.startSpeed = newIntensity;
    }

    private void setColor()
    {
        float overheatMetric = _bolt.GetOverheatMetric();
        var main = _sparks.main;
        Color newColor = gradient.Evaluate(overheatMetric);
        main.startColor = newColor;
        _light.color = newColor;
    }
}
