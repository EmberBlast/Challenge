using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    void GetDamage(float damage);
    void GetHealed(float healAmmount);
    Action onDamage { get; set; }
    Action onHeal { get; set; }
    Action onDeath { get; set; }
}
