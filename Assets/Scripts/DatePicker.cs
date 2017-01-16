using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectedDate {
	public static System.DateTime date = System.DateTime.Now;



}
public class DateCallback : AndroidJavaProxy
{
	public DateCallback() : base("android.app.DatePickerDialog$OnDateSetListener") { }

    public void onDateSet(AndroidJavaObject view, int year, int monthOfYear, int day)
    {
        SelectedDate.date = new System.DateTime(year, monthOfYear + 1,  day);
    }

}
public class TimeCallback : AndroidJavaProxy
{
    public TimeCallback() : base("android.app.TimePickerDialog$OnTimeSetListener") { }

	public void onTimeSet(AndroidJavaObject view, int hour, int munite, int Second)
    {
		SelectedDate.date = new System.DateTime(hour,munite,Second);
    }

}

public class DatePicker : MonoBehaviour 
{
	private AndroidJavaObject activity;
    private bool _isRunning;
	private bool _isRunningTime;
	public Text txtDate;
	public Text txtTime;
    void Start()
    {
    }

    public void PickDate()
	{
		new AndroidJavaObject("android.app.DatePickerDialog", activity, new DateCallback(), SelectedDate.date.Year, SelectedDate.date.Month - 1, SelectedDate.date.Day).Call("show");
	}
    void PickTime()
    {
        new AndroidJavaObject("android.app.TimePickerDialog", activity, new TimeCallback(), SelectedDate.date.Hour, SelectedDate.date.Minute - 1).Call("show");
    }
    public void ShowCalenda()
    { 
        _isRunning = true;
	}public void ShowTime()
	{ 
		_isRunningTime = true;
	}
    public void setDate()
    {
        activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(PickDate));
		//txtDate.text = System.String.Format("{0:yyyy-MM-dd}", SelectedDate.date.ToLongDateString());

    }
	public void setTime()
    {
        activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(PickTime));
       
    }
    
    void Update()
    {
		string[] date = System.String.Format("{0:yyyy-MM-dd}", SelectedDate.date.ToShortDateString()).Split('/');
		txtDate.text = date[2] +"-"+ date[0] +"-"+ date[1];
    }
}