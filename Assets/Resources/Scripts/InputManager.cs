using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public Vector2 Input { get; private set; }

    [Header("Axis Names")]
    [SerializeField] private string horizontalAxis = "Horizontal";
    [SerializeField] private string verticalAxis = "Vertical";

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        float x = UnityEngine.Input.GetAxisRaw(horizontalAxis);
        float y = UnityEngine.Input.GetAxisRaw(verticalAxis);

        this.Input = new Vector2(x, y);

        if (this.Input.sqrMagnitude > 1)
            this.Input.Normalize();
    }
}
