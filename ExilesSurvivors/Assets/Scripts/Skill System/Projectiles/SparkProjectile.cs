using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;



public class SparkProjectile : Projectile
{
    public GameObject SparkHitEffectPrefab; // Add this property

    public float InitialDirectionChangeTime = 0.5f;
    public float InitialDirectionChangeTimeVariance = 0.5f; // Time between direction changes

    [Header("Direction Settings")]
    public float maxAngleDeviation = 90f; // Maximum angle change per direction shift
    public float minDirectionChangeInterval = 0.3f;
    public float maxDirectionChangeInterval = 0.7f;
    public float transitionDuration = 0.2f; // Time to smoothly change direction

    private Vector2 currentDirection;
    private Coroutine directionRoutine;

    public override void Initialize(Vector2 direction, float speed, float damage)
    {
        gameObject.transform.SetParent(GameObject.Find("Projectiles").transform);
        this.direction = direction;
        currentDirection = direction.normalized;
        Speed = speed;
        Damage = damage;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = currentDirection * Speed;

        directionRoutine = StartCoroutine(DirectionChangeRoutine());
        ActivateOnCreateEvents();
    }

    private IEnumerator DirectionChangeRoutine()
    {
        // Initial random delay
        yield return new WaitForSeconds(UnityEngine.Random.Range(InitialDirectionChangeTime, InitialDirectionChangeTime + InitialDirectionChangeTimeVariance));

        while (true)
        {
            // Calculate constrained new direction
            Vector2 newDirection = GetConstrainedDirection(currentDirection);
            Vector2 startDirection = currentDirection;

            // Smooth transition between directions
            
            float elapsed = 0f;
            while (elapsed < transitionDuration)
            {
                elapsed += Time.deltaTime;
                currentDirection = Vector2.Lerp(startDirection, newDirection, elapsed / transitionDuration);
                rb.velocity = currentDirection * Speed;
                yield return null;
            }
            

            // Wait random interval before next change
            yield return new WaitForSeconds(UnityEngine.Random.Range(minDirectionChangeInterval, maxDirectionChangeInterval));
        }
    }

    private Vector2 GetConstrainedDirection(Vector2 previousDirection)
    {
        // Generate random angle within deviation limits
        float randomAngle = UnityEngine.Random.Range(-maxAngleDeviation, maxAngleDeviation);
        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);
        return rotation * previousDirection.normalized;
    }

    protected override void OnDestroy()
    {
        // Stop coroutine when destroyed
        if (directionRoutine != null) StopCoroutine(directionRoutine);
        base.OnDestroy();
    }

    protected override void HandleDefaultHit(Collider2D other)
    {
        if (other.CompareTag("Terrain")) {


            StopCoroutine(directionRoutine);

            // Convert transform.position to Vector2 to avoid type mismatch
            Vector2 position2D = transform.position;

            // Use a raycast to detect the surface normal at the impact point
            RaycastHit2D hit = Physics2D.Raycast(position2D - rb.velocity.normalized * 0.1f, rb.velocity.normalized, 0.2f);
            if (hit.collider != null)
            {
                Vector2 normal = hit.normal;

                // Reflect the velocity correctly off the surface
                Vector2 reflectedDirection = Vector2.Reflect(rb.velocity, normal).normalized;

                // Apply the new velocity while maintaining speed
                rb.velocity = reflectedDirection * Speed;
                currentDirection = reflectedDirection;
            }

            Vector2 newDirection = GetConstrainedDirection(currentDirection);
            currentDirection = newDirection;
            rb.velocity = currentDirection * Speed;

            directionRoutine = StartCoroutine(DirectionChangeRoutine());
            return;

        }

        if ((other.CompareTag("Enemy") || other.CompareTag("Terrain")) && !HitEnemies.Contains(other.gameObject))
        {
            HitEnemies.Add(other.gameObject);
            CreateHitEffect();
            OnDestroy();
            Destroy(gameObject);

            if (other.CompareTag("Enemy") && other.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage((int)Damage);
            }
        }
    }

    private void CreateHitEffect()
    {

        // Create explosion effect
        if (SparkHitEffectPrefab != null)
        {
            var SparkHitEffect = Instantiate(SparkHitEffectPrefab, transform.position, Quaternion.identity);
            SparkHitEffect.transform.SetParent(GameObject.Find("Projectiles").transform);
            Destroy(SparkHitEffect, 2f); // Adjust duration to match your animation
        }

    }

}