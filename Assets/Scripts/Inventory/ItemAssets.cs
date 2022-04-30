using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for assigning sprites
/// </summary>
public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public Transform ItemWorld;
    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite medkitSprite;
    public Sprite shildSprite;
    public Sprite ringSprite;
    public Sprite necklaceSprite;
    public Sprite bibSprite;
    public Sprite helmetSprite;
    public Sprite bootsSprite;
    public Sprite goldSprite;
}
