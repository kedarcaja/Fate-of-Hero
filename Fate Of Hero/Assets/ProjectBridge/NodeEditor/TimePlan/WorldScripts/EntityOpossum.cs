using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityOpossum : MonoBehaviour
{

	public List<CharacterScript> characters { get; private set; } = new List<CharacterScript>();
	private void Awake()
	{
		characters.Clear();


        characters.AddRange(FindObjectsOfType<EnemyScript>());
        characters.AddRange(FindObjectsOfType<NPCScript>());
    }
	public Biom GetEntityBiom(CharacterScript character)
	{

        foreach (Biom b in MainOpossum.WeatherOpossum.bioms)
		{
			if (b.IsInBiom(character.transform))
			{

				return b;
			}

        }

        return null;
	}
}
