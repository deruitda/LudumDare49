using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10;
    [SerializeField]
    private float _jumpForce = 2;
    [SerializeField]
    private Rigidbody _rigidbody;

    private float _minJumpDistance = 1.5f; // distance from ground allowed before player can jump again

    /// <summary>
    /// Character move speed
    /// </summary>
    public float MoveSpeed => _moveSpeed;

    /// <summary>
    /// Force applied during jump
    /// </summary>
    public float JumpForce => _jumpForce;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal movement (a and d keys)
        transform.Translate(new Vector2(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0));

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
        }

        RaycastHit hit;
        var a = Physics.Raycast(_rigidbody.transform.position, Vector2.down, out hit, _minJumpDistance);
    }

    private bool IsGrounded() => Physics.Raycast(_rigidbody.transform.position, Vector2.down, _minJumpDistance);
}
