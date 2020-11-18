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

    public event Action onCharacterSpellPickup;
    public void CharacterSpellPickup() {
        if (onCharacterSpellPickup != null) {
            onCharacterSpellPickup();
        }
    }

    public event Action<bool> onCharacterWeaponAttachment;
    public void CharacterWeaponAttachment(bool attached) {
        if (onCharacterWeaponAttachment != null) {
            onCharacterWeaponAttachment(attached);
        }
    }
}
