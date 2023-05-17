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
    public CustomerAccount(string u, string p) 
    {
        this.UserNameSetter = u;
        this.PasswordSetter = p;
    }

}