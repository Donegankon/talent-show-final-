using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{
    public enum Commands
    {
        MOVEFORWARD,
        MOVEBACKWARD,
        TURNRIGHT,
        TURNLEFT,
        WAIT,
        LEDON,
        LEDOFF,
        GETLIGHTLEVELRIGHT,
        GETLIGHTLEVELLEFT,
        GETLIGHTLEVELAVERAGE,
        NOTEOFF,
        NOTEON,
        NONE,
        DONE
    }
    // **************************************************
    //
    // Title: Finch Control - Talent Show
    // Description: Konnor Donegans Finch Robot program
    //              For M3S1 Talent Show
    // Application Type: Console
    // Author: Konnor Donegan
    // Dated Created: 1/22/2020
    // Last Modified: 1/25/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmMenuScreen(finchRobot);
                        break;

                    case "e":
                        userProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }



        #region USER PROGRAMMING
        static void userProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            string menuChoice;
            bool quitMenu = false;

            //
            // tuple to store all three command parameters
            //
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Commands> commands = new List<Commands>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // Get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit to Menu");
                Console.WriteLine("\t\t Enter Choice");
                menuChoice = Console.ReadLine().ToLower();

                //
                // Menu Choice 
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        DisplayFinchCommands(commands);
                        break;

                    case "d":
                        DisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter one of the letters to go it's menu.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }

        static void DisplayExecuteFinchCommands(Finch finchRobot, List<Commands> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int waitMs = (int)(commandParameters.waitSeconds * 1000);
            int ledBright = commandParameters.ledBrightness;
            string commandOut = "";
            const int MOTORSSPEEDTURN = 125;

            DisplayScreenHeader("Execute given Commaneds Menu");
            Console.WriteLine("Please enter a Command for the Robot");
            foreach (Commands command in commands)
            {
                switch (command)
                {
                    case Commands.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandOut = Commands.MOVEFORWARD.ToString();
                        break;
                    case Commands.MOVEBACKWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandOut = Commands.MOVEBACKWARD.ToString();
                        break;
                    case Commands.TURNRIGHT:
                        finchRobot.setMotors(MOTORSSPEEDTURN, (-1 * MOTORSSPEEDTURN));
                        commandOut = Commands.TURNRIGHT.ToString();
                        break;
                    case Commands.TURNLEFT:
                        finchRobot.setMotors((-1 * MOTORSSPEEDTURN), (MOTORSSPEEDTURN));
                        commandOut = Commands.TURNLEFT.ToString();
                        break;
                    case Commands.WAIT:
                        finchRobot.wait(waitMs);
                        commandOut = Commands.WAIT.ToString();
                        break;
                    case Commands.LEDON:
                        finchRobot.setLED(ledBright, ledBright, ledBright);
                        break;
                    case Commands.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        break;
                    case Commands.GETLIGHTLEVELRIGHT:
                        commandOut = $""
                }
            }
        }

        static void DisplayFinchCommands(List<Commands> commands)
        {
            throw new NotImplementedException();
        }

        static void UserProgrammingDisplayGetFinchCommands(List<Commands> commands)
        {
            throw new NotImplementedException();
        }

        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ALARM
        static void LightAlarmMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            bool quitlightalarmMenu = false;
            string menuChoice;
            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Light Alarm Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Input the Sensors to Monitor");
                Console.WriteLine("\tb) Input Range-Type");
                Console.WriteLine("\tc) Input Min-Max Threshold Value");
                Console.WriteLine("\td) Input Time to Monitor");
                Console.WriteLine("\te Input Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplayInputSensorsMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplayInputRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplayInputMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmDisplayInputTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmInputAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitlightalarmMenu = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a letter to go to it's menu.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitlightalarmMenu);
        }

        static string LightAlarmDisplayInputSensorsMonitor()
        {
            bool validInput;
            string userInput;
            string sensorsToMonitor = "";
            do
            {
                validInput = true;
                DisplayScreenHeader("Monitor Sensors");
                Console.Write("Which sensors do you wish to use to monitor? {Both, Left, Right}");
                userInput = Console.ReadLine().ToLower();
                if (userInput == "both")
                {
                    sensorsToMonitor = "both";
                }
                else if (userInput == "left")
                {
                    sensorsToMonitor = "left"
                }
                else if (userInput == "right")
                {
                    sensorsToMonitor = "right";
                }
                else
                {
                    Console.WriteLine("Please enter a valid response.");
                    DisplayContinuePrompt();
                    validInput = false;
                }
            } while (!validInput);
            Console.WriteLine(sensorsToMonitor + " will be monitored for this application.");
            DisplayMenuPrompt("Light Alarm Menu");
            return sensorsToMonitor;
        }

        static string LightAlarmDisplayInputRangeType()
        {
            bool validinput;
            bool minRes = false;
            string rangeType;
            string userInput;
            do
            {
                DisplayScreenHeader("Range-Type of Light Sensor");
                validinput = true;
                Console.Write("Range-Type: Minimum or Maximum");
                userInput = Console.ReadLine();
                if (userInput == "Maximum")
                {
                    minRes = false;
                }
                else if (userInput == "Minimum")
                {
                    minRes = true;
                }
                else
                {
                    Console.WriteLine("Please enter either minimum or maximum option.");
                    DisplayContinuePrompt();
                    validinput = false;
                }
            } while (!validinput);
            if (minRes == true)
            {
                rangeType = "Minimum";
            }
            else
            {
                rangeType = "Maximum";
            }
            Console.WriteLine("Range-Type is: " + rangeType);
            DisplayMenuPrompt("Light Alarm Menu");
            return rangeType;
        }

        static int LightAlarmDisplayInputMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            bool validinput;
            string userinput;
            int minMaxThresholdValue;

            do
            {
                DisplayScreenHeader("Minimum-Maximum Threshold Value");
                Console.WriteLine("Right-Light Sensor currently: " + finchRobot.getRightLightSensor());
                Console.WriteLine("Left-Light Sensor currently: " + finchRobot.getLeftLightSensor());
                Console.WriteLine(rangeType + " Light Sensor value: ");
                userinput = Console.ReadLine();
                int.TryParse(userinput, out minMaxThresholdValue);
                validinput = int.TryParse(userinput, out minMaxThresholdValue);
                if (!validinput)
                {
                    Console.WriteLine("Please enter a integer number.");
                    DisplayContinuePrompt();
                }
                else
                {
                    validinput = true;
                }
            } while (!validinput);
            Console.WriteLine(rangeType + "Threshold value is:" + minMaxThresholdValue);
            DisplayMenuPrompt("Light Alarm Menu");
            return minMaxThresholdValue;
        }

        static int LightAlarmDisplayInputTimeToMonitor()
        {
            bool validInput;
            string userInput;
            int timeToMonitor;
            do
            {
                DisplayScreenHeader("Monitor Duration");
                Console.WriteLine("Input the duration you wish to monitor (seconds):");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out timeToMonitor);
                validInput = int.TryParse(userInput, out timeToMonitor);
                if (!validInput)
                {
                    Console.WriteLine("Please enter an Integer number.");
                    DisplayContinuePrompt();
                }
                else
                {
                    validInput = true;
                }
            } while (!validInput);
            Console.WriteLine("For " + timeToMonitor + "the light will be monitored.");
            DisplayMenuPrompt("Light Alarm Menu");
            return timeToMonitor;
        }

        static void LightAlarmInputAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            bool isThresholdExceeded = false;
            int lightValueCurrent = 0;
            int timeSecElapsed = 0;

            DisplayScreenHeader("Setting the Alarm to Given Inputs");
            Console.WriteLine("Sensor/s to monitor: " + sensorsToMonitor);
            Console.WriteLine("Range-Type: " + rangeType);
            Console.WriteLine("Threshold Value: " + minMaxThresholdValue);
            Console.WriteLine("And the time to monitor: " + timeToMonitor);
            Console.WriteLine("Press any key to begin Light alarm monitoring.");
            Console.ReadKey();
            while ((timeSecElapsed < timeToMonitor) && !isThresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "Right":
                        lightValueCurrent = finchRobot.getRightLightSensor();
                        break;
                    case "Left":
                        lightValueCurrent = finchRobot.getLeftLightSensor();
                        break;
                    case "Both":
                        lightValueCurrent = (finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2;
                        break;
                }
                switch (rangeType)
                {
                    case "Minimum":
                        if (lightValueCurrent < minMaxThresholdValue)
                        {
                            isThresholdExceeded = true;
                        }
                        break;
                    case "Maximum":
                        if (lightValueCurrent > minMaxThresholdValue)
                        {
                            isThresholdExceeded = true;
                        }
                        break;
                }
                finchRobot.wait(650);
                timeSecElapsed++;
            }
            if (isThresholdExceeded)
            {
                Console.WriteLine(rangeType + " Threshold value of: " + minMaxThresholdValue + "has been exceeded by the value" + lightValueCurrent);
                finchRobot.noteOn(400);
                finchRobot.wait(500);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine(rangeType + " Threshold value of: " + minMaxThresholdValue + "was not exceeded by the value");
                DisplayMenuPrompt("Light Alarm");
            }
        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;
            double[] Temperatures = null;
            double[] FahrenheitTemp = null;
            double[] RightLightLevel = null;
            double[] LeftLightLevel = null;
            double dataPointFrequency = 0;
            int numberOfDataPoints = 0;
            bool quitDataMenu = false;
            string menuChoice;
            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;
                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;
                    case "c":
                        Temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        FahrenheitTemp = DataRecorderDisplayGetDataFahrenheit(numberOfDataPoints, dataPointFrequency, finchRobot);
                        RightLightLevel = DataRecorderDisplayGetDataRightLight(numberOfDataPoints, dataPointFrequency, finchRobot);
                        LeftLightLevel = DataRecorderDisplayGetDataLeftLight(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;
                    case "d":
                        DataRecorderDisplayData(Temperatures);
                        DataRecorderDisplayDataFahrenheit(FahrenheitTemp);
                        DataRecorderDisplayDataRightLight(RightLightLevel);
                        DataRecorderDisplayDataLeftLight(LeftLightLevel);
                        break;
                    case "q":
                        quitDataMenu = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a letter to go to it's menu.");
                        DisplayContinuePrompt();
                        break;

                }

            } while (!quitDataMenu);
        }

        /// ***********************************************************************
        /// *               Data Recorder > Tabulate Celcius Data                 *
        /// ***********************************************************************

        static void DataRecorderDisplayData(double[] Temperatures)
        {
            DisplayScreenHeader("Tabulate Celcisus Temps");
            Console.WriteLine("Recording #".PadLeft(20) +
                              "Temperature F".PadLeft(20));
            Console.WriteLine("***********".PadLeft(20) +
                              "***********".PadLeft(20));
            for (int index = 0; index < Temperatures.Length; index++)
            {
                Console.WriteLine((index + 1).ToString().PadLeft(20) + Temperatures[index].ToString("n2".PadLeft(20) + "F"));
            }
            DisplayContinuePrompt();
        }

        /// **************************************************************************
        /// *               Data Recorder > Tabulate Fahrenheit Data                 *
        /// **************************************************************************

        static void DataRecorderDisplayDataFahrenheit(double[] fahrenheitTemp)
        {
            DisplayScreenHeader("Tabulate Fahrenheit Temps");
            Console.WriteLine("Recording #".PadLeft(20) +
                              "Temperature F".PadLeft(20));
            Console.WriteLine("***********".PadLeft(20) +
                              "***********".PadLeft(20));
            for (int index = 0; index < fahrenheitTemp.Length; index++)
            {
                Console.WriteLine((index + 1).ToString().PadLeft(20) + fahrenheitTemp[index].ToString("n2".PadLeft(20) + "F"));
            }
            DisplayContinuePrompt();
        }

        /// *************************************************************************
        /// *               Data Recorder > Tabulate Right Light Data               *
        /// *************************************************************************

        static void DataRecorderDisplayDataRightLight(double[] rightLightLevel)
        {
            DisplayScreenHeader("Show Right light data");
            Console.WriteLine("Recording #".PadLeft(20) +
                              "Temperature C".PadLeft(20));
            Console.WriteLine("***********".PadLeft(20) +
                              "***********".PadLeft(20));
            for (int index = 0; index < rightLightLevel.Length; index++)
            {
                Console.WriteLine((index + 1).ToString().PadLeft(20) + rightLightLevel[index].ToString("n2".PadLeft(20)));
            }
            DisplayContinuePrompt();
        }

        /// ************************************************************************
        /// *               Data Recorder > Tabulate Left Light Data               *
        /// ************************************************************************

        static void DataRecorderDisplayDataLeftLight(double[] leftLightLevel)
        {
            DisplayScreenHeader("Show Left light data");
            Console.WriteLine("Recording #".PadLeft(20) +
                              "Temperature C".PadLeft(20));
            Console.WriteLine("***********".PadLeft(20) +
                              "***********".PadLeft(20));
            for (int index = 0; index < leftLightLevel.Length; index++)
            {
                Console.WriteLine((index + 1).ToString().PadLeft(20) + leftLightLevel[index].ToString("n2".PadLeft(20)));
            }
            DisplayContinuePrompt();

        }

        static double[] DataRecorderDisplayGetDataLeftLight(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] LeftLightLevel = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Left Light Data");
            Console.WriteLine("Inputted number of Data points and Frequency time: " + numberOfDataPoints + " : " + dataPointFrequency);
            Console.WriteLine("The application will begin recording the temperature of your room.");
            DisplayContinuePrompt();
            for (int index = 0; index < numberOfDataPoints; index++) ;
            {
                int index = 0;
                LeftLightLevel[index] = finchRobot.getLeftLightSensor();
                int dfwait = (int)(dataPointFrequency * 1000);
                finchRobot.wait(dfwait);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Attained Left Light Data");
            DisplayContinuePrompt();
            return LeftLightLevel;
        }

        static double[] DataRecorderDisplayGetDataRightLight(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] RightLightLevel = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Left Light Data");
            Console.WriteLine("Inputted number of Data points and Frequency time: " + numberOfDataPoints + " : " + dataPointFrequency);
            Console.WriteLine("The application will begin recording the temperature of your room.");
            DisplayContinuePrompt();
            for (int index = 0; index < numberOfDataPoints; index++) ;
            {
                int index = 0;
                RightLightLevel[index] = finchRobot.getRightLightSensor();
                int dfwait = (int)(dataPointFrequency * 1000);
                finchRobot.wait(dfwait);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Attained Right Light Data");
            DisplayContinuePrompt();
            return RightLightLevel;
        }

        /// ************************************************************************
        /// *               Data Recorder > Attain Fahrenheit Data                 *
        /// ************************************************************************

        static double[] DataRecorderDisplayGetDataFahrenheit(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] FahrenheitTemp = new double[numberOfDataPoints];
            DisplayScreenHeader("Attain Fahrenheit Data");
            Console.WriteLine("Inputted number of Data points and Frequency time: " + numberOfDataPoints + " : " + dataPointFrequency);
            Console.WriteLine("The application will begin recording the temperature of your room.");
            DisplayContinuePrompt();
            for (int index = 0; index < numberOfDataPoints; index++) ;
            {
                int index = 0;
                double tempin;
                double tempout;
                tempin = Convert.ToDouble(finchRobot.getTemperature());
                tempout = (tempin * 1.8) + 32;
                FahrenheitTemp[index] = tempout;
                int dfwait = (int)(dataPointFrequency * 1000);
                finchRobot.wait(dfwait);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Attained Fahrenheit Data");
            Console.WriteLine();
            Console.WriteLine("\t Table of found Celcius Temperatures");
            DataRecorderDisplayDataFahrenheit(FahrenheitTemp);
            DisplayContinuePrompt();
            return FahrenheitTemp;
        }

        /// *********************************************************************
        /// *               Data Recorder > Attain Celcius Data                 *
        /// *********************************************************************

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] Temperatures = new double[numberOfDataPoints];
            DisplayScreenHeader("Attain Data: Celcius");
            Console.WriteLine("Inputted number of Data points and Frequency time: " + numberOfDataPoints + " : " + dataPointFrequency);
            Console.WriteLine("The application will begin recording the temperature of your room.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++) ;
            {
                int index = 0;
                Temperatures[index] = finchRobot.getTemperature();
                int dfwait = (int)(dataPointFrequency * 1000);
                finchRobot.wait(dfwait);
            }
            DisplayContinuePrompt();
            DisplayScreenHeader("Attained Data");
            Console.WriteLine();
            Console.WriteLine("\t Table of found Celcius Temperatures");
            DataRecorderDisplayData(Temperatures);
            DisplayContinuePrompt();
            return Temperatures;
        }

        /// *************************************************************
        /// *               Data Recorder > Get Frequency                *
        /// *************************************************************

        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            string userinput;

            do
            {
                DisplayScreenHeader("Attain Data Point Frequency");
                Console.WriteLine();
                Console.WriteLine("Please give the frequency of the datapoints in seconds, from 0 to 100:");
                userinput = Console.ReadLine();
                double.TryParse(userinput, out dataPointFrequency);
                if (dataPointFrequency >= 0 && dataPointFrequency <= 100) ;
                {
                    Console.WriteLine("Your inputted frequency of data points in seconds: " + dataPointFrequency);
                    DisplayContinuePrompt();
                    return dataPointFrequency;
                }
                else
                {
                    Console.WriteLine("The value entered is not valid, please try again: ");
                    userinput = Console.ReadLine();
                    double.TryParse(userinput, out dataPointFrequency);
                    DisplayContinuePrompt();

                }


            }
            while (dataPointFrequency >= 0 && dataPointFrequency <= 100);
            double.TryParse(userinput, out dataPointFrequency);
            return dataPointFrequency;
        }

        /// *************************************************************
        /// *               Data Recorder > # of Points                 *
        /// *************************************************************

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string userInput;
            // validate input
            do
            {
                DisplayScreenHeader("Number of Data Points");
                Console.WriteLine();
                Console.Write("Please enter the number of data points between 0 and 100:");
                userInput = Console.ReadLine();
                int.TryParse(userInput, out numberOfDataPoints);
                if (numberOfDataPoints >= 0 && numberOfDataPoints <= 100)
                {
                    Console.WriteLine("Inputted integer: " + numberOfDataPoints);
                    DisplayContinuePrompt();
                    return numberOfDataPoints;
                }
                else
                {
                    Console.WriteLine("Invalid integer inputted, please meet the criteria.");
                    userInput = Console.ReadLine();
                    int.TryParse(userInput, out numberOfDataPoints);
                    DisplayContinuePrompt();
                }
            }
            while (numberOfDataPoints >= 0 && numberOfDataPoints <= 100);
            int.TryParse(userInput, out numberOfDataPoints);
            return numberOfDataPoints;
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing it Up");
                Console.WriteLine("\td) Song-bot");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMixingItUp(finchRobot);
                        break;

                    case "d":
                        TalentShowDisplaySongBot(finchRobot);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }
        /// *******************************************************
        /// *               Talent Show > Singing                 *
        /// *******************************************************
        static void TalentShowDisplaySongBot(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Finch Robot Singing");
            Console.WriteLine("The Finch Robot will begin singing a mongolian throat song.");
            DisplayContinuePrompt();
            finchRobot.noteOn(300);
            finchRobot.wait(400);
            finchRobot.noteOff();
            finchRobot.noteOn(400);
            finchRobot.wait(400);
            finchRobot.noteOff();
            finchRobot.noteOn(450);
            finchRobot.wait(400);
            finchRobot.noteOff();
            finchRobot.noteOn(200);
            finchRobot.wait(400);
            finchRobot.noteOff();
            finchRobot.noteOn(350);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            finchRobot.noteOn(450);
            finchRobot.wait(1000);
            finchRobot.noteOff();
            DisplayMenuPrompt("Talent Show Menu");
        }
        /// *****************************************************************
        /// *               Talent Show > Mixing it Up                *
        /// *****************************************************************
        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Finch Robot Mixing it Up");
            Console.WriteLine("The Finch Robot will simulate a small EDM concert: light, sound, and jamming.");
            DisplayContinuePrompt();
            for (int mixit = 0; mixit < 10; mixit++)
            {
                finchRobot.setLED(0, 255, 255);
                finchRobot.setMotors(100, 0);
                finchRobot.noteOn(88);
                finchRobot.wait(500);
                finchRobot.noteOff();
                finchRobot.setLED(0, 255, 0);
                finchRobot.setMotors(0, 100);
                finchRobot.noteOn(44);
                finchRobot.wait(500);
                finchRobot.noteOff();
                finchRobot.setLED(0, 0, 255);
                finchRobot.setMotors(0, -100);
                finchRobot.noteOn(132);
                finchRobot.wait(500);
                finchRobot.noteOff();
                finchRobot.setLED(255, 0, 0);
                finchRobot.setMotors(-100, 0);
                finchRobot.noteOn(100);
                finchRobot.wait(500);
                finchRobot.noteOff();
                finchRobot.setLED(255, 0, 255);
                finchRobot.setMotors(-100, 0);
                finchRobot.noteOn(77);
                finchRobot.wait(500);
                finchRobot.noteOff();
                finchRobot.setMotors(0, 0);
                finchRobot.setLED(0, 0, 0);
            }
            DisplayMenuPrompt("Talent Show Menu");
        }
        /// *****************************************************************
        /// *               Talent Show > Dancing                   *
        /// *****************************************************************
        static void TalentShowDisplayDance(Finch finchRobot)
        {
            Console.CursorVisible = false;
            DisplayScreenHeader("Finch Robot Dance");
            Console.WriteLine("The Finch Robot will begin dancing!");
            DisplayContinuePrompt();
            for (int jig = 0; jig < 10; jig++)
            {
                finchRobot.setMotors(-255, -255);
                finchRobot.wait(500);
                finchRobot.setMotors(255, 0);
                finchRobot.wait(500);
                finchRobot.setMotors(0, 0);
            }
            for (int jigtwo = 0; jigtwo < 8; jigtwo++)
            {
                finchRobot.setMotors(0, 255);
                finchRobot.wait(500);
                finchRobot.setMotors(255, 255);
                finchRobot.wait(500);
                finchRobot.setMotors(0, 0);
            }
            for (int jigthree = 0; jigthree < 7; jigthree++)
            {
                finchRobot.setMotors(125, -125);
                finchRobot.wait(500);
                finchRobot.setMotors(-125, 125);
                finchRobot.wait(500);
                finchRobot.setMotors(0, 0);
            }
            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(255, 0, 255);
                finchRobot.noteOn(255);
                finchRobot.wait(600);
                finchRobot.noteOff();
                finchRobot.setLED(0, 0, 255);
                finchRobot.noteOn(400);
                finchRobot.wait(600);
                finchRobot.noteOff();
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(300);
                finchRobot.wait(600);
                finchRobot.noteOff();
                finchRobot.setLED(255, 0, 0);
                finchRobot.noteOn(273);
                finchRobot.wait(600);
                finchRobot.noteOff();
                finchRobot.setLED(0, 255, 255);
                finchRobot.noteOn(125);
                finchRobot.wait(600);
                finchRobot.noteOff();
                DisplayContinuePrompt();
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}