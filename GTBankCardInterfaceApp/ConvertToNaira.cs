using Microsoft.VisualBasic;

namespace GTBankCardInterfaceApp
{
    class ConvertToNaira
    {
        private string ConvertDigit(string MyDigit)
        {
            string functionReturnValue = null;

            switch (MyDigit)
            {
                case "1":
                    functionReturnValue = "One";
                    break;
                case "2":
                    functionReturnValue = "Two";
                    break;
                case "3":
                    functionReturnValue = "Three";
                    break;
                case "4":
                    functionReturnValue = "Four";
                    break;
                case "5":
                    functionReturnValue = "Five";
                    break;
                case "6":
                    functionReturnValue = "Six";
                    break;
                case "7":
                    functionReturnValue = "Seven";
                    break;
                case "8":
                    functionReturnValue = "Eight";
                    break;
                case "9":
                    functionReturnValue = "Nine";
                    break;
                default:
                    functionReturnValue = string.Empty;
                    break;
            }
            return functionReturnValue;
        }
        private string ConvertTens(string MyTens)
        {
            string Result = string.Empty;

            if (MyTens.Substring(0,1).Equals("1"))
            {
                switch (MyTens)
                {
                    case "10":
                        Result = "Ten";
                        break;
                    case "11":
                        Result = "Eleven";
                        break;
                    case "12":
                        Result = "Twelve";
                        break;
                    case "13":
                        Result = "Thirteen";
                        break;
                    case "14":
                        Result = "Fourteen";
                        break;
                    case "15":
                        Result = "Fifteen";
                        break;
                    case "16":
                        Result = "Sixteen";
                        break;
                    case "17":
                        Result = "Seventeen";
                        break;
                    case "18":
                        Result = "Eighteen";
                        break;
                    case "19":
                        Result = "Nineteen";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (MyTens.Substring(0,1))
                {
                    case "2":
                        Result = "Twenty ";
                        break;
                    case "3":
                        Result = "Thirty ";
                        break;
                    case "4":
                        Result = "Forty ";
                        break;
                    case "5":
                        Result = "Fifty ";
                        break;
                    case "6":
                        Result = "Sixty ";
                        break;
                    case "7":
                        Result = "Seventy ";
                        break;
                    case "8":
                        Result = "Eighty ";
                        break;
                    case "9":
                        Result = "Ninety ";
                        break;
                    default:
                        break;
                }

                Result = Result + ConvertDigit(MyTens.Substring(1, 1));
            }
            return Result;
        }
        private string ConvertHundreds(string MyNumber)
        {
            string Result = string.Empty;

            MyNumber = Strings.Right("000" + MyNumber, 3);

            if (Strings.Left(MyNumber, 1) != "0")
            {
                if (Strings.Mid(MyNumber, 2, 1) != "0")
                {
                    Result = ConvertDigit(Strings.Left(MyNumber, 1)) + " Hundred and ";
                }
                else
                {
                    Result = ConvertDigit(Strings.Left(MyNumber, 1)) + " Hundred ";
                }
            }

            if (Strings.Mid(MyNumber, 2, 1) != "0")
            {
                Result = Result + ConvertTens(Strings.Mid(MyNumber, 2));
            }
            else
            {
                Result = Result + ConvertDigit(Strings.Mid(MyNumber, 3));
            }

            return Strings.Trim(Result);
        }
        public string Num2Naira(string MyNumber,string currency,string subcurrency)
        {
            //Try
            string Temp = null;
            string Naira = string.Empty;
            string Kobo = string.Empty;
            int Decimalplace = 0;
            int Count = 0;

            string[] Place = new string[10];
            Place[2] = " Thousand ";
            Place[3] = " Million ";
            Place[4] = " Billion ";
            Place[5] = " Trillion ";

            // Convert MyNumber to a string, trimming extra spaces.
            MyNumber = Strings.Trim(MyNumber);

            // Find decimal place.
            Decimalplace = Strings.InStr(MyNumber, ".",CompareMethod.Text);

            // If we find decimal place...
            if (Decimalplace > 0)
            {
                // Convert kobo
                Temp = Strings.Left(Strings.Mid(MyNumber, Decimalplace + 1) + "00", 2);
                Kobo = ConvertTens(Temp);

                // Strip off kobo from remainder to convert.
                MyNumber = Strings.Trim(Strings.Left(MyNumber, Decimalplace - 1));
            }

            Count = 1;
            while (MyNumber != string.Empty)
            {
                // Convert last 3 digits of MyNumber to English Naira.
                Temp = ConvertHundreds(Strings.Right(MyNumber, 3));
                if (Temp != string.Empty)
                    Naira = Temp + Place[Count] + Naira;
                if (Strings.Len(MyNumber) > 3)
                {
                    // Remove last 3 converted digits from MyNumber.
                    MyNumber = Strings.Left(MyNumber, Strings.Len(MyNumber) - 3);
                }
                else
                {
                    MyNumber = string.Empty;
                }
                //Count = Count + 1
                Count += 1;
            }

            // Clean up Naira.
            switch (Naira)
            {
                case "":
                    Naira = currency;
                    //"No Naira"
                    break;
                case "One":
                    Naira = "One "+ currency;
                    break;
                default:
                    Naira = Naira + " " + currency;
                    break;
            }

            // Clean up kobo.
            switch (Kobo)
            {
                case "":
                    Kobo = " Only.";
                    break;
                case "One":
                    Kobo = " One "+ subcurrency +" Only.";
                    break;
                default:
                    Kobo = " " + Kobo + " "+ subcurrency +".";
                    break;
            }

            return Naira + Kobo;
        }
    }
}