using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum EDays { Monday, Tuesday, Wednesday, Thurstday, Friday, Saturday, Sunday }
public class WorldTimeOpossum : MonoBehaviour
{
	private int minutes, seconds, hours;
	private EDays days = EDays.Monday;
	public event Action OnTimerStart, OnTimerEnd, OnTimerUpdate, OnDayChange, OnTimeSkip;
	private const float delay = 1.5f;
	public bool IsRunning = true;
	public int Seconds { get => seconds; }
	public int Minutes { get => minutes; }
	public int Hours { get => hours; }
	public EDays Day { get => days; }
	public TimeSpan GetTimeAsTimeSpan { get { return new TimeSpan(Hours, Minutes, Seconds); } }

	int minuteBefore = 0; // to trigger minutes
	
	private void Start()
	{

		Begin();

	}
	private void Update()
	{
	//	Debug.Log("Day: " + Day.ToString() + " Hours: " + Hours + " Minutes: " + Minutes + " Seconds: " + Seconds);
		if (Input.GetKeyDown(KeyCode.T))
		{
			SkipMinute();
		}
		if (Input.GetKeyDown(KeyCode.P))
		{
			Stop();
		}
		if (Input.GetKeyDown(KeyCode.O))
		{
			Begin();
		}


		if (minutes % 2 == 0 && minutes > 0 && minutes != minuteBefore)
		{

			minuteBefore = minutes;
			MainOpossum.WeatherOpossum.CheckWeather();

		}
	}


	public void Begin()
	{
		IsRunning = true;
		if (OnTimerStart != null)
		{
			OnTimerStart();
		}

		StartCoroutine("UpdateTime");

	}

	private IEnumerator UpdateTime()
	{

		while (IsRunning)
		{
			yield return new WaitForSecondsRealtime(delay);

			seconds++;

			if (OnTimerUpdate != null)
			{
				OnTimerUpdate();
			}
			CheckConversion();
			Restart();
		}
	}
	public void CheckConversion()
	{
		if (seconds >= 60)
		{

			minutes += ((seconds - (seconds % 60)) / 60);

			seconds = seconds % 60;


		}
		if (minutes >= 60)
		{
			hours += ((minutes - (minutes % 60)) / 60);

			minutes = minutes % 60;


		}
		if (hours == 24)
		{
			if ((int)days == 7)
			{
				days = 0;
			}
			else
			{
				days++;
			}
			hours = 0;
			if (OnDayChange != null)
			{
				OnDayChange();
			}
		}
	}
	public void Stop()
	{
		if (IsRunning)
		{
			if (OnTimerEnd != null)
			{
				OnTimerEnd();
			}
			IsRunning = false;

			StopAllCoroutines();

		}
	}
	private void Restart()
	{
		Stop();
		Begin();
	}
	public void SkipMinute()
	{
		Skip(60);
	}
	public void SkipHour()
	{
		Skip(3600);
	}
	public void Skip(int seconds)
	{
		Stop();

		this.seconds += seconds;
		CheckConversion();
		if (OnTimeSkip != null)
		{
			OnTimeSkip();
		}
		Begin();
	}


}






