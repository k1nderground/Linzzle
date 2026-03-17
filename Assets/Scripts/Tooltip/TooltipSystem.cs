using UnityEngine;
using TMPro;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    private GameObject _root;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _descText;
    private RectTransform _rect;
    private Canvas _canvas;

    private const float OffsetX = 20f;
    private const float OffsetY = -20f;
    private const float Padding = 12f;

    public void SetupReferences(GameObject root, TextMeshProUGUI name,
        TextMeshProUGUI price, TextMeshProUGUI desc, Canvas canvas)
    {
        _root = root;
        _nameText = name;
        _priceText = price;
        _descText = desc;
        _rect = root.GetComponent<RectTransform>();
        _canvas = canvas;
        instance = this;

        root.SetActive(false);
    }

    void Update()
    {
        if (_root != null && _root.activeSelf)
            FollowCursor();
    }

    public static void Show(string label, int price, string description = "")
    {
        if (instance == null) return;

        instance._nameText.text = label;
        instance._priceText.text = "Цена: " + price + " $";
        instance._descText.text = description;

        instance._descText.gameObject.SetActive(!string.IsNullOrEmpty(description));
        instance._root.SetActive(true);

        Canvas.ForceUpdateCanvases();
        instance.FollowCursor();
    }

    public static void Hide()
    {
        if (instance == null) return;
        instance._root.SetActive(false);
    }

    void FollowCursor()
    {
        float scale = _canvas.scaleFactor;

        float w = _rect.sizeDelta.x * scale;
        float h = _rect.sizeDelta.y * scale;

        float x = Input.mousePosition.x + OffsetX * scale;
        float y = Input.mousePosition.y + OffsetY * scale;

        x = Mathf.Clamp(x, Padding, Screen.width - w - Padding);
        y = Mathf.Clamp(y, h + Padding, Screen.height - Padding);

        _rect.position = new Vector3(x, y, 0f);
    }
}