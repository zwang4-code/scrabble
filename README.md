# Team2_Scrabble
We plan to test the game Scrabble (URL: https://github.com/poyea/scrabble ).  This game is written in C#, so we will be using the NUnit framework to perform  our unit tests using Visual Studio. 

# Steps to Build tests
* Take git clone of the repository
* Open project in VS 2022 on Windows platform
* Clean and rebuild the solution
* Click on Run Tests

# Prerequisites to run UI tests
* Download WinAppDriver onto your Window’s computer 
  - Go to https://github.com/Microsoft/WinAppDriver/releases and download WinAppDriver v1.2.1 (Latest)
* Enable Developer Mode in Windows settings 
* Find WinAppDriver in your local folder (located in C:\Program Files (x86)\Windows Application Driver by default) and double click on it to run. 
* Go to ScrabbleAppiumTest/SessionSetup.cs file and update the field "ScrabblePath" to your locally downloaded Scrabble project
  - @"<local_path_to_Scrabble_Folder>\Scrabble\bin\Debug\Scrabble2018.exe"
