using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarRaycaster : MonoBehaviour
{

    [SerializeField] private float distance = 100;

    [SerializeField] private bool sonarEnabled;
    // public GameObject previouslyCollidedGameObject = null;
    // public bool previouslyHittingTarget;

    List<GameObjectAndDistance> previouslyCollidedGameObjects = new List<GameObjectAndDistance>();
    // Use this for initialization
    void Start()
    {
        this.sonarEnabled = PlayerPrefs.GetInt("sonar", 0) == 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(sonarEnabled) {
            CastRay();
        }
        
    }

    public void CastRay()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(this.transform.position, this.transform.TransformDirection(Vector3.forward), distance, LayerMask.GetMask("Sonar"));

        List<GameObjectAndDistance> collidedGameObjects = new List<GameObjectAndDistance>();
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Vector3 hitPoint = hit.point;
            Vector3 centerPoint = hit.transform.position;
            float distance = Vector3.Distance(centerPoint, hitPoint);

            float colliderRadius = hit.transform.localScale.x;
            float relativeValue = Mathf.Lerp(1, 0.25f, Mathf.InverseLerp(1, colliderRadius, distance));

            collidedGameObjects.Add(new GameObjectAndDistance(hit.transform.gameObject, distance, relativeValue));
        }

        foreach (GameObjectAndDistance item in collidedGameObjects)
        {
            SonarElement sonarElement = item.gameObject.GetComponent<NearbySonarElement>().sonarElement;
            if (previouslyCollidedGameObjects.Contains(item))
            {
                // if we were already colliding before with this gameobject, then just change the pitch & volume.
                if (sonarElement != null)
                {
                    sonarElement.ChangePitchAndVolume(item.relativeValue);
                }

                previouslyCollidedGameObjects.Remove(item);
            }
            else
            {
                // if this element is new, then begin playing its sound.
                if (sonarElement != null)
                {
                    sonarElement.PlaySound(item.relativeValue);
                }

            }
        }

        // this will contain all gameobjects that were in the previous frame but not anymore.
        // stop their sound.
        foreach (GameObjectAndDistance item in previouslyCollidedGameObjects)
        {
            if (item.gameObject != null)
            {
                SonarElement sonarElement = item.gameObject.GetComponent<NearbySonarElement>().sonarElement;
                if (sonarElement != null)
                {
                    sonarElement.StopSound();
                }
            }


        }

        // now switch this frame's collided game objects for the next frame's previous game objects.
        previouslyCollidedGameObjects = new List<GameObjectAndDistance>();
        previouslyCollidedGameObjects.AddRange(collidedGameObjects);

        // // If the raycast is hitting ANY target directly instead of just the trigger around it, 
        // // DON'T PLAY ANY SOUNDS, since the sounds are already sent by the target itself.
        // bool hittingTarget = false;
        // GameObjectAndDistance closestDistance = new GameObjectAndDistance();
        // closestDistance.gameObject = null;
        // closestDistance.distance = Mathf.Infinity;
        // hits = Physics.RaycastAll(this.transform.position, this.transform.TransformDirection(Vector3.forward), distance, LayerMask.GetMask("Sonar"));
        // if (hits.Length > 0)
        // {
        //     for (int i = 0; i < hits.Length; i++)
        //     {
        //         RaycastHit hit = hits[i];
        //         Vector3 hitPoint = hit.point;
        //         Vector3 centerPoint = hit.transform.position;
        //         float distance = Vector3.Distance(centerPoint, hitPoint);
        //         if(closestDistance.distance > distance) {
        // 			closestDistance.distance = distance;
        //             closestDistance.gameObject = hit.transform.gameObject;
        // 		}
        //         if (distance <= 1)
        //         {
        //             hittingTarget = true;
        //             previouslyHittingTarget = true;
        //             break;
        //         }
        //     }

        // 	if(!hittingTarget) {
        //         float colliderRadius = closestDistance.gameObject.transform.localScale.x;
        // 		float relativeValue = Mathf.Lerp(1, 0.25f, Mathf.InverseLerp(1, colliderRadius, closestDistance.distance));
        //         SonarElement sonarElement = closestDistance.gameObject.GetComponent<NearbySonarElement>().sonarElement;
        //         if(closestDistance.gameObject == previouslyCollidedGameObject) {
        //             if(previouslyHittingTarget) {
        //                 // if we were hitting the target previously, that means we must play the sound again.
        //                 sonarElement.PlaySound(relativeValue);
        //             }
        //             // if we are still on the same collider, then just change pitch and volume.
        //             sonarElement.ChangePitchAndVolume(relativeValue);
        //         } else {
        //             // if not, play the new collider's sound, and update the reference to the collider.
        //             previouslyCollidedGameObject = closestDistance.gameObject;
        //             sonarElement.PlaySound(relativeValue);
        //         }

        //         // if we are here and we are not hitting the target, that means for the future
        //         // we must know that we didn't hit the target. mark it as false.
        //         previouslyHittingTarget = false;
        //         // Debug.Log("closest distance: " + closestDistance.distance + " ||| relative value: " + relativeValue + " ||| previously hitting target: " + previouslyHittingTarget);
        // 	} else {
        //         if(closestDistance.gameObject != previouslyCollidedGameObject) {
        //             // if we are not still on the same collider, update the reference to the collider.
        //             previouslyCollidedGameObject = closestDistance.gameObject;
        //         }
        // 		// Debug.Log("hitting target.");
        // 	}
        // } else {
        //     previouslyHittingTarget = false;
        //     // clear the reference to the previously collided game object.
        //     // after pausing it sonar sound.
        //     if(previouslyCollidedGameObject != null) {
        //         previouslyCollidedGameObject.GetComponent<NearbySonarElement>().sonarElement.StopSound();
        //     }
        //     previouslyCollidedGameObject = null;
        // }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(Vector3.forward) * distance);
    }

}

class GameObjectAndDistance
{
    public GameObject gameObject;
    public float distance;

    public float relativeValue;
    public GameObjectAndDistance(GameObject gameObject, float distance, float relativeValue)
    {
        this.gameObject = gameObject;
        this.distance = distance;
        this.relativeValue = relativeValue;
    }

    public override bool Equals(object obj)
    {
        GameObjectAndDistance castedObject = obj as GameObjectAndDistance;
        if (castedObject == null)
        {
            return false;
        }
        else
        {
            return gameObject == castedObject.gameObject;
        }
    }

    public override int GetHashCode()
    {
        return this.gameObject.GetHashCode();
    }
}