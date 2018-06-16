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

    [SerializeField] private GameObject sonarTrigger;

    public float timeToHide = 3f;

    Coroutine hideCoroutine;
    // Use this for initialization
    void Start()
    {
        this.targetRigidbody = GetComponentInChildren<Rigidbody>();
        this.compass = GameObject.FindGameObjectWithTag("Compass").GetComponent<Compass>();
        if (this.compass != null)
        {
            this.compass.AddTargetToBeTracked(this.gameObject);
        }
        this.initialPosition = this.transform.position;
        this.desiredPosition = this.initialPosition + new Vector3(0, desiredHeightOffset, 0);
        this.audioController = GetComponent<AudioController>();
        StartCoroutine(MoveToOverTime(this.initialPosition, this.desiredPosition, this.secondsToMove));
    }

    IEnumerator HideAfterTime(float time) {
        yield return new WaitForSeconds(time);
        this.broken = true;
        AnimateDestruction();
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
        StartUpActions();
        yield return null;
    }

    public void StartUpActions()
    {
        this.hideCoroutine = StartCoroutine(HideAfterTime(timeToHide));
        this.audioController.Play();
        MoveTarget moveTarget = GetComponent<MoveTarget>();
        if (moveTarget != null)
        {
            moveTarget.enabled = true;
        }
    }

    public abstract void DealDamage(float damage);

    public void DisableColliders()
    {
        foreach (Collider collider in this.colliderContainer.GetComponents<Collider>())
        {
            collider.enabled = false;
        }
    }

    public void AnimateDestruction()
    {
        if(this.compass != null) {
            this.compass.DeleteTarget(this.gameObject);
        }
        if(hideCoroutine != null) {
            StopCoroutine(hideCoroutine);
        }
        if (sonarTrigger != null)
        {
            sonarTrigger.GetComponent<NearbySonarElement>().sonarElement.StopSound();
            Destroy(sonarTrigger);
        }
        DisableColliders();

        Light light = GetComponentInChildren<Light>();
        light.enabled = false;

        MoveTarget moveTarget = GetComponent<MoveTarget>();
        if (moveTarget != null)
        {
            moveTarget.enabled = false;
        }

        this.targetRigidbody.isKinematic = false;
        this.targetRigidbody.useGravity = true;
        this.targetRigidbody.AddRelativeForce(Vector3.right * 5f, ForceMode.Impulse);

        // Destroy after 3 seconds.
        StartCoroutine((this.gameObject.AddComponent(typeof(DestructionScheduler)) as DestructionScheduler).DestroyAfterTime(3f));

    }
}
