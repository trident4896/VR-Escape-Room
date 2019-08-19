using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawn : MonoBehaviour
{
    public Transform cameraObject;
    float buttonSpawnDistance = 1.5f;
    public GameObject buttonPrefab;
    public bool isButtonSpawn = false;

    public float buttonDespawnTimer = 5f;

    private void Update()
    {
        if(isButtonSpawn == true)
        {
            buttonDespawnTimer -= Time.deltaTime;
        }

        if (buttonDespawnTimer <= 0)
        {
            ButtonDespawn();
        }

    }

    public void ButtonSpawning()
    {
        Vector3 playerPosition = cameraObject.position;
        Vector3 playerDirection = cameraObject.forward;
        Quaternion playerRotation = cameraObject.rotation;
        Vector3 spawnPos = playerPosition + (playerDirection * buttonSpawnDistance);

        if (isButtonSpawn == false)
        {
            var buttonAsset = Resources.Load("YN_Button") as GameObject;
            buttonPrefab = Instantiate(buttonAsset, spawnPos, playerRotation);
            isButtonSpawn = true;
        }
    }

    public void ButtonDespawn()
    {
        Destroy(buttonPrefab);
        isButtonSpawn = false;
        buttonDespawnTimer = 5f;
    }
}
