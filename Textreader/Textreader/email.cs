using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textreader
{
    class email
    {
        private string emailHeader;
        private string emailText;
        private string emailAddress;
        private string emailBody;


        public string EmailHeader
        {
            get { return emailHeader; }
            set
            {
                emailHeader = value;
            }
        }

        public string EmailText
        {
            get { return emailText; }
            set
            {
                emailText = value;
            }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                emailAddress = value;
            }
        }

        public string EmailBody
        {
            get { return emailBody; }
            set
            {
                emailBody = value;
            }
        }
    }
}
