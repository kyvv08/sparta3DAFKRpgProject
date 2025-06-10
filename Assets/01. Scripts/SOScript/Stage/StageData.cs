using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage",menuName = "StageData")]
public class StageData : ScriptableObject
{
    public int stageNum;
    public List<EnemySO> enemies;
    public List<int> enemyNum;
    public Vector3 playerRespawnPosition;
}
