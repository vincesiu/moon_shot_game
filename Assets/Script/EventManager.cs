using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour {
    public static EventManager current;
    private void Awake() {
        current = this;
    }

    public event Action onCharacterDeathEvent;
    public void CharacterDeathEvent() {
        if (onCharacterDeathEvent != null) {
            onCharacterDeathEvent();
        }
    }

    public event Action<int> onCharacterDamageEvent;
    public void CharacterDamageEvent(int damage) {
        if (onCharacterDamageEvent != null) {
            onCharacterDamageEvent(damage);
        }
    }


    public event Action<int, int> onEnemyDamageEvent;
    public void EnemyDamageEvent(int damage, int target) {
        if (onEnemyDamageEvent != null){
            onEnemyDamageEvent(damage, target);
        }
    }


    public event Action<int> onEnemyDeathEvent;
    public void EnemyDeathEvent(int target)
    {
        if (onEnemyDeathEvent != null)
        {
            onEnemyDeathEvent(target);
        }
    }


    public event Action onCharacterSpellPickup;
    public void CharacterSpellPickup() {
        if (onCharacterSpellPickup != null) {
            onCharacterSpellPickup();
        }
    }

    public event Action onCharacterWeaponAttachment;
    public void CharacterWeaponAttachment() {
        if (onCharacterWeaponAttachment != null) {
            onCharacterWeaponAttachment();
        }
    }

    public event Action<bool> onEnableUserInput;
    // This is for the main character "dude"
    // True == enable user input
    // False == disable user input
    public void EnableUserInput(bool enabled)
    {
        if (onEnableUserInput != null)
        {
            onEnableUserInput(enabled);
        }
    }

    public event Action<int> onStartRoom;
    public void StartRoom(int roomId)
    {
        if (onStartRoom != null)
        {
            onStartRoom(roomId);
        }
    }

    public event Action onFinishRoom;
    public void FinishRoom()
    {
        if (onFinishRoom != null)
        {
            onFinishRoom();
        }
    }
}
