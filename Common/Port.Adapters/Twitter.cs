using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaNet.Common.Port.Adapters
{
    public class Twitter
    {
        TwitterContext twitterCtx;

        private void autorizar() {
            if (twitterCtx != null)
                return;

           /* var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "NIa7b3cyQa4rZ4hqlC4oAQ",
                    ConsumerSecret = "1cOTHymyK2itKzvNJbSHqVU0BcgDGhXgGiZ5iKxQE"
                    //TwitterAccessToken = "2164976383-XQwGVDO7TO3a8gqxuWcMrTaDo3ST44zj84ewlnt",
                    //TwitterAccessTokenSecret = "HG15JneGyvH6jIW9UdsytxtfvwttpjSwUewWJg4RLpiCc"
                }
            };*/

            var auth = GetCredentials();
            
            auth.AuthorizeAsync();

            twitterCtx = new TwitterContext(auth);
        }

       private static SingleUserAuthorizer GetCredentials()
       {
           return new SingleUserAuthorizer
           {
               CredentialStore = new SingleUserInMemoryCredentialStore
               {
                   /*ConsumerKey = "NIa7b3cyQa4rZ4hqlC4oAQ",
                   ConsumerSecret = "1cOTHymyK2itKzvNJbSHqVU0BcgDGhXgGiZ5iKxQE",
                   AccessToken = "2164976383-XQwGVDO7TO3a8gqxuWcMrTaDo3ST44zj84ewlnt",
                   AccessTokenSecret = "HG15JneGyvH6jIW9UdsytxtfvwttpjSwUewWJg4RLpiCc"*/
                   ConsumerKey = "I54IEaRGsjYcgu3IPY8m7Q64A",
                   ConsumerSecret = "fryYPiGfYs6fvDIAMTTlBEnYOG4sOBJaSpNxVvzca2ROG9WSUc",
                   AccessToken = "2515503114-T6JOBKCRB9oCAq2sQQsjGBYAr4AqDkrVd2igRU8",
                   AccessTokenSecret = "kISqE2ODCIMiSq4c4u8jfpdsjqu9YhH0ngIsTp40Jjt0c"
               }
           };
        }

        public void postar(string msg)
        {
             autorizar();
             twitterCtx.TweetAsync(DateTime.Now + " " + msg);
            //twitterCtx.UpdateStatus(DateTime.Now + " " + msg);
        }

    }
}
