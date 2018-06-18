using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

/*
class that represents an entity that can be spawned with a probability.
the minimum probability and maximum probability will always have the values
that ensures that maximum-minimum = probabilityPercentage.
 */
public class EntityWithProbability {
	
	// Entity represented by this object, i.e. the entity to spawn.
	[SerializeField] private Object entity;

	// a probability like 20% would be 20.
	[SerializeField] private int probabilityPercentage;

	// minimum bracket in the percentage.
	private int minimumProbability;

	// maxmimum bracket in the percentage.
	private int maximumProbability;

	public Object GetEntity() {
		return this.entity;
	}
	public int GetProbabilityPercentage() {
		return this.probabilityPercentage;
	}
	public int GetMinimumProbability() {
		return this.minimumProbability;
	}
	public int GetMaximumProbability() {
		return this.maximumProbability;
	}

	public void SetEntity(Object entity) {
		this.entity = entity;
	}
	public void SetProbabilityPercentage(int probabilityPercentage) {
		this.probabilityPercentage = probabilityPercentage;
	}
	public void SetMinimumProbability(int minimumProbability) {
		this.minimumProbability = minimumProbability;
	}
	public void SetMaximumProbability(int maximumProbability) {
		this.maximumProbability = maximumProbability;
	}


}
