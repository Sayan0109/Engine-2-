using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [Header("Selected Abilities (choose 2)")]
    public AbilityData abilitySlot1;
    public AbilityData abilitySlot2;

    private PlayerAbilityExecutor executor;

    private float cooldown1 = 0;
    private float cooldown2 = 0;

    void Start()
    {
        executor = GetComponent<PlayerAbilityExecutor>();
    }

    void Update()
    {
        cooldown1 -= Time.deltaTime;
        cooldown2 -= Time.deltaTime;

        // ПК управление
        if (Input.GetKeyDown(KeyCode.Alpha1))
            TryUseAbility(abilitySlot1, ref cooldown1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            TryUseAbility(abilitySlot2, ref cooldown2);
    }

    // Вызывают UI-кнопки (для телефона)
    public void UseAbilitySlot1() =>
        TryUseAbility(abilitySlot1, ref cooldown1);

    public void UseAbilitySlot2() =>
        TryUseAbility(abilitySlot2, ref cooldown2);

    private void TryUseAbility(AbilityData ability, ref float cd)
    {
        if (ability == null) return;
        if (cd > 0) return;

        executor.ExecuteAbility(ability);
        cd = ability.cooldown;
    }
}
