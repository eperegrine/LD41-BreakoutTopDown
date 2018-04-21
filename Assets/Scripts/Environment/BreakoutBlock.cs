using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Renderer))]
public class BreakoutBlock : MonoBehaviour
{
    public int StartingHealth = 3;
    private int _health = 3;

    public Color[] Colours;
    public UnityEvent OnDeath;
    
    Renderer r;

    private void Awake()
    {
        r = GetComponent<Renderer>();
        _health = StartingHealth;
    }

    private void Start()
    {
        CalculateColour();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Ball>() != null)
        {
            TakeDamage(1);
        }
    }

    private void TakeDamage(int amt)
    {
        _health -= amt;
        if (_health > 0) CalculateColour();
        else Die();
    }

    private void Die()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }

    private void CalculateColour()
    {
        int colLength = Colours.Length;
        if (_health > colLength)
        {
            Debug.LogWarningFormat("BreakoutBlock {0} has health outside colour range");
        }

        if (colLength != 0 && _health!=0)
        {
            r.material.color = Colours[Mathf.Min(colLength, _health)-1];
        }
    }
}
