using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;


public class Canvas : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _spawn;
    [SerializeField] private  Button _endGame;
    [SerializeField] private Button _winLevel;
    [SerializeField] private Text[] _levelsNumbers;
    [SerializeField] private Mover _mover;
    [SerializeField] private ParticleSystem[] _winParticles;
    [SerializeField] private ParticleSystem _looseParticles;
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
        _spawn.gameObject.SetActive(false);
        PlayerPrefs.SetInt(StaticFields.SaveName, ++currentLvl);
        Destroy(_mover);
        foreach (var particle in _winParticles)
        {
            particle.gameObject.SetActive(true);
        }
        _winLevel.gameObject.SetActive(true);
    }

    public void OnLevelLoose()
    {
        PlayerPrefs.DeleteKey(StaticFields.SaveName);
        _spawn.gameObject.SetActive(false);
        _endGame.gameObject.SetActive(true);
        Destroy(_mover);
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
