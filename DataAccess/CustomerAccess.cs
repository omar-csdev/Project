using Newtonsoft.Json;

//omar's jsoneditor aangepast naar de behoeftes van customerdata.json
public static class CustomerAccess
{

    public static List<CustomerAccount> LoadAll()
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\customerdata.json");
        string JSONString = File.ReadAllText(filePath);

        List<CustomerAccount> accounts = JsonConvert.DeserializeObject<List<CustomerAccount>>(JSONString) ?? new List<CustomerAccount>();
        return accounts;
    }


    public static void WriteAll(List<CustomerAccount> accounts)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\customerdata.json");
        string updatedJSONString = JsonConvert.SerializeObject(accounts, Formatting.Indented);

        File.WriteAllText(filePath, updatedJSONString);
    }
}