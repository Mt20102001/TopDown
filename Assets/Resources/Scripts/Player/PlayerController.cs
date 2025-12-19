using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private IsoController movementController;
    [SerializeField] private CombatController combatController;


    public bool IsMoving { get; private set; }
    public bool IsCombat { get; private set; }
    public Vector2 LocalMove { get; private set; }


    void Start()
    {
        if (movementController != null)
            movementController.Init();

        if (combatController != null)
            combatController.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementController != null)
        {
            IsMoving = movementController.MoveDir.magnitude > 0.01f;
            if (IsMoving)
            {
                Vector3 local = transform.InverseTransformDirection(movementController.MoveDir).normalized;
                LocalMove = new Vector2(local.x, local.z);
            }
            else
                LocalMove = Vector2.zero;
            movementController.CustomUpdate(Time.deltaTime);
        }

        if (combatController != null)
        {
            IsCombat = combatController.HasTargetToAtk;
            combatController.CustomUpdate(Time.deltaTime);
        }

        if (combatController.HasTargetToAtk)
            movementController.LookAtTarget(combatController.TargetPosAtk, Time.deltaTime);
        else
        {
            if (movementController.MoveDir != Vector3.zero && IsMoving)
                movementController.LookDirection(movementController.MoveDir, Time.deltaTime);
        }
    }
}
