using UnityEngine;

public class ScalablePanel : MonoBehaviour
{
    [Tooltip("50% = 0.5, 100% = 1.0")]
    [SerializeField] private float widthPercentage = 0.5f;

    [SerializeField] private float heightPercentage = 1.0f;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Vector2Int size = GetSize(widthPercentage, heightPercentage);
            rectTransform.sizeDelta = new Vector2(size.x, size.y);
        }
    }


    Vector2Int GetSize(float widthPercentage, float heightPercentage)
    {
        Vector2Int currentResolution = new Manager().GetCurrentResolution();
        int width = Mathf.RoundToInt(currentResolution.x * widthPercentage);
        int height = Mathf.RoundToInt(currentResolution.y * heightPercentage);
        Debug.Log($"width: {width}, height: {height} for resolution {currentResolution.x} * {widthPercentage} / {currentResolution.y} * {heightPercentage}");
        return new Vector2Int(width, height);
    }




}
