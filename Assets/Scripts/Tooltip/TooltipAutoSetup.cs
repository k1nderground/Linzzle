using UnityEngine;
using UnityEngine.UI;

public class TooltipAutoSetup : MonoBehaviour
{
    private static readonly (string objName, string label, int tileid, string desc)[] ButtonData =
    {
        ("HDD_BUTTON", "HDD", 1, "Жёсткий диск для хранения данных"),
        ("COOLER_BUTTON", "Охлаждение", 3, "Снижает температуру компонентов"),
        ("RAM_BUTTON", "RAM", 2, "Оперативная память"),
        ("TABLE_BUTTON1", "Материнская плата 1", 4, "Основная плата, тип 1"),
        ("TABLE_BUTTON2", "Материнская плата 2", 5, "Основная плата, тип 2"),
        ("TABLE_BUTTON3", "Материнская плата 3", 6, "Основная плата, тип 3"),
        ("WIRE_BLOCK_BUTTON", "Провод", 7, "Блок проводки"),
        ("WIRE_ELECTRICITY_BUTTON","Кабель питания", 8, "Подаёт питание на компоненты"),
    };

    void Awake()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas не найден");
            return;
        }

        TooltipFactory.CreateTooltip(canvas);

        foreach (var data in ButtonData)
        {
            GameObject btn = GameObject.Find(data.objName);
            if (btn == null)
            {
                Debug.LogWarning($"Объект {data.objName} не найден");
                continue;
            }

            if (btn.GetComponent<Button>() == null)
                btn.AddComponent<Button>();

            TooltipTrigger trigger = btn.GetComponent<TooltipTrigger>();
            if (trigger == null)
                trigger = btn.AddComponent<TooltipTrigger>();

            trigger.itemName = data.label;
            trigger.tileid = data.tileid;
            trigger.description = data.desc;
        }
    }
}