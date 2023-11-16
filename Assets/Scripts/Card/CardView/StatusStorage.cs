using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusStorage : MonoBehaviour
{
    [SerializeField] private Sprite[] rarityImages;
    public static Dictionary<string, Sprite> StatusSprites = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var rarityImage in rarityImages)
        {
            StatusSprites.Add(rarityImage.name, rarityImage);
            Debug.Log(rarityImage.name);
        }
    }

    
}
