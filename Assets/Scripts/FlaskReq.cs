using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlaskReq : MonoBehaviour
{
    public string Dungeon_url = "http://127.0.0.1:5000/getDungeonGAN";  
    public string Room_url = "http://127.0.0.1:5000/getRoomCGAN"; 
    

    public List<int> DungeonData;
    public List<int> RoomData;
    public DungeonGenerator dungeonGenerator;

    int reqCount = 0;
    public void GetDungeonData()
    {
        StartCoroutine(RequestDungeonGAN());
    }
    public IEnumerator GetRoomData()
    {
        yield return StartCoroutine(RequestRoomGAN(dungeonGenerator.RoomsUpdated));
    }

    IEnumerator RequestDungeonGAN()
    {
        
        UnityWebRequest request = UnityWebRequest.Get(Dungeon_url);
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
        string RoomGAN_url_with_timestamp = Room_url + "?timestamp=" + DateTime.Now.Ticks;
        UnityWebRequest request2 = UnityWebRequest.Get(RoomGAN_url_with_timestamp);
        Debug.Log("Request Sent RoomGAN: " + RoomGAN_url_with_timestamp);

        yield return request2.SendWebRequest();

        if (request2.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(request2.error);
        }
        else
        {
            // Parse response JSON using JsonUtility
            string jsonResponse2 = request2.downloadHandler.text;
            Debug.Log("Response received: " + jsonResponse2);
            RoomDataResponse response2 = JsonUtility.FromJson<RoomDataResponse>(jsonResponse2);
            RoomData = response2.room_data;
            reqCount++;
            Debug.Log("RoomDataCount: "+ RoomData.Count);        
            
           
        }
    }

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
