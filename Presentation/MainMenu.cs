using Project.DataAccess;
using Project.Presentation;
using System;
using System.Drawing;
using Console = Colorful.Console;
static class MainMenu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    public static void NewStart(bool isGuest)
    {
        while (true)
        {
            SaveOldReservations.WriteOldReservationsToJSON();
            Console.Clear();
            Helper.DisplayRestaurantLogo();
            Helper.Say("1", "Login");
            Helper.Say("2", "Create Account");
            Helper.Say("3", "Continue as guest");
            Helper.Say("4", "Admin");
            Helper.Say("5", "Exit");
            int firstinput;
            while (true)
            {
                try
                {
                    // Read user input as an integer
                    firstinput = int.Parse(Console.ReadLine());

                    // Validate user input
                    if (firstinput < 1 || firstinput > 5)
                    {
                        string message = ("Please enter a valid number between 1 and 5.");
                        Helper.Error(message);
                        NewStart(isGuest);
                    }
                    break;
                }
                catch (FormatException)
                {
                    string message = ("Please enter a valid number between 1 and 5.");
                    Helper.Error(message);
                    NewStart(isGuest);
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    Helper.Error(message);
                    NewStart(isGuest);
                }
            }

            switch (firstinput)
            {
                case 1:
                    // Handle Option 1
                    // Sign the User in

                    // SignIn();
                    // Invoke LoggedInUser();
                    //LoggedInUser(isGuest);
                    AccountManager.LogIn();
                    break;
                case 2:
                    // Handle Option 2
                    AccountManager.CreateAccount();
                    break;
                case 3:
                    isGuest = true;
                    // Handle Option 3
                    ContinueAsGuest(isGuest); // Pass the isGuest parameter
                    break;
                case 4:
                    // Handle Option 4
                    AdminLogin.Start();
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    return; // Breaks out of the method, terminating the program
                            // Does not work, goes back to admin for example if you log in as admin and then logout and return to main window instead of quitting.
                default:
                    Helper.ErrorDisplay("1, 2, 3, 4 or 5");
                    break;
            }

            Console.WriteLine(); // Empty line for spacing
        }
    }

    public static void ContinueAsGuest(bool isGuest)
    {
        int choice;

        while (true)
        {
            Console.Clear();
            Helper.DisplayRestaurantLogo();
            Helper.Say("1", "Make a Reservation");
            Helper.Say("2", "View Menu and order");
            Helper.Say("3", "Go back to main menu");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    // Handle Option 1
                    Reservations.Reservationstart();
                    break;
                case 2:
                    // Handle Option 2
                    Console.Clear();
                    FoodMenu.Start(isGuest); // Pass the isGuest parameter
                    break;
                case 3:
                    // Sign the User out in the JSON
                    // Return the User to the start screen
                    NewStart(isGuest);
                    break;
                default:
                    Helper.ErrorDisplay("1, 2 or 3");
                    break;
            }

            Console.WriteLine(); // Empty line for spacing
        }
    }
}