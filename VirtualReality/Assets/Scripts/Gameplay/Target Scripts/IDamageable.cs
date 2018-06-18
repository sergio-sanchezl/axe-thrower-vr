using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface that must be implemented by gameobject's scripts that can be damageable.
public interface IDamageable {
    void DealDamage(float damage);
}
