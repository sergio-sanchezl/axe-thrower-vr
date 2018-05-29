using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class TargetScript : MonoBehaviour, IDamageable
{

    public float desiredHeightOffset;
    public float secondsToMove = 1f;

    Vector3 initialPosition;
    Vector3 desiredPosition;

    public Transform player;

    AudioController audioController;

    protected bool broken = false;

    protected Compass compass;

    // reference to the gameobject that contains this target's colliders.
    [SerializeField] protected GameObject colliderContainer;

    private Rigidbody targetRigidbody;
    // Use this for initialization
    void Start()
    {
        this.targetRigidbody = GetComponentInChildren<Rigidbody>();
        this.compass = GameObject.FindGameObjectWithTag("Compass").GetComponent<Compass>();
        this.compass.AddTargetToBeTracked(this.gameObject);
        this.initialPosition = this.transform.position;
        this.desiredPosition = this.initialPosition + new Vector3(0, desiredHeightOffset, 0);
        this.audioController = GetComponent<AudioController>();
        StartCoroutine(MoveToOverTime(this.initialPosition, this.desiredPosition, this.secondsToMove));
    }

    IEnumerator MoveToOverTime(Vector3 initialPosition, Vector3 desiredPosition, float secondsToMove)
    {
        // move to a position while looking at the player, in an animated way.
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / secondsToMove;
            this.transform.position = Vector3.Lerp(initialPosition, desiredPosition, t);
            this.transform.LookAt(this.player);
            yield return null;
        }
        this.audioController.Play();
        yield return null;
    }


    public abstract void DealDamage(float damage);

    public void DisableColliders() {
        foreach (Collider collider in this.colliderContainer.GetComponents<Collider>()) {
            collider.enabled = false;
        }
    }

    public void AnimateDestruction() {
        DisableColliders();
        this.targetRigidbody.isKinematic = false;
        this.targetRigidbody.useGravity = true;
        this.targetRigidbody.AddRelativeForce(Vector3.right * 5f, ForceMode.Impulse);
        
        // Destroy after 3 seconds.
        StartCoroutine((this.gameObject.AddComponent(typeof(DestructionScheduler)) as DestructionScheduler).DestroyAfterTime(3f));
        
    }
}
