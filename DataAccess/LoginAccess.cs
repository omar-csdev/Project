using Newtonsoft.Json;


public static class LoginAccess
{

    public static List<Admin> LoadAll(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\admindata.json");
        string JSONString = File.ReadAllText(filePath);

        List<Admin> accounts = JsonConvert.DeserializeObject<List<Admin>>(JSONString) ?? new List<Admin>();
        return accounts;
    }


    public static void WriteAll(List<Admin> accounts, string fileName)
    {
        string updatedJSONString = JsonConvert.SerializeObject(accounts, Formatting.Indented);

        File.WriteAllText(fileName, updatedJSONString);
    }
}