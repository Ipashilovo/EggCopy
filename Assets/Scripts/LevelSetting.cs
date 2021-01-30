using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetting
{
    public int Speed { get; private set; }
    public SkilletLineSetting[] SkilletLines { get; private set; }

    public LevelSetting(int speed, SkilletLineSetting[] skilletLines)
    {
        Speed = speed;
        SkilletLines = skilletLines;
    }
}

public class SkilletLineSetting
{
    public int SkilletsRotation { get; private set; }
    public int Amount { get; private set; }
    public int UsefullNumbers { get; private set; }
    public int ColorNumber { get; private set; }

    public SkilletLineSetting(int amount, int usefullNumbers, int colorNumber)
    {
        SkilletsRotation = 10;
        Amount = amount;
        UsefullNumbers = usefullNumbers;
        ColorNumber = colorNumber;
    }
}