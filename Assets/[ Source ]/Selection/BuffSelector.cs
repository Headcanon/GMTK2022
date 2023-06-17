using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colors { Cyan, Magenta, Yellow }
public class BuffSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;
    [SerializeField] private Colors diceColor;

    public int selectedItemId;

    public void Previous()
    {
        items[selectedItemId].SetActive(false);
        selectedItemId--;
        if (selectedItemId < 0) selectedItemId = items.Count - 1;
        items[selectedItemId].SetActive(true);
    }

    public void Next()
    {
        items[selectedItemId].SetActive(false);
        selectedItemId++;
        if (selectedItemId >= items.Count) selectedItemId = 0;
        items[selectedItemId].SetActive(true);
    }

    public void SetId()
    {
        PlayerPrefs.SetInt(diceColor.ToString() + "Buff", selectedItemId);
    }
}
