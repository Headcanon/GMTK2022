using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDice : MonoBehaviour
{
    [SerializeField] private DieProcessor magentaDie;
    [SerializeField] private Transform magentaPosition;

    [SerializeField] private DieProcessor cyanDie;
    [SerializeField] private Transform cyanPosition;

    [SerializeField] private DieProcessor yellowDie;
    [SerializeField] private Transform yellowPosition;

    private List<DieProcessor> diceList;

    private void Awake()
    {
        diceList = new List<DieProcessor>();

        magentaDie.dieBuff = (DieProcessor.Buff) PlayerPrefs.GetInt(Colors.Magenta.ToString() + "Buff");
        diceList.Add(Instantiate(magentaDie, magentaPosition));

        cyanDie.dieBuff = (DieProcessor.Buff)PlayerPrefs.GetInt(Colors.Cyan.ToString() + "Buff");
        diceList.Add(Instantiate(cyanDie, cyanPosition));

        yellowDie.dieBuff = (DieProcessor.Buff)PlayerPrefs.GetInt(Colors.Yellow.ToString() + "Buff");
        diceList.Add(Instantiate(yellowDie, yellowPosition));

        GetComponent<DiceManager>().diceList = diceList;
    }
}
