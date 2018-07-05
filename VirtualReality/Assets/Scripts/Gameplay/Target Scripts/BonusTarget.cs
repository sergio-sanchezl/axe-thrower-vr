using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTarget : TargetScript {

	public BonusManager bonusManager;
	public override void DealDamage(float damage)
    {
        if (!base.broken)
        {
            base.DealDamage(damage);
            bonusManager.ExecuteRandomBonus();
            this.broken = true;
            AnimateDestruction();
            // DisableColliders();
            //Destroy(this.gameObject);
        }
    }
}
