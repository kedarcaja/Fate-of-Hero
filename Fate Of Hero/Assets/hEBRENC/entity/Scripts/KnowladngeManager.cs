using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class KnowladngeManager : MonoBehaviour
{
	[SerializeField]
	private GameObject textPrefab,imagePrefab;
	
	public KnowableEntity CurrentEntity { get; set; }
	public GameObject TextPrefab
	{
		get
		{
			return textPrefab;
		}

	
	}

	public GameObject ImagePrefab
	{
		get
		{
			return imagePrefab;
		}

		
	}
	public static KnowladngeManager Instance { get; private set; }
	private void Awake()
	{
		Instance = FindObjectOfType<KnowladngeManager>();
	}
	
}
