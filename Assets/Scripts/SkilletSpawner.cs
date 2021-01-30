using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilletSpawner : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Mover _mover;
    [SerializeField] LevelGenerator _levelGenerator;
    [SerializeField] UsefullPiecesVariants _usefullPiecesVariants;
    [SerializeField] private ColorArray _colorArray;
    [SerializeField] private Skillet _skillet;
    private List<Skillet> _skillets = new List<Skillet>();
    private LevelSetting _currentLevel;

    private void Start()
    {
        _currentLevel = _levelGenerator.GetCurrentLevel();
        int rotate = 0;
        Vector3 shift = transform.position;
        for (int i = 0; i < _currentLevel.SkilletLines.Length; i++)
        {
            CreateSkilletLine(_currentLevel.SkilletLines[i], shift, rotate);
            shift -= new Vector3(0, _currentLevel.SkilletLines[i].Amount, 0) * StaticFields.SkilletStep;
            rotate += _currentLevel.SkilletLines[i].SkilletsRotation * _currentLevel.SkilletLines[i].Amount;
        }
        _canvas.SetSkilletList(_skillets);
        _mover.SetSkillets(_skillets);
    }

    private void CreateSkilletLine(SkilletLineSetting lineSetting, Vector3 startPosition, int startRotation)
    {
        int colorNumber = lineSetting.ColorNumber;
        for (int i = 0; i < lineSetting.Amount; i++)
        {
            var newSkillet = Instantiate(_skillet, startPosition - new Vector3(0, StaticFields.SkilletStep * i, 0), Quaternion.identity);
            newSkillet.transform.Rotate(0, startRotation + lineSetting.SkilletsRotation * i, 0);
            newSkillet.transform.SetParent(this.transform);

            Color color = _colorArray.GetColor(colorNumber);
            newSkillet.SetColor(color);

            int[] usefullVariant = _usefullPiecesVariants.GetUsefullPiecesArray(lineSetting.UsefullNumbers);
            newSkillet.SetUsefullPieces(usefullVariant);
            
            _skillets.Add(newSkillet);
        }
    }

    public LevelSetting GetCurrentLevel()
    {
        return _currentLevel;
    }
}