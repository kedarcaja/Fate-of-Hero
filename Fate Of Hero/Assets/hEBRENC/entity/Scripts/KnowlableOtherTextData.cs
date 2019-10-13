using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName= "HerBestEncyk/KnowlableOtherTextData", fileName="")]
public class KnowlableOtherTextData : KnowlableEntityData
{
	[SerializeField]
	private string text;

	public string Text
	{
		get
		{
			return text;
		}

	
	}
}
