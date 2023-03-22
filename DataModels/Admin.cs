public class Admin
{
    public string UserName;
    public string Password;
    public bool IsLoggedIn;

    public Admin(string u, string p)
    {
        this.UserName = u;
        this.Password = p;
    }
    
    public void LogIn(string u, string p) 
    {
        this.IsLoggedIn = true;
    }

    public void LogOut()
    {
        this.IsLoggedIn = false;
    }
}