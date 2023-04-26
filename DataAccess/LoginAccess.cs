using Newtonsoft.Json;

//omar's jsoneditor aangepast naar admin behoeftes
public static class LoginAccess
{

    public static List<Admin> LoadAll(string fileName)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\admindata.json");
        string JSONString = File.ReadAllText(filePath);

        List<Admin> accounts = JsonConvert.DeserializeObject<List<Admin>>(JSONString) ?? new List<Admin>();
        return accounts;
    }


    public static void WriteAll(List<Admin> accounts)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\admindata.json");
        string updatedJSONString = JsonConvert.SerializeObject(accounts, Formatting.Indented);

        File.WriteAllText(filePath, updatedJSONString);
    }
}