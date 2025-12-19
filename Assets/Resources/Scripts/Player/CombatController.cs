using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float rangeBasicAtk;
    [SerializeField] private float firerateBasicAtk;

    private float currentFirerate;
    private float currentRangeAtk;
    private Collider[] hitColliders = new Collider[1];

    public Vector3 TargetPosAtk { get; private set; }
    public bool HasTargetToAtk { get; private set; }


    void OnDrawGizmosSelected()
    {
        if (rangeBasicAtk > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, rangeBasicAtk);
        }
    }


    public void Init()
    {
        hitColliders = new Collider[1];
        currentFirerate = 0f;
        currentRangeAtk = rangeBasicAtk;
        TargetPosAtk = Vector3.zero;
    }

    public void CustomUpdate(float tick)
    {
        int numb = Physics.OverlapSphereNonAlloc(this.transform.position, currentRangeAtk, hitColliders, targetLayer);
        this.HasTargetToAtk = numb > 0;


        if (!HasTargetToAtk)
        {
            currentFirerate = 0f;
            return;
        }

        TargetPosAtk = hitColliders[0].transform.position;

        if (currentFirerate < firerateBasicAtk)
        {
            currentFirerate += tick;
            if (currentFirerate >= firerateBasicAtk)
            {
                currentFirerate = 0f;
                Fire();
            }
        }
    }

    private void Fire()
    {
        Debug.Log("Shoot");
    }
}
