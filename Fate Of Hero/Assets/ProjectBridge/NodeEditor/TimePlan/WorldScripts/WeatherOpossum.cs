using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EWeather {SunnyDay, Thunder }
public class WeatherOpossum : MonoBehaviour
{
	public List<Biom> bioms { get; private set; }

	private void Awake()
	{
		bioms = FindObjectsOfType<Biom>().ToList();
	}
	public void CheckWeather()
	{
		foreach (Biom b in FindObjectsOfType<Biom>())
		{
			b.CheckWeather();
		}
	}
	public EWeather GetWeatherInBiom(Biom biom)
	{
		return biom.Weather;
	}
}
