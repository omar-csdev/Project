public class CustomerAccount
{
    private string _userName;
    public string UserNameSetter
    {
        get
        {
            return _userName;
        }

        set
        {
            _userName = value;
        }
    }

    private string _password;
    public string PasswordSetter
    {
        get
        {
            return _password;
        }

        set
        {
            _password = value;
        }
    }

    public bool IsLoggedIn = false;
    public int ID;
    public CustomerAccount(string u, string p, List<CustomerAccount> list)
    {
        this.UserNameSetter = u;
        this.PasswordSetter = p;
        if (list?.Count > 0)
        {
            this.ID = list[list.Count - 1].ID + 1;
        }

        else
        {
            this.ID = 1;
        }
    }

}