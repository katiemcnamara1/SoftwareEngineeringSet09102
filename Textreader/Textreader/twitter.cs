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
            set { tweetBody = value; }
        }

        public string TwitterHandle
        {
            get { return twitterHandle; }
            set { twitterHandle = value; }
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

