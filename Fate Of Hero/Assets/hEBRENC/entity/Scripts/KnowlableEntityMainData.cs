using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName = "HerBestEncyk/EntityMainData")]
public class KnowlableEntityMainData : KnowlableEntityData
{

	[SerializeField]
	private Sprite portait, siluet;
	public Sprite Sprite { get { return IsKnown ? portait : siluet; } }
	public string _Name { get { return IsKnown ? name : ""; } }


}
