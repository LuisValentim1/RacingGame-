using System;
using UnityEngine;
using UnityEngine.UI;

public enum DayState { night, sunrise, day, sunset }
public enum WeekDays { sunday, monday, tuesday, wednesday, thursday, friday, saturday }
public enum Months { January, February, March, April, May, June, July, August, September, October, November, December }
public enum Season { Autumn, Winter, Spring, Summer }

public class TimeCalendar : MonoBehaviour
{
    public static TimeCalendar instance;
    public static TimeCalendar Get() { return instance; }

    // VARIABLES - PUBLIC
    [Header("Settings:")]
    public int speed = 120;

    [Space(15)]
    [Header("Current Time:")]
    public float seconds = 0;
    public float minutes = 0, hours = 8;
    public float day = 15, month = 9, year = 2021;

    public int currentWeekDayNumber = 1;

    public DayState currentDayState;
    public WeekDays currentWeekDay;
    public Months currentMonth;
    public Season currentSeason;

    public Text textShowTime;
    public Text txt;
    
    public bool timeFromSystem;

    // METHODS - STANDARD
    private void Awake() {
        instance = this;
    }

    private void Start() {

    }

    public void Update() {
        UpdateTime();
    }

    private void OnValidate() {
        UpdateDayState();
        UpdateMonth();
        UpdateSeason();
    }

    // METHODS - PRIVATE
    void UpdateTime() {
        if (textShowTime != null)
            textShowTime.text = GetCurrentTime();

        if (Input.GetKeyDown(KeyCode.P)) {
            timeFromSystem = !timeFromSystem;

            if (timeFromSystem == true) {
                txt.text = "P - Deactivate Computer Time";
            } else {
                txt.text = "P - Activate Computer Time";
            }
        }

        if (timeFromSystem == false) {
            seconds += Time.deltaTime * speed;

            if (seconds >= 60) {
                int i = 0;
                for (i = 0; i + 60 <= seconds; i += 60) {
                    minutes++;
                }
                seconds -= i;
            }

            if (minutes >= 60) {
                int i = 0;
                for (i = 0; i + 60 <= minutes; i += 60) {
                    hours++;
                }
                minutes -= i;

                UpdateDayState();
            }

            if (hours >= 24) {
                int i = 0;
                for (i = 0; i + 24 <= minutes; i += 24) {
                    day++;
                }
                hours -= i;

                NextWeekDay();
            }

            if (day >= 10) {
                month++;
                day = 0;
                UpdateMonth();
                UpdateSeason();
            }

            if (month >= 12) {
                year++;
                month = 0;
            }
        } else {
            System.DateTime curDateTime = System.DateTime.Now;
            seconds = curDateTime.Second;
            minutes = curDateTime.Minute;
            hours = curDateTime.Hour;
            day = curDateTime.Day;
            month = curDateTime.Month;
            year = curDateTime.Year;
            
            UpdateDayState();
            UpdateMonth();
            UpdateSeason();
        }
    }

    void NextWeekDay() {
        currentWeekDay++;
        currentWeekDayNumber++;
        if (currentWeekDayNumber > 6) {
            currentWeekDayNumber = 0;
            currentWeekDay = WeekDays.sunday;
        }
    }

    void UpdateDayState() {
        if (hours >= 20 || hours < 8)
            currentDayState = DayState.night;
        else if (hours >= 8 && hours < 9)
            currentDayState = DayState.sunrise;
        else if (hours >= 9 && hours < 19)
            currentDayState = DayState.day;
        else
            currentDayState = DayState.sunset;

        GetComponent<TimeLighting>().OnUpdateDayState(currentDayState);
    }

    void UpdateSeason() {
        if (month >= 8 && month < 11)
            currentSeason = Season.Autumn;
        else if (month >= 11 && month < 2)
            currentSeason = Season.Winter;
        else if (month >= 2 && month < 5)
            currentSeason = Season.Spring;
        else if (month >= 5 && month < 8)
            currentSeason = Season.Summer;
    }

    void UpdateMonth() {
        switch (month) {
            case 0:
                currentMonth = Months.January;
                break;
            case 1:
                currentMonth = Months.February;
                break;
            case 2:
                currentMonth = Months.March;
                break;
            case 3:
                currentMonth = Months.April;
                break;
            case 4:
                currentMonth = Months.May;
                break;
            case 5:
                currentMonth = Months.June;
                break;
            case 6:
                currentMonth = Months.July;
                break;
            case 7:
                currentMonth = Months.August;
                break;
            case 8:
                currentMonth = Months.September;
                break;
            case 9:
                currentMonth = Months.October;
                break;
            case 10:
                currentMonth = Months.November;
                break;
            case 11:
                currentMonth = Months.December;
                break;
        }
    }

    public string GetCurrentTime() {
        return Mathf.Floor(hours) + ":" + Mathf.Floor(minutes) + ":" + Mathf.RoundToInt(seconds);
    }

    public float GetTimeNormalized() {
        float hoursNormalized = Remap(hours, 0, 24, 0, 1);
        float minutesNormalized = Remap(minutes, 0, 60, 0, 1f / 24);
        float secondsNormalized = Remap(seconds, 0, 60, 0, 1f / (24 * 60));
        return hoursNormalized + minutesNormalized + secondsNormalized;
    }

    public float GetTimeNormalized_ToMidDay() {
        float hoursNormalized = RemapLimitBefore(hours, 12, 16, 0, 1);
        float minutesNormalized = Remap(minutes, 0, 60, 0, 1f / 24);
        float secondsNormalized = Remap(seconds, 0, 60, 0, 1f / (24 * 2));
        return hoursNormalized + minutesNormalized + secondsNormalized;
    }

    public float GetTimeNormalized_ToDay() {
        float hoursNormalized = RemapLimitBefore(hours, 6, 21, 0, 1);
        float minutesNormalized = 0;
        float secondsNormalized = 0;

        if (hours >= 6 && hours <= 21) {
            minutesNormalized = Remap(minutes, 0, 60, 0, 1f / 15);
            secondsNormalized = Remap(seconds, 0, 60, 0, 1f / (15 * 2));
        }

        return hoursNormalized + minutesNormalized + secondsNormalized;
    }


    public override string ToString() {
        return "<Calendar> " + year + "/" + month + "/" + day + " - "
            + hours + " hours and " + minutes + " minutes | " + seconds + " seconds;  Day Week = " + currentWeekDay + "; Month = " + currentMonth
            + "; Season = " + currentSeason;
    }
    




    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public Vector3 Remap(Vector3 value, Vector3 from1, Vector3 to1, Vector3 from2, Vector3 to2)
    {
        float x = Remap(value.x, from1.x, to1.x, from2.x, to2.x);
        float y = Remap(value.y, from1.y, to1.y, from2.y, to2.y);
        float z = Remap(value.z, from1.z, to1.z, from2.z, to2.z);
        return new Vector3(x, y, z);
    }

    public float RemapLimitBefore(float value, float from1, float to1, float from2, float to2)
    {
        if (value < from1)
            return from2;
        if (value > to1)
            return to2;
        else
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public float RemapLimitAfter(float value, float from1, float to1, float from2, float to2)
    {
        float newValue = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        if (newValue < from1)
            return from2;
        if (newValue > to1)
            return to2;
        else
            return newValue;
    }
}