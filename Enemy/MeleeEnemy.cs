using System.Collections;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float coolDown;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask ciel;
    [SerializeField] private float dosah;
    [SerializeField] private float colliderVzdialenost;
    private Animator animator;
    private bool mozeZautocit;
    private Health zivotCielu;

    void Start()
    {
        mozeZautocit = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GetComponent<Health>().jeMrtvy)
        {
            if (CielVDosahu() && mozeZautocit && zivotCielu.akutalneZivoty > 0)
            {
                mozeZautocit = false;
                animator.SetTrigger("utok");
                StartCoroutine(CoolDown());
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        mozeZautocit = true;
    }

    private bool CielVDosahu()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * dosah * transform.localScale.x * colliderVzdialenost,
           new Vector3(box.bounds.size.x * dosah, box.bounds.size.y, box.bounds.size.z)
           , 0, Vector2.left, 0, ciel);

        if (hit.collider != null)
            zivotCielu = hit.transform.GetComponent<Health>();
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * dosah * transform.localScale.x * colliderVzdialenost,
           new Vector3(box.bounds.size.x * dosah, box.bounds.size.y, box.bounds.size.z));
    }

    private void DamageCiel()
    {
        zivotCielu.TakeDamage(damage);
    }
}
