using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [Range(0, 4)]
    [SerializeField] int CurrentStageGizmo = 0;
    [SerializeField] int CurrentStage = 0;//testing, need to rework
    static int MAX_STAGE = 4;//testing, need to rework
    [SerializeField] LevelData _levelData;
    [SerializeField] ClockUI _clockUI;

    int _minTime = 0;
    int _maxTime = 0;
    int _currentTime = 0;
    public bool Play = false;
    void Awake()
    {
        SetTimePeriod(7, 11);
        SetStage((int)STAGE.ONE);
    }

    void OnDrawGizmos()
    {
        List<Vector3> points = new List<Vector3>();
        int puppetIndex = 0;
        float sphereScale = 0.25f;
        //PUPPET ONE
        for (int i = 0; i < _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList.Count; i++)
        {
            var point = _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList[i];
            points.Add(point.position);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i + 1 < points.Count; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
        Gizmos.DrawSphere(points[points.Count - 1], sphereScale);
        points.Clear();

        //PUPPET TWO
        puppetIndex = 1;
        for (int i = 0; i < _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList.Count; i++)
        {
            var point = _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList[i];
            points.Add(point.position);
        }
        Gizmos.color = Color.yellow;
        for (int i = 0; i + 1 < points.Count; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
        Gizmos.DrawSphere(points[points.Count - 1], sphereScale);
        points.Clear();

        //PUPPET THREE
        puppetIndex = 2;
        for (int i = 0; i < _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList.Count; i++)
        {
            var point = _levelData._puppetList[puppetIndex]._stageDatas[CurrentStageGizmo]._stageList[i];
            points.Add(point.position);
        }
        Gizmos.color = Color.green;
        for (int i = 0; i + 1 < points.Count; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
        Gizmos.DrawSphere(points[points.Count - 1], sphereScale);
    }
    void SetTimePeriod(int min, int max) 
    {
        _minTime= min;
        _maxTime= max;
        _currentTime = _minTime;
    }
    private void SetStage(int stageENum)
    {
        for (int i = 0; i < _levelData._puppetList.Count; i++)
        {
            var stage = _levelData._puppetList[i]._stageDatas[stageENum]._stageList;
            _levelData._puppetList[i].Puppet.SetupDestinationElements(stage);
        }

    }
    public void TestButton()
    {
        if (CurrentStage > MAX_STAGE)
            return;
        if (!Play)
        {
            for (int i = 0; i < _levelData._puppetList.Count; i++)
            {
                _levelData._puppetList[i].Puppet.SetActive(true);
            }
            Play = true;
        }
        else
        { 
            StartCoroutine(EnterNextStage());
        }
    }
    IEnumerator EnterNextStage()
    {
        _currentTime++;
        CurrentStage++;
        SetStage(CurrentStage);
        for (int i = 0; i < _levelData._puppetList.Count; i++)
        {
            _levelData._puppetList[i].Puppet.SetDestinationIndex(-1);
            _levelData._puppetList[i].Puppet.ResetState();
            _levelData._puppetList[i].Puppet.SetActive(true);
        }   
        yield return new WaitForSeconds(25.0f);
    }
    enum STAGE
    {
        ONE, TWO, THREE, FOUR, FIVE
    }
}
