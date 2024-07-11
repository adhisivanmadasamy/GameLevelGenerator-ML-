using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskReq : MonoBehaviour
{
    [SerializeField] string DungeonGAN_url = "http://127.0.0.1:5000/getDungeonGAN";
    [SerializeField] string DungeonOther_url = "http://127.0.0.1:5000/getDungeonGAN";
    [SerializeField] string RoomGAN_url = "http://127.0.0.1:5000/getRoomGAN";
    [SerializeField] string RoomOther_url = "http://127.0.0.1:5000/getDungeonGAN";

    public List<int> DungeonData;
    public List<int> RoomData;
    public DungeonGenerator dungeonGenerator;

    public void GetDungeonData()
    {
        StartCoroutine(RequestDungeonGAN());
    }
    public void GetRoomData()
    {
        StartCoroutine(RequestRoomGAN(dungeonGenerator.RoomsUpdated));
    }


    IEnumerator RequestDungeonGAN()
    {
        
        UnityWebRequest request = UnityWebRequest.Get(DungeonGAN_url);
        Debug.Log("Request Sent DungeonGAN");
        
        yield return request.SendWebRequest();

        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request.error);
        }
        else
        {
            // Parse response JSON using JsonUtility
            string jsonResponse = request.downloadHandler.text;
            ImageDataResponse response = JsonUtility.FromJson<ImageDataResponse>(jsonResponse);
            
            DungeonData = response.image_data;

            Debug.Log(DungeonData.Count);

            dungeonGenerator.ClearTiles();
            dungeonGenerator.DisplayDungeonMap();           
        }
    }

    IEnumerator RequestRoomGAN(int roomsup)
    {
        
        UnityWebRequest request2 = UnityWebRequest.Get(RoomGAN_url);
        Debug.Log("Request Sent RoomGAN");

        yield return request2.SendWebRequest();

        if (request2.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request2.error);
        }
        else
        {
            // Parse response JSON using JsonUtility
            string jsonResponse2 = request2.downloadHandler.text;
            ImageDataResponse response2 = JsonUtility.FromJson<ImageDataResponse>(jsonResponse2);

            RoomData = response2.image_data;

            Debug.Log(RoomData.Count);
            

            
        }
    }


    // Class to match the structure of JSON response
    [System.Serializable]
    public class ImageDataResponse
    {
        public string status;
        public List<int> image_data;
    }

    [System.Serializable]
    public class RoomDataResponse
    {
        public string status;
        public List<int> room_data;
    }
}
