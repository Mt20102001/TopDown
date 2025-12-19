using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class IsoController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 720f;
    public Camera cam;

    CharacterController cc;

    Vector3 isoForward;
    Vector3 isoRight;

    public Vector3 MoveDir { get; private set; }

    public void Init()
    {
        cc = GetComponent<CharacterController>();

        if (cam == null)
            cam = Camera.main;

        CalculateIsoVectors();
    }


    void CalculateIsoVectors()
    {
        Vector3 f = cam.transform.forward;
        f.y = 0;
        f.Normalize();

        Vector3 r = cam.transform.right;
        r.y = 0;
        r.Normalize();

        isoForward = f;
        isoRight = r;
    }


    public void LookAtTarget(Vector3 targetPos, float tick)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRot = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotationSpeed * tick
        );
    }


    public void LookDirection(Vector3 direction, float tick)
    {
        Quaternion target = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            target,
            rotationSpeed * tick
        );
    }


    public void CustomUpdate(float tick)
    {
        Vector2 input = InputManager.Instance.Input;

        Vector3 move = isoForward * input.y + isoRight * input.x;

        if (move.sqrMagnitude > 1f)
            move.Normalize();

        MoveDir = move;

        // movement
        cc.Move(move * moveSpeed * tick);
    }
}
