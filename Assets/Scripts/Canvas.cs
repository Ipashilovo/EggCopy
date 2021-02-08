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


public class Canvas : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private DropSpownerButton _spawner;
    [SerializeField] private Text[] _levelsNumbers;
    [SerializeField] private Mover _mover;
    [SerializeField] private ParticleSystem[] _winParticles;
    [SerializeField] private ParticleSystem _looseParticles;
    [SerializeField] private Image _winPanel;
    [SerializeField] private Image _loosePanel;
    private int currentLvl;
    

    private List<Skillet> _skillets = new List<Skillet>();

    private void Start()
    {
        ShowCurrentLevelNumbers();
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

    public void SetSkilletList(List<Skillet> skillets)
    {
        _skillets = skillets;
        SetSliderAmount();

        foreach (Skillet skillet in _skillets)
        {
            skillet.OnUsefullPieceTriggered += RemoveSkillet;
            skillet.OnUslessPieceTriggered += OnLevelLoose;
        }
    }

    private void SetSliderAmount()
    {
        _slider.maxValue = _skillets.Count;
    }

    private void ChangeSliderValue()
    {
        _slider.value += 1;
    }

    private void RemoveSkillet(Skillet skillet)
    {
        skillet.OnUsefullPieceTriggered -= RemoveSkillet;
        skillet.OnUslessPieceTriggered -= OnLevelLoose;
        _skillets.Remove(skillet);
        CheckSkilletList();
        ChangeSliderValue();
    }

    private void CheckSkilletList()
    {
        if (_skillets.Count == 0)
        {
            ShowWinUI();
        }
    }
    
    private void ShowWinUI()
    {
        _spawner.gameObject.SetActive(false);
        PlayerPrefs.SetInt(StaticFields.SaveName, currentLvl);
        Destroy(_mover);
        foreach (var particle in _winParticles)
        {
            particle.gameObject.SetActive(true);
        }
        _winPanel.gameObject.SetActive(true);
    }

    public void OnLevelLoose()
    {
        PlayerPrefs.DeleteKey(StaticFields.SaveName);
        _mover.RemoveSpeed();
        _spawner.gameObject.SetActive(false);
        _loosePanel.gameObject.SetActive(true);
        _looseParticles.gameObject.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnDisable()
    {
        foreach (Skillet skillet in _skillets)
        {
            skillet.OnUslessPieceTriggered -= OnLevelLoose;
            skillet.OnUsefullPieceTriggered -= RemoveSkillet;
        }
    }
}
