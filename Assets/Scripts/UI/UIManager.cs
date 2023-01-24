using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public List<Heart> hearts;
    public TextMeshProUGUI diamodText;
    private int diamondCollected = 0;

    private void Start()
    {
        diamondCollected = 0;
        SearchHearts();
        RefreshDiamondText();
    }

    private void SearchHearts()
    {
        hearts = new List<Heart>(GetComponentsInChildren<Heart>());
    }

    public void AddDiamonds(int v)
    {
        for (int i = 0; i < v; i++)
        {
            AddOneDiamond();
        }
    }

    public void AddOneDiamond()
    {
        diamondCollected++;
        RefreshDiamondText();
    }

    private void RefreshDiamondText()
    {
        diamodText.text = diamondCollected.ToString();
    }

    public void SetHeartsNumber(int v)
    {
        int i = 0;
        foreach(Heart h in hearts)
        {
            if (i < v)
                h.gameObject.SetActive(true);
            else
                h.gameObject.SetActive(false);
            i++;
        }
    }

    public void ModifyHearts(Damageable damageable)
    {
        SetHeartsNumber(damageable.actualHealtPoint);
    }

    public void CollectItem(Collectible collectible)
    {
        if (collectible.collectibleType == ECollectibleType.Diamond)
            AddOneDiamond();
    }
}
