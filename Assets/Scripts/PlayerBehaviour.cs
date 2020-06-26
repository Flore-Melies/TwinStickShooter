using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileSpeed;

    private Rigidbody2D myRigidbody;

    private Controls controls;
    private Vector2 inputDirection;
    private Vector2 mouseWorldPos;
    private Vector2 direction;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Enable();
        controls.Main.Move.performed += OnMovePerformed;
        controls.Main.Move.canceled += OnMoveCanceled;
        controls.Main.Shoot.performed += OnShootPerformed;
        controls.Main.Aim.performed += OnAimPerformed;
    }

    private void OnAimPerformed(InputAction.CallbackContext obj)
    {
        var mousePos = obj.ReadValue<Vector2>();
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        direction = (mouseWorldPos - (Vector2) transform.position).normalized;
    }

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        Shoot();
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        inputDirection = obj.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        inputDirection = Vector2.zero;
    }

    // Start is called before the first frame update
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        myRigidbody.velocity = inputDirection * speed;
    }

    private void Shoot()
    {
        var projShot = Instantiate(projectile, transform.position, Quaternion.identity);
        projShot.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed, ForceMode2D.Impulse);
        projShot.transform.right = direction;
    }

    private void OnDestroy()
    {
        controls.Disable();
    }
}