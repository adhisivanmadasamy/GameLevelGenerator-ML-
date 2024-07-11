using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestReq : MonoBehaviour
{
    [SerializeField] string DungeonGAN_url = "http://127.0.0.1:5000/getDungeonGAN";
    [SerializeField] string DungeonOther_url = "http://127.0.0.1:5000/getDungeonGAN";
    [SerializeField] string RoomGAN_url = "http://127.0.0.1:5000/getDungeonGAN";
    [SerializeField] string RoomOther_url = "http://127.0.0.1:5000/getDungeonGAN";

    // Function to send request to Flask
    public void GetDungeonData()
    {
        StartCoroutine(RequestDungeonGAN());
    }

    IEnumerator RequestDungeonGAN()
    {
        // Create UnityWebRequest
        UnityWebRequest request = UnityWebRequest.Get(DungeonGAN_url);

        // Send request
        yield return request.SendWebRequest();

        // Check for errors
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse response JSON using JsonUtility
            string jsonResponse = request.downloadHandler.text;
            ImageDataResponse response = JsonUtility.FromJson<ImageDataResponse>(jsonResponse);

            // Log the output in Unity console
            Debug.Log("Image data received from Flask: " + string.Join(", ", response.image_data));
        }
    }

    // Class to match the structure of JSON response
    [System.Serializable]
    public class ImageDataResponse
    {
        public string status;
        public List<int> image_data;
    }
}




