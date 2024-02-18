using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [TextArea(1, 10)]
    [SerializeField] public List<string> dialogText;

    [SerializeField] public string nameCharacter;
    [SerializeField] public Sprite iconImage;
    [SerializeField] public AudioClip sound;
    public int indexDialog = 0;


}
