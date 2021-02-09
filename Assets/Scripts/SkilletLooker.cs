using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilletLooker : MonoBehaviour
{
    [SerializeField] private DropSpownerButton _dropSpownerButton;
    private List<ISpawnLookerReaction> _spawnLookerReaction = new List<ISpawnLookerReaction>();
    private List<Skillet> _skillets;

    private void OnDisable()
    {
        foreach (var skillet in _skillets)
        {
            skillet.OnUsefullPieceTriggered -= OnUsefullSkilletDestroy;
            skillet.OnUslessPieceTriggered -= OnUselessSkilletDestroy;
        }
    }


    public void SetNewLookerReaction(ISpawnLookerReaction reaction)
    {
        _spawnLookerReaction.Add(reaction);
    }
    
    public void SetSkilletList(List<Skillet> skillets)
    {
        _skillets = skillets;
        foreach (var spawnLookerReaction in _spawnLookerReaction)
        {
            spawnLookerReaction.InitValue(_skillets.Count);
        }

        foreach (var skillet in _skillets)
        {
            skillet.OnUsefullPieceTriggered += OnUsefullSkilletDestroy;
            skillet.OnUslessPieceTriggered += OnUselessSkilletDestroy;
        }
    }

    private void OnUsefullSkilletDestroy(Skillet skillet)
    {
        skillet.OnUsefullPieceTriggered -= OnUsefullSkilletDestroy;
        skillet.OnUslessPieceTriggered -= OnUselessSkilletDestroy;
        _skillets.Remove(skillet);
        foreach (var spawnLookerReaction in _spawnLookerReaction)
        {
            spawnLookerReaction.OnChangeValue();
        }
        if (_skillets.Count == 0)
        {
            OnLevelWin();
        }
    }

    private void OnUselessSkilletDestroy()
    {
        _dropSpownerButton.gameObject.SetActive(false);
        PlayerPrefs.DeleteKey(StaticFields.SaveName);
        foreach (var spawnLookerReaction in _spawnLookerReaction)
        {
            spawnLookerReaction.OnLevelLoose();
        }
    }

    private void OnLevelWin()
    {
        foreach (var spawnLookerReaction in _spawnLookerReaction)
        {
            spawnLookerReaction.OnLevelWin();
        }
    }
}
