﻿
using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Ex03.ConsoleUI
{
    public class UIManagar
    {
        public void InitilizeUIManager()
        {
            startGarage();
        }
        private void startGarage()
        {
            bool isGarageOpen = true;
            Garage garage = new Garage();

            while (isGarageOpen)
            {
                showMenu();
               int userMenuChoice = (int)getAndValidateInputFromUserInRange(1,8);
               executeUserChoice(userMenuChoice, ref isGarageOpen,garage);
            }
        }
        private void showMenu()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            string title = "== Garage Menu ==";
            string border = new string('=', title.Length);
            Console.ForegroundColor = ConsoleColor.White;

            menu.AppendLine(border);
            menu.AppendLine(title);
            menu.AppendLine(border);
            Console.ForegroundColor = ConsoleColor.White; 
            menu.AppendLine(string.Format("{0, -2} {1}", "1.", "Put Your Vehicle in The Garage."));
            menu.AppendLine(string.Format("{0, -2} {1}", "2.", "Display All License Numbers of Vehicles in The Garage."));
            menu.AppendLine(string.Format("{0, -2} {1}", "3.", "Update The Status of a Vehicle In The Garage."));
            menu.AppendLine(string.Format("{0, -2} {1}", "4.", "Inflate Wheels of a Chosen Vehicle To The Maximum Capacity."));
            menu.AppendLine(string.Format("{0, -2} {1}", "5.", "Fuel a Vehicle."));
            menu.AppendLine(string.Format("{0, -2} {1}", "6.", "Charge a Vehicle."));
            menu.AppendLine(string.Format("{0, -2} {1}", "7.", "Display Vehicle Information by License Number"));
            menu.AppendLine(string.Format("{0, -2} {1}", "8.", "Exit Garage."));
            menu.AppendLine(border);
            Console.Write(menu.ToString());
            printInputRequest();
            Console.ResetColor();
        }
        private void displayTypeOfVehicleMenu()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            string title = "==== Choose Vehicle Type ====";
            string border = new string('=', title.Length);

            menu.AppendLine(border);
            menu.AppendLine(title);
            menu.AppendLine(border);
            menu.AppendLine(string.Format("{0, -2} {1}", "1.", "Fuel Motorcycle"));
            menu.AppendLine(string.Format("{0, -2} {1}", "2.", "Electric Motorcycle"));
            menu.AppendLine(string.Format("{0, -2} {1}", "3.", "Fuel Car"));
            menu.AppendLine(string.Format("{0, -2} {1}", "4.", "Electric Car"));
            menu.AppendLine(string.Format("{0, -2} {1}", "5.", "Truck"));
            menu.AppendLine(border);
            Console.Write(menu.ToString());
            printInputRequest();
            Console.ResetColor(); 
        }
        private int showMenuOfOptionToFilterCarsInGarage()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            string title = "==== License Plate Filter ====";
            string border = new string('=', title.Length);

            menu.AppendFormat("{0}{1}", border, Environment.NewLine);
            menu.AppendFormat("{0}{1}", title, Environment.NewLine);
            menu.AppendFormat("{0}{1}", border, Environment.NewLine);
            menu.AppendFormat("Displaying License Numbers Using The Following Filter:{0}{1}", Environment.NewLine, Environment.NewLine);
            menu.AppendFormat("{0, -2} {1}{2}", "1.", "Filter by Current Status in The Garage.", Environment.NewLine);
            menu.AppendFormat("{0, -2} {1}{2}", "2.", "Don't Use Filtering.", Environment.NewLine);
            menu.AppendFormat("{0}{1}", border, Environment.NewLine);
            Console.Write(menu.ToString());
            printInputRequest();
            Console.ResetColor();
            int userChoice = (int)getAndValidateInputFromUserInRange(1, 2);

            return userChoice;
        }

        private void displayFuelMenu()
        {
            Console.Clear();
            StringBuilder menu = new StringBuilder();
            string title = "==== Choose A Fuel ====";
            string border = new string('=', title.Length);

            menu.AppendLine(border);
            menu.AppendLine(title);
            menu.AppendLine(border);
            menu.AppendLine(string.Format("{0, -2} {1}", "1.", "Octan95"));
            menu.AppendLine(string.Format("{0, -2} {1}", "2.", "Octan96"));
            menu.AppendLine(string.Format("{0, -2} {1}", "3.", "Octan98"));
            menu.AppendLine(string.Format("{0, -2} {1}", "4.", "Soler"));
            menu.AppendLine(border);
            Console.Write(menu.ToString());
            printInputRequest();
            Console.ResetColor(); 
        }

        private float getAndValidateInputFromUserInRange(float i_MinValue, float i_MaxValue)
        {
            string userInputChoice = Console.ReadLine();
            float userInputNumerical = 0;

            while (!float.TryParse(userInputChoice, out userInputNumerical) ||
                   ValueOutOfRangeException.IsValueOutOfRange(userInputNumerical, i_MinValue, i_MaxValue))
            {
                string messageOutOfRange =
                    string.Format("Your Choice is out of range! Enter input again in the range {0} - {1}:",
                        i_MinValue, i_MaxValue);
                Console.WriteLine(messageOutOfRange);
                userInputChoice = Console.ReadLine();
            }

            return userInputNumerical;
        }

        private void executeUserChoice(int i_UserChoice, ref bool io_IsGarageOpen, Garage i_Garage)
        {
            Console.Clear();
            io_IsGarageOpen = true;
            
            switch (i_UserChoice)
            {
                case 1:
                    getVehicleStatusAndOwnerDetails(i_Garage);
                    break;
                case 2:
                    showLicenseNumbersInGarage((int)showMenuOfOptionToFilterCarsInGarage(), i_Garage);
                    break;
                case 3:
                    updateStatusOfVehicle(i_Garage);
                    break;
                case 4:
                    inflateVehicleWheels(i_Garage);
                    break;
                case 5:
                    powerUpVehicle(i_Garage, Engine.eEngineType.Fuel);
                    break;
                case 6:
                    powerUpVehicle(i_Garage, Engine.eEngineType.Battery);
                    break;
                case 7:
                    displayVehicleInformation(i_Garage);
                    break;
                case 8:
                    Console.Clear();
                    io_IsGarageOpen = false;
                    exitingScreen(out io_IsGarageOpen);
                    break;
            }
        }
        
        private void displayVehicleInformation(Garage i_Garage)
        {
            Console.WriteLine(@"Display Vehicle Information From The Garage By License Number:{0}", Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    Garage.VehicleStatusAndOwnerDetails userVehicle = i_Garage.GetVehicleByLicenseNumber(licenseNumber);
                    Console.WriteLine(userVehicle.ToString());
                    displayBackToMenuOption();
                    userChoice = 2;
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }

        private void getVehicleStatusAndOwnerDetails(Garage i_Garage)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@" === Vehicle Is Being Added To The Garage === {0}", Environment.NewLine);
            Garage.VehicleStatusAndOwnerDetails vehicleStatusAndOwnerDetails = null;
            string licenseNumber = getVehicleLicenseNumberFromUser();

            if (i_Garage.VehiclesOfGarage.TryGetValue(licenseNumber, out vehicleStatusAndOwnerDetails))
            {
                Console.WriteLine("The Entered License Number Already Exist. Status Changed To - Under Repair.");
                vehicleStatusAndOwnerDetails.VehicleStatus = Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus.UnderRepair;
            }
            else
            {
                displayTypeOfVehicleMenu();
                VehicleAllocator.eVehicleType vehicleType = (VehicleAllocator.eVehicleType)getAndValidateInputFromUserInRange(1, 5);
                Vehicle newVehicle = VehicleAllocator.AllocateVehicle(vehicleType, licenseNumber);
                vehicleStatusAndOwnerDetails = setInformationForVehicle(newVehicle, vehicleType);
                i_Garage.VehiclesOfGarage.Add(licenseNumber, vehicleStatusAndOwnerDetails);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vehicle Added Successfully.");
                Console.ResetColor();
            }
            
            displayBackToMenuOption();
        }

        private void askForFuelOctanAndFuelAmountToFill(
            out int o_FuelChoice,
            out float o_FuelAmountToAdd,
            Engine.eEngineType i_EngineType)
        {
            o_FuelChoice = 0;

            if(i_EngineType == Engine.eEngineType.Fuel)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Enter Which Octan You'd Like:");
                displayFuelMenu();
                o_FuelChoice = (int)getAndValidateInputFromUserInRange(1, 4);
            }

            Console.WriteLine("Enter how much you'd like to add to you vehicle: ");
            o_FuelAmountToAdd = getAndValidateInputFromUserInRange(0, 120);
        }

        private string getAndValidateStringInputOfDigitsAndLetters(string i_ErrorMessage)
        {
            string userInput = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine(i_ErrorMessage);
                userInput = Console.ReadLine();

            }

            return userInput;
        }

        private void printInputRequest()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("Enter Your Choice: ");
            Console.ResetColor();
        }

        private void displayBackToMenuOption()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Press Any Key To Get Back To Menu");
            Console.ReadKey();
            Console.ResetColor();
        }

        private string getAndValidatePositiveNumberInput(string i_Message)
        {
            Console.Write(i_Message);
            string userInput = Console.ReadLine();
            int numberUserInput;

            while (!int.TryParse(userInput, out numberUserInput) || numberUserInput < 1)
            {
                Console.WriteLine("Enter a Positive Number. Try Again: ");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private string getVehicleLicenseNumberFromUser()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter The License Number Of The Vehicle: ");
            string licenseNumber = getAndValidateStringInputOfDigitsAndLetters("License Number Format Invalid. Try Again.");

            return licenseNumber;
        }

        private Garage.VehicleStatusAndOwnerDetails setInformationForVehicle(
            Vehicle i_Vehicle,
            VehicleAllocator.eVehicleType i_VehicleType)
        {
            string manufactorName;
            float currentAirPressure, currentEnergy;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"==== VEHICLE INFORMATION ENTRY ===={0}", Environment.NewLine);
            Console.WriteLine("Answer The Questions To Set The Vehicle Information");
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter The Model Name: ");
            i_Vehicle.ModelName = getAndValidateStringInputOfDigitsAndLetters("Model Name Format Invalid. Try Again.");
            Console.Write("Enter Wheels Manufacturer Name: ");
            manufactorName = getAndValidateStringInputOfDigitsAndLetters("Manufacturer Name Invalid. Try Again. ");
            Console.Write("Enter Wheels Remaining Air Pressure: ");
            currentAirPressure = getAndValidateInputFromUserInRange(0, (int)i_Vehicle.Wheels.First().MaxAirPressure);
            Console.Write("Enter Remaining Power Left In Vehicle: ");
            if(i_Vehicle.Engine is Engine.FuelBasedEngine)
            {
                currentEnergy = getAndValidateInputFromUserInRange(
                    0,
                    (i_Vehicle.Engine as Engine.FuelBasedEngine).MaxAmountOfFuel);
            }
            else
            {
                currentEnergy = getAndValidateInputFromUserInRange(
                    0,
                    (i_Vehicle.Engine as Engine.ElectricBasedEngine).BatteryCapacity);
            }

            i_Vehicle.SetPowerLevelLeft(currentEnergy);
            i_Vehicle.SetPowerPercentageLeft();
            getVehicleOwnerData(out string o_OwnerName, out string o_OwnerPhoneNumber);
            i_Vehicle.SetVehicleWheels(manufactorName, currentAirPressure);
            correctAndSetVehicleInformation(ref i_Vehicle, i_VehicleType);
            Console.ResetColor();

            return new Garage.VehicleStatusAndOwnerDetails(o_OwnerName, o_OwnerPhoneNumber, i_Vehicle);
        }

        private void getVehicleOwnerData(out string o_OwnerName, out string o_OwnerPhoneNumber)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter Your name:");
            o_OwnerName = getAndValidateStringInputOfDigitsAndLetters("Invalid Name Format. Try Again");
            o_OwnerPhoneNumber = getAndValidatePositiveNumberInput("Enter Your Phone Number: ").ToString();
        }

        private void correctAndSetVehicleInformation(ref Vehicle io_Vehicle, VehicleAllocator.eVehicleType i_TypeOfVehicle)
        {
            List<string> queries = VehicleAllocator.GetAndSetVehicleQueries(i_TypeOfVehicle, io_Vehicle);
            List<string> userResponses = getResponsesAboutVehicle(queries);
            bool isWrongAnswer = true;

            while (isWrongAnswer)
            {
                try
                {
                    VehicleAllocator.SetResponsesForVehicle(i_TypeOfVehicle, io_Vehicle, userResponses);
                    isWrongAnswer = false;
                }
                catch (Exception exception)
                {
                    if (exception is FormatException || exception is ValueOutOfRangeException || exception is ArgumentException)
                    {
                        while (exception != null)
                        {
                            Console.Write(exception.Message);
                            userResponses[int.Parse(exception.Source)] = Console.ReadLine();
                            exception = exception.InnerException;
                        }
                    }
                    else
                    {
                        throw exception;
                    }
                }
            }
        }

        private void inflateVehicleWheels(Garage i_Garage)
        {
            Console.WriteLine(@"Inflating Air Pressure To Maximum{0}", Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    i_Garage.FillWheelsToMaxAirPressureByLicenseNumber(licenseNumber);
                    userChoice = 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Wheels Filled Successfully.");
                    displayBackToMenuOption();
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }

        private void showLicenseNumbersInGarage(int i_UserFilterChoice, Garage i_Garage)
        {
            bool isFiltered = false;
            Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus status = Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus.UnderRepair;

            if (i_UserFilterChoice == 1)
            {
                status = getStatusOfVehicle();
                isFiltered = true;
            }

            string licensePlatesByStatus = i_Garage.GetListOfLicenseNumberByStatus(status, isFiltered);
            if (licensePlatesByStatus.Length != 0)
            {
                Console.WriteLine(licensePlatesByStatus);
            }
            else
            {
                Console.WriteLine("Vehicles With the Requested Status Do Not Exist Right Now.");
            }

            displayBackToMenuOption();
        }

        
        private List<string> getResponsesAboutVehicle(List<string> i_Questions)
        {
            List<string> answers = new List<string>(i_Questions.Capacity);

            foreach (string question in i_Questions)
            {
                Console.Write(question);
                answers.Add(Console.ReadLine());
            }

            return answers;
        }

        private Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus getStatusOfVehicle()
        {
            Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus status;
            Console.Write(@"Choose Vehicle Status: 
1. Under repair
2. Repaired
3. Paid
Choice: ");
            int chosenVehicleStatus = (int)getAndValidateInputFromUserInRange(1, 3);
            status = (Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus)chosenVehicleStatus;

            return status;
        }

        private void updateStatusOfVehicle(Garage i_Garage)
        {
            Console.WriteLine("Updating Vehicle Status...{0}", Environment.NewLine);
            int userChoice = 1;

            while (userChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    i_Garage.GetVehicleByLicenseNumber(licenseNumber);
                    Garage.VehicleStatusAndOwnerDetails.eVehicleGarageStatus newStatus = getStatusOfVehicle();
                    i_Garage.SetVehicleGarageStatus(licenseNumber, newStatus);
                    userChoice = 2;
                    Console.WriteLine("Vehicle Status Changed Successfully!");
                    displayBackToMenuOption();
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                    userChoice = showBadInputMessageTryAgainAndGetMenu();
                }
            }
        }

        private int showBadInputMessageTryAgainAndGetMenu()
        {
            Console.WriteLine(@"Choose From The Following Options: 

1. Enter Input 
2. Back To Menu
Enter Your Choice: ");
            int userChoice = (int)getAndValidateInputFromUserInRange(1, 2);

            return userChoice;
        }

        private void powerUpVehicle(Garage i_Garage, Engine.eEngineType i_EngineType)
        {
            int userMenuChoice = 1;

            while (userMenuChoice == 1)
            {
                try
                {
                    string licenseNumber = getVehicleLicenseNumberFromUser();
                    Garage.VehicleStatusAndOwnerDetails vehicleToPowerUp = i_Garage.GetVehicleByLicenseNumber(licenseNumber);
                    askForFuelOctanAndFuelAmountToFill(out int userFuelTypeChoice, out float quantityToFuelOrCharge, i_EngineType);
                    i_Garage.FuelOrChargeVehicle(i_EngineType, licenseNumber, quantityToFuelOrCharge, (Engine.FuelBasedEngine.eFuelOctan)userFuelTypeChoice, vehicleToPowerUp);
                    userMenuChoice = 2;
                    Console.WriteLine("Engine of Vehicle Successfully Filled Up!");
                    displayBackToMenuOption();
                    break;
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    if (exception.MinValue != exception.MaxValue)
                    {
                        Console.WriteLine("Value Ranges Are {0} to {1}", exception.MinValue, exception.MaxValue);
                    }
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                userMenuChoice = showBadInputMessageTryAgainAndGetMenu();
            }
        }


        private void exitingScreen(out bool o_IsGargeOpen)
        {
            Console.Write("Closing Garage");
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(800);
                Console.Write('.');
            }

            Console.WriteLine("");
            Console.WriteLine("Garage Is Closed. Have A Great Day!");
            o_IsGargeOpen = false;
        }

    }
}
