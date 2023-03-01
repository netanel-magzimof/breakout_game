using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    public BatEdgesCollider leftCollider;
    public BatEdgesCollider rightCollider;
    private SpriteRenderer spriteRenderer;
    public Bat bat;
    public int STARTINGSPEED, MIDDLESPEED, FINALSPEED;
    private int curSpeed;
    private Vector2 DEFAULT_POSITION = new(0, -3.6F);
    private bool isMoving;
    private Rigidbody2D physics;
    private Vector2 prevVelocity;
    private int batHitsCounter;
    private TrailRenderer trailRenderer;
    private bool isHitMiddleRow;

    private void Start()
    {
        trailRenderer = gameObject.GetComponent<TrailRenderer>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        ResetParameters();
    }

    private void ResetParameters()
    {
        isMoving = false;
        isHitMiddleRow = false;
        curSpeed = STARTINGSPEED;
        physics.position = DEFAULT_POSITION;
        isHitMiddleRow = false;
        spriteRenderer.color = Color.white;
        trailRenderer.enabled = false;
        batHitsCounter = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!isMoving) return;
        if (didHitMiddleRow(col))
        {
            isHitMiddleRow = true;
            handleSpeed(col);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            batHitsCounter++;
            handleSpeed(col);
        }

        var contact = col.contacts[0];
        var contactNormal = contact.normal;
        FixCollisionVelocity(contactNormal);
        Vector2 newVelocity;
        if (Mathf.Approximately(prevVelocity.x, 0))
            newVelocity = new Vector2(3, 6).normalized * curSpeed;
        else if (leftCollider.IsBallTriggered() && prevVelocity.x >= 0)
            newVelocity = new Vector2(-3, 6).normalized * curSpeed;
        else if (rightCollider.IsBallTriggered() && prevVelocity.x <= 0)
            newVelocity = new Vector2(3, 6).normalized * curSpeed;
        else
            newVelocity = Vector2.Reflect(prevVelocity, contactNormal).normalized * curSpeed;
        physics.velocity = newVelocity.normalized * curSpeed;
        physics.angularVelocity = curSpeed * 50;
        prevVelocity = physics.velocity.normalized * curSpeed;
    }

    private void handleSpeed(Collision2D col)
    {
        if (isHitMiddleRow || batHitsCounter >= 8)
        {
            trailRenderer.enabled = true;
            spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
            curSpeed = FINALSPEED;
        }
        else if (batHitsCounter >= 4)
        {
            curSpeed = MIDDLESPEED;
        }
    }

    private bool didHitMiddleRow(Collision2D col)
    {
        return col.gameObject.CompareTag("LVL 2");
    }

    // Avoid deadlocks by making sure the ball’s hit velocity is inside the
    // allowed degrees according to our desired game-feel. By 'faking' the
    // hit velocity we later reflect according to the new velocity resulting
    // in reflected angles that maintain the game’s rhythm.
    private void FixCollisionVelocity(Vector2 contactNormal)
    {
        // Angle between the surface normal and the inverse of the ball
        // velocity. We use the inverse since the regular direction is beyond
        // the 90 degree range we operate inside.
        var angleDeg = Vector2.SignedAngle(contactNormal, -prevVelocity);
        var absDeg = Mathf.Abs(angleDeg);
        // >15 to avoid a deadlock with the parallel wall.
        // <70 to avoid a deadlock between the perpendicular walls.
        var clampedDeg = Mathf.Clamp(absDeg, 15, 70);
        // Nothing to fix...
        if (Mathf.Approximately(absDeg, clampedDeg))
            return;
        // Compute the desired angle by compensating the normal’s degree
        var signedRad = clampedDeg * Mathf.Sign(angleDeg) * Mathf.Deg2Rad;
        var normalRad = Mathf.Atan2(contactNormal.y, contactNormal.x);
        var newRad = normalRad + signedRad;
        // The initial vector is normalized so we multiple by the original
        // velocity magnitude to maintain speed. We use negative magnitude to
        // compensate for inverting the velocity earlier.
        var newDir = new Vector2(Mathf.Cos(newRad), Mathf.Sin(newRad));
        var magnitude = prevVelocity.magnitude; // vector’s length
        prevVelocity = newDir * -magnitude;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            bat.ActivateBat(true);
            float x = 1;
            if (Random.value >= 0.5f) x = -1;
            physics.velocity = new Vector2(Random.Range(1, 5) * x, Random.Range(1, 5)).normalized * curSpeed;
            physics.angularVelocity = curSpeed * 50;
            prevVelocity = physics.velocity;
            isMoving = true;
        }
    }

    public void OutOfBounds()
    {
        physics.velocity = Vector2.zero;
        physics.angularVelocity = 0;
        isMoving = false;
        physics.position = DEFAULT_POSITION;
    }

    public void Restart()
    {
        gameObject.SetActive(true);
        OutOfBounds();
        ResetParameters();
    }

    private void FixedUpdate()
    {
        physics.velocity = physics.velocity.normalized * curSpeed;
    }
}