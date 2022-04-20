using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Sprite manaPotionSprite;
    public Sprite medkitSprite;
    public Sprite shildSprite;
    public Sprite ringSprite;
    public Sprite necklaceSprite;
    public Sprite bibSprite;
    public Sprite bowSprite;
    public Sprite helmetSprite;
    public Sprite bootsSprite;
}
