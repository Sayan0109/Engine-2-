using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityExecutor : MonoBehaviour
{
    public GameObject shieldVisual;
    public Renderer playerRenderer;

    private bool isInvisible = false;
    private Rigidbody rb;

    // Список ВСЕХ трейлов на объекте игрока
    private TrailRenderer[] trails;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Ищем все трейлы на игроке и дочерних объектах
        trails = GetComponentsInChildren<TrailRenderer>(true);
    }

    public void ExecuteAbility(AbilityData ability)
    {
        switch (ability.abilityType)
        {
            case AbilityType.Shield:
                StartCoroutine(DoShield(ability.duration));
                break;

            case AbilityType.Invisibility:
                StartCoroutine(DoInvisibility(ability.duration));
                break;

            case AbilityType.HyperJump:
                DoHyperJump(ability.jumpDistance);
                break;
        }
    }

    private IEnumerator DoShield(float duration)
    {
        shieldVisual.SetActive(true);
        yield return new WaitForSeconds(duration);
        shieldVisual.SetActive(false);
    }

    private IEnumerator DoInvisibility(float duration)
    {
        isInvisible = true;
        playerRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        playerRenderer.enabled = true;
        isInvisible = false;
    }

    private void DoHyperJump(float distance)
    {
        if (rb == null) rb = GetComponent<Rigidbody>();

        // Остановить движение
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 🔥 ОЧИСТКА ВСЕХ трейлов
        if (trails != null)
        {
            foreach (var t in trails)
            {
                if (t != null)
                    t.Clear();
            }
        }

        // Прыжок
        Vector3 target = transform.position + transform.forward * distance;
        rb.MovePosition(target);
    }
}
