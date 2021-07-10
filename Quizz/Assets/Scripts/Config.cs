using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Config")]
public class Config : ScriptableObject
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<string> namesOfObjects;

    public List<string> GetNames()
    {
        return namesOfObjects;
    }

    public List<Sprite> GetSprites()
    {
        return sprites;
    }
}