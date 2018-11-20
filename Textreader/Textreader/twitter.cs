using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textreader
{
    class twitter
    {
        private string tweetHeading;
        private string tweetBody;
        private string twitterHandle;

        public string TweetHeading
        {
            get { return tweetHeading; }
            set { tweetHeading = value; }
        }

        public string TweetBody
        {
            get { return tweetBody; }
            set
            {   if (value.Length < 141)
                    tweetBody = value;
                else
                    throw new ArgumentException("Tweet body too long.");
            }
        }

        public string TwitterHandle
        {
            get { return twitterHandle; }
            set
            {
                if (value.Length < 16)
                    twitterHandle = value;
                else
                    throw new ArgumentException("Twitter handle too long.");
            }
        }

    }


    public class hashtag
    {
        private string hash;
        private string tagCounter;

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        public string TagCounter
        {
            get { return tagCounter; }
            set { tagCounter = value; }
        }
    }
}

