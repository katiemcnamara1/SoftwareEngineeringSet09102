using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Textreader
{
    class Data
    {

        public string[][] fileRead()
        {
            var readL = new List<string[]>();
            int row = 0;
            string filePath = @"textwords.csv";
            StreamReader sr = new StreamReader(filePath);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(',');
                readL.Add(line);
                row++;

            }

            var Output = readL.ToArray();
            return Output;
        }

        //checking UK number length 
        public Boolean PhoneNumberUK(string phoneNumberUK)
        {
            if (phoneNumberUK.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> Input(string input)
        {
            List<string> Output = new List<string>(input.Split(null));
            return Output;
        }

        //checking ES number length 
        public Boolean PhoneNumberES(string phoneNumberES)
        {
            if (phoneNumberES.Length == 9)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //checking Chinese number length 
        public Boolean phoneNumberChina(string phoneNumberChina)
        {
            if (phoneNumberChina.Length == 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string abbreviations(List<String> Input, string[][] abbreviations)
        {
            bool test = false;
            string output = "";
            foreach (var i in Input)
            {
                test = true;
                var temp = i[0];
                string y = "";
                if (char.IsUpper(temp))
                {
                    for (int j = 0; j < abbreviations.Length; j++)
                    {
                        y = abbreviations[j][0];
                        if (i.Equals(y))
                        {
                            output += " <" + abbreviations[j][1] + "> ";

                        }
                        else if (test == true)
                        {
                            output += i;
                            output += " ";
                            test = false;
                        }

                    }

                }
                else
                {
                    output += i;
                    output += " ";
                }
            }
            return output;
        }
        
        public Boolean validD(string x)
        {
            try
            {
                DateTime dt = DateTime.Parse(x);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //validate lengths of stuff
        public Boolean emailLengths(string subject, string body)
        {
            if (subject.Length > 20)
            {
                MessageBox.Show("Email Subject is too long, please revise");
                return false;
            }
            else
            {
                //validate body length
                if (body.Length > 1028)
                {
                    MessageBox.Show("Email Body is too long, please retry");
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
        //validates SIR
        public bool SIRCheck(List<string> input)
        {
            if (input.Count == 2)
            {
                if (input[0].Equals("SIR"))
                {
                    try
                    {
                        DateTime dt = DateTime.Parse(input[1]);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
        //vaidates email
        public Boolean validateEmail(string input)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(input);
                return addr.Address == input;
            }
            catch
            {
                return false;
            }
        }
        
        //valiates sort code
        public string CSort(List<string> x)
        {
            bool temp = false;
            string sc = x[0] + " " + x[1];
            if (sc.Equals("Sort Code:"))
            {
                if (Regex.Match(x[2], @"\b([0-9]{2})-([0-9]{2})-([0-9]{2})\b").Success)
                {
                    temp = true;
                    if (temp == true)
                    {
                        return x[2];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("Sort Code is Invalid");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Text 'Sort Code' is Invalid");
                return null;

            }
        }

        //validates Nature
        public string CNature(List<String> x)
        {
            int temp = 0;
            List<string> incidents = new List<string>();
            incidents.Add("Theft");
            incidents.Add("Intelligence");
            incidents.Add("Terrorism");
            incidents.Add("Raid");
            incidents.Add("ATM");//Theft
            incidents.Add("Customer");//attack
            incidents.Add("Staff");//abuse
            incidents.Add("Bomb");//threat
            incidents.Add("Staff");//attack
            incidents.Add("Suspicious");//incident
            incidents.Add("Cash");//loss
            string noi = x[3] + " " + x[4] + " " + x[5];
            if (noi.Equals("Nature of Incident:") || noi.Equals("Nature Of Incident:"))
            {
                for (int i = 0; i < incidents.Count; i++)
                {
                    if (x[6].Equals(incidents[i]))
                    {
                        temp = i;
                    }
                }
                if (temp < 4)
                {
                    return incidents[temp];
                }
                else if ((temp == 4) && (x[7].Equals("Theft")))
                {
                    return (incidents[temp] + "Theft");
                }
                else if ((temp == 5) && (x[7].Equals("Attack")))
                {
                    return (incidents[temp] + "Attack");
                }
                else if ((temp == 6) && (x[7].Equals("Abuse")))
                {
                    return (incidents[temp] + "Abuse");
                }
                else if ((temp == 7) && (x[7].Equals("Threat")))
                {
                    return (incidents[temp] + "Threat");
                }
                else if ((temp == 8) && (x[7].Equals("Attack")))
                {
                    return (incidents[temp] + "Attack");
                }
                else if ((temp == 9) && (x[7].Equals("Incident")))
                {
                    return (incidents[temp] + "Incident");
                }
                else if ((temp == 10) && (x[7].Equals("Loss")))
                {
                    return (incidents[temp] + "Loss");
                }
                else
                {
                    MessageBox.Show("Nature is incorrect");
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        
        //creates a list of urls quarinteened
        public List<String> urlList(List<string> input)
        {
            List<string> output = new List<string>();
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].Contains("www.") || input[i].Contains("http://") || input[i].Contains("https://"))
                {
                    output.Add(input[i]);
                }
            }
            return output;
        }

        //quarintines urls
        


        









    }
}
