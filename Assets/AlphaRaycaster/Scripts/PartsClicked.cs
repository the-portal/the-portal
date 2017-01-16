using UnityEngine;

using UnityEngine.UI;


public class PartClicked : MonoBehaviour {
    public float originalBoardWidth = 2339f;
    public float originalPartWidth;
    public float originalPartHeight;
    public float x;
    public float y;
    public float partRatio;
    // Use this for initialization
    void Start()
    {
        RectTransform part = GetComponent<RectTransform>();
        partRatio = originalBoardWidth / (Screen.height * 1f);
        part.sizeDelta = new Vector2(originalPartWidth/partRatio, originalPartHeight / partRatio);
        part.position = new Vector2(x, y);
    }
}
