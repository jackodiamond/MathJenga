using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class StackData : MonoBehaviour
{
    static string url = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";

    private void Start()
    {
        fetchJson();
    }

    async void fetchJson()
    {
        string json = await FetchJson();

        List<DataModel> objects = JsonConvert.DeserializeObject<List<DataModel>>(json);

        StackGenerator.Instance.generateStack(objects);
    }

    public static async Task<string> FetchJson()
    {
        using (HttpClient client = new HttpClient())
        {
           
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return json;
            }
            else
            {
                Debug.Log($"Failed to retrieve data. StatusCode: {response.StatusCode}");
                return null;
            }
        }
    }
}

public class DataModel
{
    public int id;
    public string subject;
    public string grade;
    public int mastery;
    public string domainid;
    public string domain;
    public string cluster;
    public string standardid;
    public string standarddescription;
}