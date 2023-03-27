using Newtonsoft.Json;


public static class LoginAccess
{

    public static List<Admin> LoadAll(string fileName)
    {
        string JSONString = File.ReadAllText(fileName);
        List<Admin> accounts = JsonConvert.DeserializeObject<List<Admin>>(JSONString) ?? new List<Admin>();
        return accounts;
    }


    public static void WriteAll(List<Admin> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }
}