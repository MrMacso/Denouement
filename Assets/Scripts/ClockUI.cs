using UnityEngine;
using TMPro;

public class ClockUI : MonoBehaviour
{
    [Range(0,24)]
    [SerializeField] float StartingTime;
    [SerializeField] TMP_Text DigitalTMP;

    Transform _clockHourHandTransform;
    Transform _clockMinuteHandTransform;

    float _currentTime;

    bool _isPaused = false;

    const float INGAME_SECOND_MULTIPLIER = 600f;
    const float DERGREES_IN_CIRCLE = 360f;
    const float SECONDS_IN_DAY = 43200f;
    const float SECONDS_IN_HOUR = 3600f;
    const float SECONDS_IN_MINUTE = 60f;

    void Awake()
    {
        _clockHourHandTransform = transform.Find("HourHand");
        if (_clockHourHandTransform == null)
            Debug.LogWarning("_clockHourHandTransform is empty, give the desired object the name: HourHand");

        _clockMinuteHandTransform = transform.Find("MinuteHand");
        if (_clockMinuteHandTransform == null)
            Debug.LogWarning("_clockHourMinuteTransform is empty, give the desired object the name: MinuteHand");
        
    }
    void Start()
    {
        _currentTime = StartingTime * SECONDS_IN_HOUR;
    }
    void Update()
    {   
        if( GetHour() == 24f)
            _currentTime = 0;
        
        if(!_isPaused)
            _currentTime += Time.deltaTime * INGAME_SECOND_MULTIPLIER;

        //Update hour hand
        _clockHourHandTransform.eulerAngles = new Vector3(0, 0,-_currentTime * DERGREES_IN_CIRCLE / SECONDS_IN_DAY);

        //Update minute hand
        _clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -_currentTime * DERGREES_IN_CIRCLE / SECONDS_IN_HOUR);

        DigitalTMP.text = GetHour().ToString("00") + ":" + GetMinute().ToString("00");
    }
    public float GetHour()
    {
        return Mathf.FloorToInt( _currentTime / SECONDS_IN_HOUR);
    }
    public float GetMinute() 
    {
        float hoursInCurrentTime = Mathf.FloorToInt(_currentTime / SECONDS_IN_HOUR) * SECONDS_IN_HOUR;
        float remainingSeconds = (_currentTime - hoursInCurrentTime) / SECONDS_IN_MINUTE;

        return Mathf.FloorToInt(remainingSeconds);
    }
    public void SetIsPaused(bool isPaused)
    {
        _isPaused= isPaused;
    }
    public void SetTime(float currentTime)
    {
        _currentTime = currentTime;
    }
}
