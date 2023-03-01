using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMechanic : MonoBehaviour
{
    private Rigidbody2D _physics;
    private Vector2 prevVelocity;
    private void Start()
    {
        _physics = GetComponent<Rigidbody2D>();
        _physics.velocity = new Vector2(20, -1).normalized;
        prevVelocity = _physics.velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ContactPoint2D contact = col.contacts[0];
        Vector2 contactNormal = contact.normal;
        FixCollisionVelocity(contactNormal);
        Vector2 newVelocity = Vector2.Reflect(prevVelocity, contactNormal);
        _physics.velocity = newVelocity;
        prevVelocity = _physics.velocity;
    }
    
    // Avoid deadlocks by making sure the ball’s hit velocity is inside the
    // allowed degrees according to our desired game-feel. By 'faking' the
    // hit velocity we later reflect according to the new velocity resulting
    // in reflected angles that maintain the game’s rhythm.
    private void FixCollisionVelocity (Vector2 contactNormal)
    {
        // Angle between the surface normal and the inverse of the ball
        // velocity. We use the inverse since the regular direction is beyond
        // the 90 degree range we operate inside.
        float angleDeg = Vector2.SignedAngle(contactNormal, -prevVelocity);
        float absDeg = Mathf.Abs(angleDeg);
        // >15 to avoid a deadlock with the parallel wall.
        // <70 to avoid a deadlock between the perpendicular walls.
        float clampedDeg = Mathf.Clamp(absDeg, 15, 70);
        // Nothing to fix...
        if ( Mathf.Approximately(absDeg, clampedDeg) )
            return;
        // Compute the desired angle by compensating the normal’s degree
        float signedRad = clampedDeg * Mathf.Sign(angleDeg) * Mathf.Deg2Rad;
        float normalRad = Mathf.Atan2(contactNormal.y, contactNormal.x);
        float newRad = normalRad + signedRad;
        // The initial vector is normalized so we multiple by the original
        // velocity magnitude to maintain speed. We use negative magnitude to
        // compensate for inverting the velocity earlier.
        Vector2 newDir = new Vector2(Mathf.Cos(newRad), Mathf.Sin(newRad));
        float magnitude = prevVelocity.magnitude; // vector’s length
        prevVelocity = newDir * -magnitude;
    }
    
    
    
    
    
}
