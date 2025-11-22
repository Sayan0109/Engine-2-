using UnityEngine;
using UnityEngine.UI;

public class UIAbilityButton : MonoBehaviour
{
    public Button button;
    public AbilityController controller;
    public bool isSlot1;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (isSlot1)
                controller.UseAbilitySlot1();
            else
                controller.UseAbilitySlot2();
        });
    }
}
