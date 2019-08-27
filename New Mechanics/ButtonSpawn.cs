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
    private Transform countDownBar;

    private float targetScale = 0f;

    private float scaleRate;
    private float scaleDuration = 40f;

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

        if(isButtonSpawn == true)
        {
            scaleRate += Time.deltaTime / scaleDuration;
            countDownBar.localScale = Vector3.Lerp(countDownBar.localScale, new Vector3(targetScale, countDownBar.localScale.y, countDownBar.localScale.z), Mathf.SmoothStep(0.0f,1.0f,scaleRate));

            if(countDownBar.localScale.x < 0.2f)
            {
                countDownBar.transform.GetComponent<Renderer>().material.color = Color.red;
            }

            if(countDownBar.localScale.x < targetScale)
            {
                scaleRate = 0f;
            }
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

            countDownBar = buttonPrefab.gameObject.transform.Find("Bar");
        }
    }

    public void ButtonDespawn()
    {
        Destroy(buttonPrefab);
        isButtonSpawn = false;
        buttonDespawnTimer = 5f;
        scaleRate = 0f;
    }
}
