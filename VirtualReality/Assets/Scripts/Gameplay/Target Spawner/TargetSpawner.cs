﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script that spawns targets with a rate of time around the player in an arc specified
// by angles.
public class TargetSpawner : MonoBehaviour
{
    // reference to the player
    public GameObject player;
    // if active, it will spawn entities. if not, it won't.
    public bool active = true;

    // radius of the arc
    public float radius = 10f;

    // angle of the arc
    public float spawnAngle = 180;

    [SerializeField] private float lowerAngle;
    [SerializeField] private float higherAngle;
    // time to wait  until spawning the next entity.
    

    // entities to spawn. will be randomly picked, for now.
    public EntityWithProbability[] objectsToSpawn;

    // // unused.
    // public AnimationCurve positionCurve;

    // to the edges of the arc, the enemies will appear at a lower height.
    public float minHeight = 3f;
    // to the center of the arc, the enemies will appear at a higher height.
    public float maxHeight = 8f;
    // Use this for initialization

    // Reference to the scoreManager. Will be passed to created entities.
    public ScoreManager scoreManager;

    // Reference to the bonusManager. Will be passed to created entities.
    public BonusManager bonusManager;

    public int difficultyFactor = 0; // will go from 0 to 10.
    public int chanceOfMovingTargets = 0; // will increase by 10 for every level.
    public float maximumMovingAngle = 0; // will increase by 7 every level.
    public float movingTargetSpeed = 0;
    public float timeBetweenSpawns;
    public float timeUntilTargetDisappears;
    // begin the spawn loop.
    void Start()
    {
        this.scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        PrepareProbabilities();
        StartCoroutine(SpawnLoop());
        this.spawnAngle = (PlayerPrefs.GetInt("reduced_game_area", 0) == 1) ? 90f : this.spawnAngle;
    }

    // loop. wait for the time between spawns, and then spawn the entity.
    IEnumerator SpawnLoop()
    {
        while (active)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEntity();
        }

    }

    void PrepareProbabilities()
    {
        int probability = 0;
        foreach (EntityWithProbability item in this.objectsToSpawn)
        {
            item.SetMinimumProbability(probability);
            probability += item.GetProbabilityPercentage();
            item.SetMaximumProbability(probability);
        }
    }
    Object GetGameObjectBasedOnProbability()
    {
        // will yield a number between 0 and 99
        int randomPercentage = Random.Range(0,100);
        foreach (EntityWithProbability item in this.objectsToSpawn)
        {
            // if the random percentage is between the probability brackets of the accumulated probability of the
            // object, then return this object.
            if(randomPercentage >= item.GetMinimumProbability() && randomPercentage < item.GetMaximumProbability()) {
                return item.GetEntity();
            }
        }
        // if probability parameters are not wrong, we will never reach this line.
        // if we do, just return the first entity to avoid a massive failure (sorry).
        return this.objectsToSpawn[0].GetEntity();
    }
    void SpawnRandomEntity()
    {
        // we randomly get an entity from the array, and instantiate it in the scene.
        GameObject spawnedObject = Instantiate(GetGameObjectBasedOnProbability()) as GameObject;
        // compute a angle offset. this will ensure that the arc's center is always facing the player's initial orientation.
        float angleOffset = (180 - this.spawnAngle) / 2f;
        // The lower angle shall be at the offset + 0. defines the left side of the arc.
        lowerAngle = angleOffset;
        // The higher angle shall be at the offset + angle. defines the right side of the arc.
        higherAngle = this.spawnAngle + angleOffset;
        // chooses a random angle between the lower and the higher, i.e. a random point in the arc.
        float angle = Random.Range(lowerAngle, higherAngle);
        // does math magic to translate the angle to a point of a radius 1 circunference.
        Vector3 randomCircle = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
        // multiplies the coordinate of the radius 1 circunference by the radius to obtain the coordinate in the actual circunference.
        Vector3 worldPos = transform.TransformPoint(randomCircle * radius);

        // set the gameobject (entity) position and look at the player.
        spawnedObject.transform.position = worldPos;
        spawnedObject.transform.LookAt(player.transform);

        AttachMoveComponent(spawnedObject, angle);
        // we get the Target Script. we know all entities are targets and so, contain this script.
        TargetScript ts = spawnedObject.GetComponent<TargetScript>();
        if (ts != null)
        {
            if (ts.GetType() == typeof(ScoreTarget))
            {
                ((ScoreTarget)ts).scoreManager = this.scoreManager;
            }
            if (ts.GetType() == typeof(BonusTarget))
            {
                ((BonusTarget)ts).bonusManager = this.bonusManager;
            }

            ts.timeToHide = 90f;
            ts.player = this.player.transform;
            float halfAngle = (lowerAngle + higherAngle) / 2f;
            //first sector. lerp from A0 (lower angle) to A1 (higher angle) (A0 -> min height, A1 -> max height);
            if (angle < halfAngle)
            {
                ts.desiredHeightOffset = Mathf.Lerp(minHeight, maxHeight, Mathf.InverseLerp(lowerAngle, halfAngle, angle)); // the height will depend on the angle.
            }
            else
            {
                // second sector. lerp from A1 (higher angle) to A0 (lower angle).
                ts.desiredHeightOffset = Mathf.Lerp(maxHeight, minHeight, Mathf.InverseLerp(halfAngle, higherAngle, angle)); // the height will depend on the angle.
            }

        }
    }

    // attaches the MoveTarget component to the GameObject, configuring its angle of movement and speed.
    private void AttachMoveComponent(GameObject obj, float angle) {
        MoveTarget moveTarget = obj.AddComponent(typeof (MoveTarget)) as MoveTarget;
        // script must be disabled. the target itself will enable it after appearing.
        moveTarget.enabled = false;

        float totalAngle = 90f;
        float startAngle;
        float endAngle;
        if(angle - lowerAngle < (totalAngle / 2) && higherAngle - angle < (totalAngle / 2)) {
            // the total angle is constrained by both limit angles.
            // set the angles to be as big as the difference between the limits and its actual angle.
            endAngle = angle - lowerAngle;
            startAngle = higherAngle - angle;
        } else if (angle - lowerAngle < (totalAngle / 2)) {
            // the total angle is only constrained by the lower angle.
            // set the start angle to be the difference between the angle and the lower angle, and add the remaining
            // to the other half of the total angle for the end angle.
            endAngle = angle - lowerAngle;
            startAngle = ((totalAngle / 2) - (angle - lowerAngle)) + (totalAngle / 2);
        } else if (higherAngle - angle < (totalAngle / 2)) {
            // the total angle is only constrained by the higher angle.
            // set the end angle to be the difference between the angle and the higher angle, and add the remaining
            // to the other half of the total angle for the start angle.
            endAngle = ((totalAngle / 2) - (higherAngle - angle)) + (totalAngle / 2);
            startAngle = higherAngle - angle;
        } else {
            // the total angle is not constrained by any angle.
            // simply set the half of the total angle into the start and end angles of the script.
            startAngle = totalAngle / 2;
            endAngle = totalAngle / 2;
        }

        moveTarget.startAngle = startAngle;
        moveTarget.endAngle = endAngle;

        Debug.Log("A: " + angle + " LA: " + lowerAngle + " HA: " + higherAngle + " TA: " + totalAngle + " SA: " + startAngle + " EA: " + endAngle);
        moveTarget.secondsMoving = 5;
    }
    // we draw the arc and the lines that represent it as gizmos in the editor, for debugging purposes.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.transform.position, this.radius);
        Gizmos.DrawLine(this.transform.position + new Vector3(this.radius, 0, 0), (this.transform.position - new Vector3(this.radius, 0, 0)));

        float angleOffset = (180 - this.spawnAngle) / 2f;
        float lowerAngle = angleOffset;
        float higherAngle = this.spawnAngle + angleOffset;
        Vector3 randomCircle = new Vector3(Mathf.Cos(lowerAngle * Mathf.Deg2Rad), 0, Mathf.Sin(lowerAngle * Mathf.Deg2Rad));
        Vector3 worldPos = transform.TransformPoint(randomCircle * radius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, worldPos);

        randomCircle = new Vector3(Mathf.Cos(higherAngle * Mathf.Deg2Rad), 0, Mathf.Sin(higherAngle * Mathf.Deg2Rad));
        worldPos = transform.TransformPoint(randomCircle * radius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, worldPos);
    }

    public void IncreaseDifficulty() {
        if(difficultyFactor == 10) {
            return;
        } else {
            difficultyFactor++;
        }
    }
}

