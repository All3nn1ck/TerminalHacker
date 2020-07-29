using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    //Game configuration variables
    string[] level_1_passwords = { "books", "coffee", "mug", "password", "font", "borrow" };
    string[] level_2_passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level_3_passwords = { "starfield", "telescope", "environment", "exploration", "astronauts" };
    const string menuMessage = "Write menu";

    //Game state variables
    private int level;
    private enum Screen { MainMenu, Password, Win } //'Screen' helps me to understand in which part of the game the user is playing
    string password;
    Screen currentScreen;




    // Start is called before the first frame update
    void Start()
    {
        this.ShowMainMenu();        
    }

    //This is the first thing player will see when launching the game
    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu; //At the start of the game the player is in the main manu 
        //WriteLine is a method of Terminal.cs and it allows me to write on the terminal seen in the game
        Terminal.WriteLine("Welcome to 'Terminal Hacker'");
        Terminal.WriteLine("The player shall find the correct word to solve the anagram");
        Terminal.WriteLine(menuMessage);
        Terminal.WriteLine("Select the theme of the adventure");
        Terminal.WriteLine("Press 1 for: Starbucks_WIFI (Easy)");
        Terminal.WriteLine("Press 2 for: Police_Station (Medium)");
        Terminal.WriteLine("Press 3 for: NASA (Hard)");
        
    }

    //'OnUserInput' is called everytime the user press 'enter' and it displays the string written by the user in the console
    void OnUserInput(string input)
    {
        //I wanted to add an Easter Egg with the number 307
        //We can always go to the main menu
        if (input == "menu" || input == "Menu")
        {
            Terminal.ClearScreen();
            this.currentScreen = Screen.MainMenu;
            this.ShowMainMenu();
            //Without the following line I had problems with the password screen, I could press 1 and mess with the main menu writes
            //Adding this prevent that problem
        }
        else if (this.currentScreen == Screen.MainMenu)
        {
            this.RunMainMenu(input);
        } else if (this.currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
        
        
    }

    //This handle the level
    void RunMainMenu(string input)
    {
        bool isLevelValidNumber = (input == "1" || input == "2" || input == "3");
        if (isLevelValidNumber)
        {
            level = int.Parse(input); //'Parse' take a string and convert it into a number
            AskForPassword();
        }
        else if (input == "307")
        {
            Terminal.WriteLine("Welcome Bahamut"); //Easter egg
            Terminal.WriteLine(@"
    ..----..
  .': o  o :`.
 .':   ()   :`.
.' :-======-: `.
`-' `.    .' `-'
   .'      `.
      Rowlf
"
                );
            Terminal.WriteLine(menuMessage);
        }
        else
        {
            int randomNumber = Random.Range(1, 1000);
            Terminal.WriteLine("Error_" + randomNumber + ". Please, try something else");
            Terminal.WriteLine(menuMessage);

        }
    }

    void AskForPassword() //TODO - things for the third level
    {
        this.currentScreen = Screen.Password;
        Terminal.ClearScreen(); //This is to clear my mind and the screen
        this.SetRandomPassword();
        Terminal.WriteLine("Enter your password: " + password.Anagram()); //psw.Anagram just shuffle the password to create an hint for the player


    }

    void SetRandomPassword()
    {
        switch (level)
        {
            case 1:
                this.password = this.level_1_passwords[Random.Range(0, this.level_1_passwords.Length)]; //The password should be picked randomly
                break;
            case 2:
                this.password = this.level_2_passwords[Random.Range(0, this.level_2_passwords.Length)];
                break;
            case 3:
                this.password = this.level_3_passwords[Random.Range(0, this.level_3_passwords.Length)];
                break;
            default:
                Debug.Log("Invalid level number"); //this will be very difficult to reach due to the code at line 67
                break;
        }
    }

    //Mission: select level 1 or level 2
    void CheckPassword(string input)
    {
        if(input == password)
        {
            WinScreen();
        }
        else
        {
            AskForPassword();
        }
    }

    void WinScreen()
    {
        this.currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();    
    }

    void ShowLevelReward()
    {
        switch(level)
        {
            case 1:
                Terminal.WriteLine("Have a book...");
                Terminal.WriteLine(@"
    _______
   /      //
  /      //
 /_____ //
(______(/           
"
                );
                Terminal.WriteLine(menuMessage);
                break;
            case 2:
                Terminal.WriteLine("Hey officer, can you help my frogs?");
                Terminal.WriteLine(@"
    (+)  (+)                     
   /        \  
   \  -==-  / 
    \      / 
    
   /        \

K  E  R  M  I  T   
"
                );
                Terminal.WriteLine(menuMessage);
                break;
            case 3:
                Terminal.WriteLine("Here's casper for NASA muchachos");
                Terminal.WriteLine(@"
      .-""""-.
     / -   -  \
    |  .-. .- |
    |  \o| |o (
    \     ^    \
     '.  )--'  /
       '-...-'` 
"
                );
                Terminal.WriteLine(menuMessage);
                break;
        }
    }
}

