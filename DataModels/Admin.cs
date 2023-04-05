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
        if (this.UserName == u && this.Password == p) 
        {
            this.IsLoggedIn = true;
            Console.WriteLine($"Admin {this.UserName} logged in succesfully!");
        }

        else
        {
            Console.WriteLine("Invalid login.");
        }
    }

    public void LogOut()
    {
        this.IsLoggedIn = false;
    }
}