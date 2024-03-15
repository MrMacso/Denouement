using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string LevelName;
    public List<PuppetData> _puppetList = new List<PuppetData>();
}

[Serializable]
public class PuppetData
{
    [Header("Puppet detalis")]
    public string Name;
    public FollowPathAI Puppet;

    public List<StageData> _stageDatas= new List<StageData>();
}
[Serializable]
public class StageData
{
    public List<Transform> _stageList = new();
}
