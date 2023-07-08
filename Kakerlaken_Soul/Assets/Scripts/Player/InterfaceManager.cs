using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    [Header("Ability References")]
    public AbilityUI abilityIconPrefab;
    public Transform abilityIconSlot;

    void Start()
    {
        PlayerManager player = PlayerManager.instance;
        for (int i = 0; i < player.m_Abilities.Length; i++)
        {
            AbilityUI abilityUi = Instantiate(abilityIconPrefab, abilityIconSlot);
            player.m_Abilities[i].OnAbilityUse.AddListener((cooldown) => abilityUi.ShowCoolDown(cooldown));
            abilityUi.SetIcon(player.m_Abilities[i].icon);
        }
    }

   
}
