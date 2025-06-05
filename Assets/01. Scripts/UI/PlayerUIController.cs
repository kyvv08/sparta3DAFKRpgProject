using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private Image mpBar;
    [SerializeField] private Image expBar;

    private void Awake()
    {
#if (UNITY_EDITOR)
        if (hpBar == null)
        {
            Debug.LogError("hpBar is null, so FindChild is excuted");
            hpBar = Util.FindChild<Transform>(this.transform,"HP").Find("Bar").GetComponent<Image>();
        }

        if (mpBar == null)
        {
            Debug.LogError("mpBar is null, so FindChild is excuted");
            mpBar = Util.FindChild<Transform>(this.transform,"MP").Find("Bar").GetComponent<Image>();
        }

        if (expBar == null)
        {
            Debug.LogError("expBar is null, so FindChild is excuted");
            expBar = Util.FindChild<Transform>(this.transform,"EXP").Find("Bar").GetComponent<Image>();
        }
        #endif
    }

    private void Start()
    {
        TestCode();
    }

    void TestCode()
    {
        SetHp(.5f);
        SetMp(.3f);
        SetExp(.8f);
    }
    
    public void SetHp(float percentage)
    {
        hpBar.fillAmount = percentage;
    }

    public void SetMp(float percentage)
    {
        mpBar.fillAmount = percentage;
    }

    public void SetExp(float percentage)
    {
        expBar.fillAmount = percentage;
    }
}
