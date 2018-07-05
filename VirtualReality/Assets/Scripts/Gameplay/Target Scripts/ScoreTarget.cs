using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : TargetScript
{

	public ScoreManager scoreManager;

    // How many points should this target yield when broken.
    public int scoreYield;
    public override void DealDamage(float damage)
    {
        if (!base.broken)
        {
            base.DealDamage(damage);
            scoreManager.AddPoints(this.scoreYield);
            this.broken = true;
            AnimateDestruction();
        
            // DisableColliders();
            //Destroy(this.gameObject);
        }
    }
}
