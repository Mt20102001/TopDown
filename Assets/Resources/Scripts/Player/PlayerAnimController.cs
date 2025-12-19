using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        animator.SetBool("IsMoving", player.IsMoving);
        animator.SetBool("IsCombat", player.IsCombat);
        animator.SetFloat("X", player.LocalMove.x);
        animator.SetFloat("Y", player.LocalMove.y);
    }
}
