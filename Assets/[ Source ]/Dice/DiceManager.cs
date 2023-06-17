using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : SingletonMonoBehaviour<DiceManager>
{
    public List<DieProcessor> diceList;

    public DieProcessor farthestDie { get; private set; }
    public DieProcessor closestDie { get; private set; }

    public event Action<int, Vector3> ShowResults;

    public void RollDice()
    {
        foreach (DieProcessor die in diceList) die.Roll();
    }

    private void Start()
    {
        foreach (DieProcessor die in diceList) { die.transform.SetParent(null); }
        ReorderDice();
        DieProcessor.completeRoll += ProcessDie;
    }

    private int diceDone = 0;
    private void ProcessDie(int finalValue, DieProcessor concludedDie)
    {
        diceList.Remove(concludedDie);
        bool actuallyDead = true;
        foreach (DieProcessor die in diceList)
        {
            actuallyDead = die.ReceiveBuff(concludedDie);
            if (!actuallyDead) break;
        }

        if (actuallyDead)
        {
            diceDone++;
            if (diceDone >= 3)
            {
                ShowResults?.Invoke(finalValue, concludedDie.transform.position);
                return;
            }
            ReorderDice();
            Destroy(concludedDie.gameObject);
        }
        else
        {
            diceList.Add(concludedDie);
            ReorderDice();
        }
    }

    private void Update()
    {
        if (diceList.Count == 0) return;
        transform.position = GetCenterPoint();
        ReorderDice();
    }
    private void ReorderDice()
    {
        farthestDie = diceList[0];
        closestDie = diceList[diceList.Count - 1];

        foreach (DieProcessor die in diceList)
        {
            if (die.transform.position.z > farthestDie.transform.position.z) farthestDie = die;
            else if (die.transform.position.z < closestDie.transform.position.z) closestDie = die;
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (diceList.Count == 0) return transform.position;
        else if (diceList.Count == 1) return diceList[0].transform.position;

        Bounds bounds = new Bounds(diceList[0].transform.position, Vector3.zero);
        foreach (DieProcessor die in diceList) bounds.Encapsulate(die.transform.position);
        return bounds.center;
    }
}
