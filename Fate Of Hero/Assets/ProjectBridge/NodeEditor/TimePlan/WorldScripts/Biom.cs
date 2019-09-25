using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Biom : MonoBehaviour
{
	public EWeather Weather { get; private set; }
	[SerializeField]
	private float sunyProbability, radius;
	[SerializeField]
	private Color gizmosColor;

	public UnityEvent OnWeatherChange;
	private void Start()
	{
		CheckWeather();
	}
	private void Update()
	{
		if (Weather == EWeather.SunnyDay)
		{
			Debug.Log("Is sunny day...");
		}
		else
		{
			Debug.Log("Is thunder...");
		}
	}
	public void CheckWeather()
	{
		float random = Random.Range(1f, 100f);
		if (sunyProbability >= random - Random.Range(0f, 41f))
		{
			if (Weather == EWeather.Thunder) { OnWeatherChange?.Invoke(); }

			Weather = EWeather.SunnyDay;

		}
		else
		{
			if (Weather == EWeather.SunnyDay) { OnWeatherChange?.Invoke(); }
			Weather = EWeather.Thunder;

		}
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = gizmosColor;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	public bool IsInBiom(Transform target)
	{
		return IsInBiom(target.position);
	}
	public bool IsInBiom(Vector3 place)
	{
		return Vector3.Distance(place,transform.position) <= radius;
	}
}
