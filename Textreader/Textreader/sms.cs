using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textreader
{
    public class sms
    {
        private string messageH;
        private string smsText;
        private string smsNo;
        private string smsBody;


        public string SmsBody
        {
            get { return smsBody; }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("");
                }

                smsBody = value;
            }
        }


        public string SmsText
        {
            get { return smsText; }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("");
                }

                smsText = value;
            }
        }

        public string MessageH
        {
            get { return messageH; }
            set
            {
                if (value == "")
                {
                    throw new ArgumentException("");
                }

                messageH = value;
            }
        }
        

        public string SmsNo
        {
            get { return smsNo; }

            set
            {
                if (value == "")
                {
                    throw new ArgumentException("");
                }

                smsNo = value;
                
            }
        }
    }
}
