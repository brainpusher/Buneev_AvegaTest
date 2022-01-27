using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] [TagSelector] private string tagToCheckIntersectionWith;
    [SerializeField] private Rigidbody bulletRigidbody;
    [SerializeField] private Renderer bulletRenderer;
    
    [Tooltip("Higher value = higher speed")]
    [SerializeField] private float bulletSpeed = 52f;
    
    private float _bulletDamage = 0f;

    public void InitBullet(Transform startPosition, float damage, Color bulletColor)
    {
        _bulletDamage = damage;
        transform.position = startPosition.position;
        bulletRenderer.material.color = bulletColor;
    }

    public void LaunchBullet(Vector3 directionWithoutSpread)
    {
        bulletRigidbody.AddForce(directionWithoutSpread.normalized * bulletSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals(tagToCheckIntersectionWith))
        {
            collision.collider.gameObject.GetComponent<HealthManager>().TakeDamage(_bulletDamage);
        }
        
        ResetBulletForce();
        gameObject.SetActive(false);
    }

    private void ResetBulletForce()
    {
        bulletRigidbody.velocity = Vector3.zero;
        bulletRigidbody.angularVelocity = Vector3.zero;
    }
}
