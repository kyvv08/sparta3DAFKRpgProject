using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInfoUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI curStageText;

    private void Awake()
    {
        #if UNITY_EDITOR
        if (goldText == null)
        {
            Debug.LogWarning("goldText is null, so FindChild<T> is excuted");
            goldText = Util.FindChild<Transform>(this.transform,"CurMoney").GetComponent<TextMeshProUGUI>();
        }

        if (curStageText == null)
        {
            Debug.LogWarning("curStageText is null, so FindChild<T> is excuted");
            curStageText = Util.FindChild<TextMeshProUGUI>(this.transform,"CurStage").GetComponent<TextMeshProUGUI>();
        }
        
        #endif
    }

    public void SetCurMoney(uint value)
    {
        goldText.SetText($"{value:N0}");
    }

    public void SetCurStage(int value)
    {
        curStageText.SetText(value.ToString());
    }
}
