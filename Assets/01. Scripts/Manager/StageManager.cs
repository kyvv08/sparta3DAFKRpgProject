using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Stage",menuName = "StageData")]
public class StageData : ScriptableObject
{
    public int stageNum;
    public List<EnemySO> enemies;
    public List<int> enemyNum;
    public Vector3 playerRespawnPosition;
}

public class StageManager : MonoBehaviour
{
    private static StageManager instance;

    public static StageManager Instance { get => instance; }
    private int curStageNum = 1;
    [SerializeField] private StageData[] stageData;
    
    private List<GameObject> enemies;

    private Vector3 centerPosition;
    private float range = 20f;
    private float navMaxDistance = 5f;

    public bool IsInitDone = false;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        InitStage(curStageNum-1);
        enemies = new List<GameObject>();
    }

    public void InitStage(int index)
    {
        IsInitDone = false;
        //Debug.Log(stageData.Length);
        if (index >= stageData.Length)
        {
            return;
        }
        PlayerManager.Instance.player.transform.position = stageData[index].playerRespawnPosition;
        curStageNum = stageData[index].stageNum;
        SpawnEnemyInStage(index);
        UIManager.Instance.gameInfoUI.SetCurStage(curStageNum);
        IsInitDone = true;
    }

    private void SpawnEnemyInStage(int index)
    {
        Vector3 randomPos;
        for(int i = 0; i < stageData[index].enemies.Count;++i) 
        {
            for (int j = 0; j < stageData[index].enemyNum[i]; ++j)
            {
                Debug.Log(i);
                GetRandomPointOnNavMesh(out randomPos);
                Debug.Log("RandomPos: "+ randomPos);
                GameObject temp = Instantiate(stageData[index].enemies[i].EnemyPrefab, randomPos,Quaternion.identity);
                Debug.Log("GameObject"+temp);
                enemies.Add(temp);
            }
        }
        Debug.Log(enemies.Count);
    }
    private void GetRandomPointOnNavMesh(out Vector3 pos)
    {
        pos = (Random.insideUnitSphere * range) + centerPosition;

        NavMeshHit hit;
        
        NavMesh.SamplePosition(pos, out hit,range, NavMesh.AllAreas);
    }

    public void DestroyEnemy(GameObject obj)
    {
        foreach (GameObject enemy in enemies)
        {
            if (obj == enemy)
            {
                Destroy(obj);
                return;
            }
        }
    }

    public List<GameObject> GetEnemyList()
    {
        return enemies;
    }
}
