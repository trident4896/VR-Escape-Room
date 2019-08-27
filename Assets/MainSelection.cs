using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSelection : MonoBehaviour
{
    public static float getMainCursorFill;
    private float mainFillDelayTimer = 0.5f;

    private MainFill mainCursorFill;
    private SceneManagement levelLoading;

    public SceneManagement levelLoadingObject;
    // Start is called before the first frame update
    void Start()
    {
        mainCursorFill = GetComponent<MainFill>();
        levelLoading = levelLoadingObject.GetComponent<SceneManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit mainHit;
        Ray mainRay;

        mainRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 mainForward = Camera.main.transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(Camera.main.transform.position, mainForward, Color.red);

        if (Physics.Raycast(mainRay, out mainHit))
        {
            print("Looking");
            var mainButtonSelection = mainHit.transform;
            if (mainButtonSelection.CompareTag("Start") || mainButtonSelection.CompareTag("Quit"))
            {
                if (mainButtonSelection.CompareTag("Start"))
                {
                    mainFillDelayTimer -= Time.deltaTime;

                    if (mainFillDelayTimer <= 0f)
                    {
                        mainCursorFill.MainCursorToFill(1);
                    }

                    if (getMainCursorFill >= 1f)
                    {
                        print("Start");
                        getMainCursorFill = 0f;
                        mainFillDelayTimer = 0.5f;

                        levelLoading.LoadLevel(1);
                    }
                }

                if (mainButtonSelection.CompareTag("Quit"))
                {
                    mainFillDelayTimer -= Time.deltaTime;

                    if (mainFillDelayTimer <= 0f)
                    {
                        mainCursorFill.MainCursorToFill(1);
                    }

                    if (getMainCursorFill >= 1f)
                    {
                        print("End");
                        getMainCursorFill = 0f;
                        mainFillDelayTimer = 0.5f;

                        Application.Quit();
                    }

                }
            }
            else
            {
                print("LookingOther");
                mainCursorFill.MainCursorToFill(0);

                mainFillDelayTimer = 0f;
                getMainCursorFill = 0f;
            }
        }
        else
        {
            print("NotLooking");
            mainCursorFill.MainCursorToFill(0);

            mainFillDelayTimer = 0f;
            getMainCursorFill = 0f;
        }
    }
}
