using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour {

    [SerializeField] float shipSpeed = 1f;
    [SerializeField] float padding = 1f;
    [Range(0, 40)] [SerializeField] float rollMaxAngel = 20f;

    private PlayerStatController playerStatController;
    private GameSceneManager sceneManager;

    private Rect bounds;

    void Start() {
        sceneManager = FindObjectOfType<GameSceneManager>();
        playerStatController = GetComponent<PlayerStatController>();
        playerStatController.OnShieldValueChange += OnSheildValueChangeCheckForDeath;
        Camera gameCamera = Camera.main;
        bounds = Rect.MinMaxRect(
            gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding,
            gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding,
            gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding,
            gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding);
    }

    void Update() {
        MoveShip();
    }

    private void MoveShip() {
        float inputX = CrossPlatformInputManager.GetAxis("Horizontal");
        float inputY = CrossPlatformInputManager.GetAxis("Vertical");

        float deltaX = inputX * Time.deltaTime * shipSpeed;
        float deltaY = inputY * Time.deltaTime * shipSpeed;

        float newPosX = Mathf.Clamp(transform.position.x + deltaX, bounds.xMin, bounds.xMax);
        float newPosY = Mathf.Clamp(transform.position.y + deltaY, bounds.yMin, bounds.yMax);

        transform.position = new Vector3(newPosX, newPosY);

        float roll = rollMaxAngel * inputX;
        transform.rotation = Quaternion.Euler(0, roll, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DamangeDealer damangeDealer = collision.gameObject.GetComponent<DamangeDealer>();
        if (damangeDealer) {
            playerStatController.DecreaseShield(damangeDealer.DamangeAmount);
            Destroy(collision.gameObject);
        }
    }

    private void OnSheildValueChangeCheckForDeath(int shield) {
        if (shield <= 0) {
            sceneManager.LooseScreenDelayed();
            Destroy(gameObject);     
        }
    }
}
