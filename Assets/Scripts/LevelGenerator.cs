using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour 
{
    [SerializeField] private PalleteColor _colorPallete;
    [SerializeField] private int _levelDifficultyStep = 25;
    private LevelSetting _currentLevel;
    private NewLevel _newLevel;
    private int _levelValue = 1;
    
    private Type[] _levels =
    {
        typeof(EasyLevel),
        typeof(NormalLevel),
        typeof(HardLevel)
    };

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey(StaticFields.SaveName))
            _levelValue = PlayerPrefs.GetInt(StaticFields.SaveName);

        GetDifficulty(_levelValue);
        _newLevel.SetMaxColorNumber(_colorPallete.GetColorArrayLength());
        _currentLevel = _newLevel.CreateNewLevel();
    }

    public LevelSetting GetCurrentLevel()
    {
        return _currentLevel;
    }
    
    private void GetDifficulty(int lvl)
    {
        int number = lvl / _levelDifficultyStep;
        if (number > _levels.Length)
            number = _levels.Length - 1;
        
        _newLevel = (NewLevel)Activator.CreateInstance(_levels[number]);
    }
}

public abstract class NewLevel
{
    protected SkilletLineSetting[] _skilletLineSetting;
    protected LevelSetting _newLevel;
    protected int _lineAmount;
    protected int _speed;
    protected int _skilletAmount;
    protected int _usefullNumbers;
    protected int _startColorNumber;
    protected int _maxColorNumber;

    public void SetMaxColorNumber(int number)
    {
        _maxColorNumber = number;
    }


    public virtual LevelSetting CreateNewLevel()
    {
        for (int i = 0; i < _lineAmount; i++)
        {
            CreateRandomValue(ref _skilletAmount, ref _usefullNumbers);
            _skilletLineSetting[i] = new SkilletLineSetting(_skilletAmount, _usefullNumbers, _startColorNumber % _maxColorNumber);
            _startColorNumber++;
        }

        _newLevel = new LevelSetting(_speed, _skilletLineSetting);
        return _newLevel;
    }

    protected virtual void CreateRandomValue(ref int skilletAmount, ref int usefullNumbers)
    {
        skilletAmount = Random.Range(8, 20);
        usefullNumbers = Random.Range(0, 3);
    }
}

public class EasyLevel : NewLevel
{
    public EasyLevel()
    {
        _speed = Random.Range(60, 70);
        _startColorNumber = Random.Range(0, 6);
        _lineAmount = Random.Range(3, 4);
        _skilletLineSetting = new SkilletLineSetting[_lineAmount];
    }
}

public class NormalLevel : NewLevel
{

    public NormalLevel()
    {
        _speed = Random.Range(60, 60);
        _startColorNumber = Random.Range(0, 6);
        _lineAmount = Random.Range(4, 5);
        _skilletLineSetting = new SkilletLineSetting[_lineAmount];
    }

    protected override void CreateRandomValue(ref int skilletAmount, ref int usefullNumbers)
    {
        skilletAmount = Random.Range(8, 20);
       usefullNumbers = Random.Range(1, 4);
    }
}

public class HardLevel : NewLevel
{
    public HardLevel()
    {
        _speed = Random.Range(60, 80);
        _startColorNumber = Random.Range(0, 6);
        _lineAmount = Random.Range(4, 6);
        _skilletLineSetting = new SkilletLineSetting[_lineAmount];
    }

    protected override void CreateRandomValue(ref int skilletAmount, ref int usefullNumbers)
    {
        skilletAmount = Random.Range(8, 20);
        usefullNumbers = Random.Range(3, 6);
    }
}