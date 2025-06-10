using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StageSelectButton : MonoBehaviour
{
    public int stageIndex;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick(stageIndex));
    }

    void OnClick(int index)
    {
        // 여기서 스테이지 로딩 함수 호출
        StageManager.Instance.InitStage(index);
    }
}