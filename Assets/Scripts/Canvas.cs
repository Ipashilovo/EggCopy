using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;


public class Canvas : MonoBehaviour, ISpawnLookerReaction
{
    [SerializeField] private SkilletLooker _skilletLooker;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text[] _levelsNumbers;
    [SerializeField] private ParticleSystem[] _winParticles;
    [SerializeField] private ParticleSystem _looseParticles;
    [SerializeField] private Image _winPanel;
    [SerializeField] private Image _loosePanel;
    private int currentLvl;

    private void Start()
    {
        _skilletLooker.SetNewLookerReaction(this);
        ShowCurrentLevelNumbers();
    }
    
    public void InitValue(int value)
    {
        SetSliderAmount(value);
    }
    
    public void OnLevelLoose()
    {
        _loosePanel.gameObject.SetActive(true);
        _looseParticles.gameObject.SetActive(true);
    }
    
    public void OnChangeValue()
    {
        ChangeSliderValue();
    }
    
    public void OnLevelWin()
    {
        ShowWinUI();
    }

    private void ShowCurrentLevelNumbers()
    {
        if (PlayerPrefs.HasKey(StaticFields.SaveName))
            currentLvl = PlayerPrefs.GetInt((StaticFields.SaveName));
        else
            currentLvl = 1;
        _levelsNumbers[0].text = currentLvl.ToString();
        _levelsNumbers[1].text = (++currentLvl).ToString();
    }
    
    private void SetSliderAmount(int value)
    {
        _slider.maxValue = value;
    }

    private void ChangeSliderValue()
    {
        _slider.value += 1;
    }

    private void ShowWinUI()
    {
        PlayerPrefs.SetInt(StaticFields.SaveName, currentLvl);
        foreach (var particle in _winParticles)
        {
            particle.gameObject.SetActive(true);
        }
        _winPanel.gameObject.SetActive(true);
    }
}
