using UnityEngine;
using UnityEngine.InputSystem; // Pointer 사용

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Rigidbody2D pickRb;
    [SerializeField] private Collider2D pickCollider;


    private Camera cam;
    private bool isDragging;

    private void Awake()
    {
        cam = Camera.main;
        if (pickRb == null) pickRb = GetComponent<Rigidbody2D>();
        if (pickCollider == null) pickCollider = GetComponent<Collider2D>();
    }

    private Vector3 PointerWorldPosition
    {
        get
        {
            // 입력/카메라 null 방어
            if (Pointer.current == null || cam == null)
            {
                return pickRb != null ? (Vector3)pickRb.position : Vector3.zero;
            }

            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, -cam.transform.position.z));
            worldPos.z = 0f;
            return worldPos;
        }
    }

    private void Update()
    {
        if (Pointer.current == null) return;

        var press = Pointer.current.press;

        if (press.wasPressedThisFrame && !isDragging)
        {
            if (IsPointerOnPick())
            {
                DragStart();
            }
        }

        if (isDragging && press.isPressed)
        {
            DragMove();
        }

        if (press.wasReleasedThisFrame && isDragging)
        {
            DragEnd();
        }
    }

    private void DragStart()
    {
        if (pickRb == null) return;
        isDragging = true;

        // 시작 프레임에 바로 위치 맞춤
        pickRb.position = (Vector2)PointerWorldPosition;
        GameSignals.RaisePickDrag();
    }

    private void DragMove()
    {
        if (pickRb == null) return;

        // “그냥 끌고 다니기”: 트랜스폼/리짓바디 위치를 포인터로 고정
        // (물리 충돌/탄성 필요하면 MovePosition으로 바꾸는 게 좋음)
        pickRb.position = (Vector2)PointerWorldPosition;
    }

    private void DragEnd()
    {
        isDragging = false;
        GameSignals.RaisePickDragEnd();
    }
    private bool IsPointerOnPick()
    {
    if (pickCollider == null) return false;

    Vector2 worldPos = PointerWorldPosition;
    return pickCollider.OverlapPoint(worldPos);
    }
}
