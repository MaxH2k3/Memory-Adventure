using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class SaveSystem : IInitializable
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = new();

    public void Initialize()
    {
        _jsonSerializerSettings.Formatting = Formatting.Indented;
    }

    public void Save<T>(T data, string destination)
    {
        var json = JsonConvert.SerializeObject(data, _jsonSerializerSettings);
        File.WriteAllText(Application.persistentDataPath + "/" + destination + ".json", json);
    }

    public T Load<T>(string destination)
    {
        var jsonString = File.ReadAllText(Application.persistentDataPath + "/" + destination + ".json");
        return JsonConvert.DeserializeObject<T>(jsonString);
    }

}
