using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
[CreateAssetMenu(menuName = "HerBestEncyk/EntityOtherImageData", fileName = "")]
public class KnowlableOtherImageData : KnowlableEntityData
{
	[SerializeField]
	private Sprite sprite;
	public Sprite Sprite { get { return sprite; } }
}
public interface IInstanceable
{
	void CreateInstance(Transform parent);
}