using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Textreader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static bool first = true;
        public static bool first1 = true;
        public static bool first2 = true;
        public static bool eFirst = true;
        public static bool eFirst1 = true;
        public static bool eFirst2 = true;
        public static bool tFirst = true;
        public static bool tFirst1 = true;
        public static bool inputValid = true;
        public static bool valdiEmail = false;
        private string msgHeading = "temp";
        public bool length = false;
        public bool correctL = false;
        public List<string> smsInput = new List<string>();
        public string smsOutput = "";
        public string[][] abbreviations = new string[254][];
        public List<string> emailSubjectList = new List<string>();
        public List<string> emailContent = new List<string>();
        public List<string> sirList = new List<string>();
        public List<string> mentions = new List<string>();
        List<string> quarantineList = new List<string>();
        public Dictionary<string, string> hashtags = new Dictionary<string, string>();
        List<hashtag> items = new List<hashtag>();
        public MainWindow()
        {
            InitializeComponent();
            areaCode.Items.Add("AreaCode");
            areaCode.Items.Add("+44 (UK)");
            areaCode.Items.Add("+34 (ES)");
            areaCode.Items.Add("+86 (CN)");
            areaCode.SelectedIndex = 0;
        }

        private void smsSendbtn_Click(object sender, RoutedEventArgs e)
        {

            if (inputValid == true)
            {
                string smsBodyInput = "";
                string inputNumber = phoneNo.Text;
                string aCode = "";
                bool charCount = false;
                if (smsBodyInput.Length <= 140)
                {
                    charCount = true;
                }

                switch (areaCode.SelectedIndex)
                {
                    case 0:
                        goto default;
                    case 1:
                        aCode = "UK, +44";
                        Data ukLTest = new Data();
                        correctL = ukLTest.PhoneNumberUK(inputNumber);
                        break;
                    case 2:
                        aCode = "ES, +34";
                        Data BELTest = new Data();
                        correctL = BELTest.PhoneNumberES(inputNumber);
                        break;
                    default:
                        MessageBox.Show("Please select an area code");
                        break;

                }
                if (charCount == true)
                {
                    if (correctL == true)
                    {
                        smsBodyInput = smsBody.Text;
                        //split the sms word by word
                        Data seperateSMS = new Data();
                        smsInput = seperateSMS.Input(smsBodyInput);
                        //read in csv
                        Data readCSV = new Data();
                        abbreviations = readCSV.fileRead();
                        //abbreviation hunt
                        Data abvCheck = new Data();
                        smsOutput = abvCheck.abbreviations(smsInput, abbreviations);


                        //create file output for json
                        sms fileOut = new sms
                        {
                            MessageH = msgHeading,
                            SmsText = aCode,
                            SmsNo = inputNumber,
                            SmsBody = smsOutput

                        };

                        textBlock3.Text = messageHeader.Text;
                        textBlock4.Text = (aCode + inputNumber);
                        textBlock2.Text = smsOutput;

                        // serialize JSON directly to a file
                        using (StreamWriter file = File.AppendText(@"../../../output.json"))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(file, fileOut);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Number is not the correct length");
                    }
                }
                else
                {
                    MessageBox.Show("Message is too long");
                }
            }

        }

        private string url_search(string sentence)
        {
            try
            {
                foreach (string word in sentence.Split(' '))
                {
                    if (word.StartsWith("http:") || word.StartsWith("https:"))
                    {
                        string newM = sentence.Replace(word, "<URL Quarantined>");
                        sentence = newM;
                        foreach (string word1 in (sentence).Split(' '))
                        {
                            if (!quarantineList.Contains(word))
                            {
                                quarantineList.Add(word);
                                URLList.Items.Add(word);
                            }
                        }
                    }
                }
                return sentence;
            }
            catch
            {
                return sentence;
            }
        }



        private void emailSendBtn_Click(object sender, RoutedEventArgs e)
        {
            {
                string adrInput = senderEmail.Text;
                string subInput = subjectEmail.Text;
                string bdyInput = bodyEmail.Text;
                string sirSortCode = "";
                string sirNature = "";
                string output = "";
                List<string> subjectContent = new List<string>();
                List<string> bodyContent = new List<string>();
                Data split = new Data();
                bodyContent = split.Input(bdyInput);
                if (inputValid == false)
                {
                    MessageBox.Show("Message Heading is of invalid format");
                }
                else
                {
                    //validate email address
                    Boolean validE = false;
                    Boolean validSIR = false;
                    Data vEmail = new Data();

                    Data SIRv = new Data();
                    validE = vEmail.validateEmail(adrInput);
                    if (validE == false)
                    {
                        MessageBox.Show("Email Address is not valid, please input new email address");
                    }
                    else
                    {

                        //validate subject and body length
                        Boolean validLengths = false;
                        Data Lvalid = new Data();
                        Data listURL = new Data();
                        Data quarintene = new Data();
                        List<string> urlsQ = new List<string>();
                        validLengths = Lvalid.emailLengths(subInput, bdyInput);
                        subjectContent = split.Input(subInput);
                        if (validLengths == true)
                        {
                            validSIR = SIRv.SIRCheck(subjectContent);
                            //check if SIR
                            if (validSIR == false)
                            {
                                //if not a SIR
                                MessageBox.Show("Not SIR");
                                //create a list of urls and ouput without the quarentined ones
                                for (int i = 0; i < bodyContent.Count; i++)
                                {
                                    if (bodyContent[i].Contains("www.") || bodyContent[i].Contains("http://") || bodyContent[i].Contains("https://"))
                                    {
                                        output += " < URL Quarentined >";
                                        URLList.Items.Add(bodyContent[i]);
                                    }
                                    else
                                    {
                                        output += bodyContent[i];
                                        output += " ";
                                    }

                                }
                                //output to the text boxes
                                emailBody.Text = output;
                                emailSubject.Text = subInput;
                                emailSender.Text = adrInput;
                                //change size ot screen


                                //create a new objct to output
                                email fileOut = new email
                                {
                                    EmailHeader = msgHeading,
                                    EmailAddress = adrInput,
                                    EmailBody = output,
                                    EmailText = subInput
                                };

                                // serialize JSON to a file
                                using (StreamWriter file = File.AppendText(@"../../../output.json"))
                                {
                                    JsonSerializer serializer = new JsonSerializer();
                                    serializer.Serialize(file, fileOut);
                                }


                            }
                            else
                            {
                                //decides if its a serious incident
                                MessageBox.Show("Serious Incident Report");
                                Data getNature = new Data();
                                Data getSort = new Data();
                                //checks the sir nature and sort cde
                                sirNature = getNature.CNature(bodyContent);
                                sirSortCode = getNature.CSort(bodyContent);
                                if (sirNature != null && sirSortCode != null)
                                {
                                    SIRlistbox.Items.Add(sirSortCode);
                                    SIRlistbox.Items.Add(sirNature);
                                    sirList.Add(sirNature + sirSortCode);

                                    for (int i = 0; i < bodyContent.Count; i++)
                                    {
                                        if (bodyContent[i].Contains("www.") || bodyContent[i].Contains("http://") || bodyContent[i].Contains("https://"))
                                        {
                                            output += " < URL Quarentined >";
                                            URLList.Items.Add(bodyContent[i]);
                                        }
                                        else
                                        {
                                            output += bodyContent[i];
                                            output += " ";
                                        }


                                        emailBody.Text = output;
                                        emailSubject.Text = subInput;
                                        emailSender.Text = adrInput;



                                        //create a new objct to output
                                        email sirOut = new email
                                        {
                                            EmailHeader = msgHeading,
                                            EmailAddress = adrInput,
                                            EmailBody = output,
                                            EmailText = subInput
                                        };
                                        // serialize JSON to a file
                                        using (StreamWriter file = File.AppendText(@"../../../output.json"))
                                        {
                                            JsonSerializer serializer = new JsonSerializer();
                                            serializer.Serialize(file, sirOut);
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void twitterSendBtn_Click(object sender, RoutedEventArgs e)
        {
           

            string twitterOutput = "";
          
            //clear mentions list box
            mentionList.Items.Clear();
            items.Clear();
            //check handle "@"    
            string inputHandle = tweetSender.Text;
            string inputBody = tweetBody.Text;
            listView.Visibility = Visibility.Visible;
            //read in abreviatons csv


            Data readCSV = new Data();
            abbreviations = readCSV.fileRead();

            char c = inputHandle[0];
            if (c.Equals('@'))
            {
                //check handle length
                if (inputHandle.Length > 15)
                {
                    MessageBox.Show("The twitter handle is too many characters");
                }
                else
                {
                    //check tweet lengths
                    if (inputBody.Length > 140)
                    {
                        MessageBox.Show("The twitter body is too long");
                    }
                    else
                    {
                        //split string into array
                        Data splitString = new Data();
                        List<string> bodyArray = new List<string>();
                        bodyArray = splitString.Input(inputBody);


                        //find mentions
                        foreach (string temp in bodyArray)
                        {
                            try
                            {
                                if (temp[0].Equals('@'))
                                {
                                    mentions.Add(temp);
                                    mentionList.Items.Add(temp);
                                }
                            }
                            catch
                            {

                            }

                        }
                        //find hashtags
                        items.Clear();
                        foreach(string tag in bodyArray)
                        {
                            int counter = 0;
                            if (tag[0].Equals('#'))
                            {
                                try
                                {
                                    hashtags.Add(tag, "1");
                                }
                                catch
                                {
                                    counter = Int32.Parse(hashtags[tag]);
                                    counter++; ;
                                    string counterString = counter.ToString();
                                    hashtags[tag] = counterString;
                                }
                            }
                        }
                        foreach (KeyValuePair<string, string> kv in hashtags)
                        {
                            items.Add(new hashtag { Hash = kv.Key, TagCounter = kv.Value });
                        }


                        listView.ItemsSource = items;
                        listView.Items.Refresh();



                        //FILE output for hashtags
                        //create a new objct to output for each item in the dictionary
                        foreach (KeyValuePair<string, string> kv in hashtags)
                        {
                            hashtag hashOut = new hashtag
                            {
                                Hash = kv.Key,
                                TagCounter = kv.Value
                            };
                            // serialize JSON directly to a file
                            using (StreamWriter file = File.CreateText(@"../../../hashtags.json"))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Serialize(file, hashOut);
                            }

                        }

                        //abbreviation hunt
                        Data abvCheck = new Data();
                        twitterOutput = abvCheck.abbreviations(bodyArray, abbreviations);

                        //output

                        label6.Visibility = Visibility.Visible;
                        mentionList.Visibility = Visibility.Visible;
                        
                        
                        emailSubject.Text = (inputHandle);
                        emailBody.Text = twitterOutput;

                        //create a new objct to output for each item in the dictionary
                        twitter fileOut = new twitter
                        {
                            TweetHeading = msgHeading,
                            TwitterHandle = inputHandle,
                            TweetBody = inputBody

                        };
                        // serialize JSON directly to a file
                        using (StreamWriter file = File.AppendText(@"../../../output.json"))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            serializer.Serialize(file, fileOut);
                        }


                    }
                }
            }
            else
            {
                MessageBox.Show("Twitter Handle is incorrect format");
            }
        }
        }
    }


