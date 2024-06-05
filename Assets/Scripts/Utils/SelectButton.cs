using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private List<Image> images;
    [SerializeField] private Sprite selectImage;
    [SerializeField] private Sprite deselectImage;

    public void UpdateSprite(int index)
    {
        for (int i = 0;i < images.Count; i++)
        {
            if(i == index)
            {
                images[i].sprite = selectImage;
            }
            else
            {
                images[i].sprite = deselectImage;
            }
        }
    }
}
