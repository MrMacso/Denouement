using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [Range(0, 4)]
    [SerializeField] int CurrentStageGizmo = 0;
    [SerializeField] int CurrentStage = 0;//testing, need to rework
    static int MAX_STAGE = 4;//testing, need to rework
    [SerializeField] LevelData _levelData;
    [SerializeField] ClockUI _clockUI;

    //these floats must be between 0 and 24
    float _startingTime = 0f;
    float _endTime = 0f;

    public bool Play = false;
    void Awake()
    {
        SetFirstStage(13);
        SetupClock();
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
    void Update()
    {
        if (_clockUI.GetTimeInHour() >= _endTime)
            _clockUI.SetIsPaused(true);
    }
    void SetFirstStage(float min) 
    {
        _startingTime= min;
        _endTime= _startingTime + 1f;
        _clockUI.SetTime(_startingTime);
    }
    private void SetStage(int stageENum)
    {
        for (int i = 0; i < _levelData._puppetList.Count; i++)
        {
            var stage = _levelData._puppetList[i]._stageDatas[stageENum]._stageList;
            _levelData._puppetList[i].Puppet.SetupDestinationElements(stage);
        }
        //Debug purpose : refresh path for gizmos
        CurrentStageGizmo = CurrentStage;
    }
    // for button must stay public!
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
            _clockUI.SetIsPaused(false);
            Play = true;
        }
        else
        { 
            StartCoroutine(EnterNextStage());
        }
    }
    IEnumerator EnterNextStage()
    {
        CurrentStage++;
        SetStage(CurrentStage);
        SetClock(false);
        for (int i = 0; i < _levelData._puppetList.Count; i++)
        {
            _levelData._puppetList[i].Puppet.SetDestinationIndex(-1);
            _levelData._puppetList[i].Puppet.ResetState();
            _levelData._puppetList[i].Puppet.SetActive(true);
        }   
        yield return new WaitForSeconds(25.0f);
    }
    void SetClock(bool isPaused) 
    {
        _clockUI.SetIsPaused(isPaused);
        _endTime += 1f;
    }

    void SetupClock() 
    {
        _clockUI.SetTime(_startingTime);
        _clockUI.SetIsPaused(true);
    }
    enum STAGE
    {
        ONE, TWO, THREE, FOUR, FIVE
    }
}
