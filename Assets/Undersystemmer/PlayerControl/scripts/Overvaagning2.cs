using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overvaagning2 : MonoBehaviour
{
	public List<GameObject> cameraList = new List<GameObject>();
    private bool playerInRange = false; // To track if the player is within range
    public RawImage img;
	public RenderTexture renderTexture;

	private Camera currentCamera;
	private int currentCameraIndex = 0;
	// Start is called before the first frame update
	void Start()
	{
		GameObject[] cameras = GameObject.FindGameObjectsWithTag("VideoCamera");
		foreach (GameObject cam in cameras)
		{
			cameraList.Add(cam);
			cam.SetActive(false);
		}

	}

	// Update is called once per frame
	void Update()
	{

		{

            if (!playerInRange)
            {
                return; // Ignore input if the player is not in range
            }

            if (Input.GetKeyDown(KeyCode.M))
			{
				currentCameraIndex++;

				if (currentCameraIndex >= cameraList.Count)
				{
					currentCameraIndex = 0;
				}

				changecamtotexture(currentCameraIndex);
			}
		}
	}



	public void changecamtotexture(int camNumber)
	{
		if (camNumber >= 0 && camNumber < cameraList.Count)
		{
			if(currentCamera != null)
			{
				currentCamera.gameObject.SetActive(false);
			}
			
			Camera activeCamera = cameraList[camNumber].GetComponent<Camera>();
			if (activeCamera.gameObject != null)
			{
				activeCamera.gameObject.SetActive(true);	
			}

			
			if (currentCamera != null)
			{
				currentCamera.targetTexture = null;
			}

			
			activeCamera.targetTexture = renderTexture;
			img.texture = renderTexture;
			currentCamera = activeCamera;
		}
		else
		{
			Debug.LogWarning("Camera number is out of range.");
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

}
