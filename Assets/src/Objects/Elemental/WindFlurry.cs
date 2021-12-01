using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WindFlurry : MonoBehaviour {
    
    /// <summary>
    /// Layer that the flurry will collide with
    /// </summary>
    public LayerMask bounceLayer;

    /// <summary>
    /// Force the flurry will impulse a rigidbody on the x axis
    /// </summary>
    public float horizontalImpulse;
    
    /// <summary>
    /// Force the flurry will impulse a rigidbody on the y axis
    /// </summary>
    public float verticalImpulse;

    /// <summary>
    /// Distance that the flurry's trajectory will travel
    /// </summary>
    public float distance;

    /// <summary>
    /// Duration the impulse will last in the affected entity
    /// </summary>
    public float duration;

    /// <summary>
    /// Intensity of the slowdown effect applied
    /// </summary>
    public float slowdownIntensity;

    /// <summary>
    /// Speed at which the flurry moves
    /// </summary>
    public float speed;

    private Vector3 _direction;

    private SlowdownEffect _slowdownEffect;

    /// <summary>
    /// Entities currently being impulsed
    /// </summary>
    private HashSet<Affectable> impulsedEntities;

    /// <summary>
    /// Maximum bounces the flurry can hit before stopping the bounce check
    /// </summary>
    private float _maximumBounces = 7;

    /// <summary>
    /// Epsilon that controls position comparisons
    /// </summary>
    private float _kEpsilon = 0.15f;

    // Start is called before the first frame update
    void Start() {
        _slowdownEffect = new SlowdownEffect(slowdownIntensity, duration);
        _direction = transform.right;
        impulsedEntities = new HashSet<Affectable>();
        StartCoroutine(FollowTrajectory(GetTrajectory(transform.position, _direction, distance)));
        Destroy(gameObject, 3f);
    }

    /// <summary>
    /// Flurry applies an impulse once to an entity
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        Affectable a = other.GetComponent<Affectable>();
        if (a) {
            if (!impulsedEntities.Contains(a)) {
                impulsedEntities.Add(a);
                _slowdownEffect.Apply(a);
                // The force of the flurry is multiplied if the affected entity has a negative crowd control resistance
                float resistance = a.GetResistanceTo(Resistance.CROWD_CONTROL);
                float forceMultiplier = (resistance<0) ? Mathf.Lerp(1f, 2f,-resistance/100f) : 1f;
                a.StartCoroutine(
                    a.controller.ApplyImpulse(Vector3.Scale(transform.right , 
                            new Vector2(forceMultiplier * horizontalImpulse, forceMultiplier * verticalImpulse)), 
                        duration, 0f));
            }
        }
    }

    public IEnumerator FollowTrajectory(List<Vector3> trajectory) {
        Vector3 currentPosition, nextPosition, currentDirection;
        for (int i = 0; i < trajectory.Count-1; i++) {
            currentPosition = trajectory[i];
            nextPosition = trajectory[i+1];
            currentDirection = Vector3.Normalize(nextPosition - currentPosition);
            transform.right = currentDirection;
            while (Vector3.Distance(transform.position, nextPosition)>_kEpsilon) {
                transform.position += speed * Time.fixedDeltaTime * currentDirection;
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public List<Vector3> GetTrajectory(Vector2 startingPoint, Vector2 direction, float distance) {
        List<Vector3> result = new List<Vector3>();
        result.Add(startingPoint);
        Vector2 currentContactPoint = startingPoint, currentDirection = direction;
        float currentDistance = 0, remainingDistance = distance, traveledDistance;
        int currentHits = 0;
        while (currentDistance<distance && currentHits<_maximumBounces) {
            // First, new point is moved a little bit so that it doesn't detect the same collider
            currentContactPoint += currentDirection * 0.01f;
            RaycastHit2D hit = Physics2D.Raycast(currentContactPoint, currentDirection, remainingDistance, bounceLayer);
            if (hit.collider) {
                traveledDistance = Vector2.Distance(currentContactPoint,hit.point);
                result.Add(new Vector3(hit.point.x,hit.point.y, transform.position.z));
                // Then, new direction and contact point are updated
                currentDirection = Vector2.Reflect(currentDirection, hit.normal);
                currentContactPoint = hit.point;
                currentDistance += traveledDistance;
                remainingDistance -= traveledDistance;
                currentHits++;
            }
            else {
                result.Add(currentContactPoint + currentDirection * remainingDistance);
                currentDistance += remainingDistance+20;
            }
        }
        return result;
    }
}
