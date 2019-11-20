using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace EmailAPI.Controllers
{
    public class EmailController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public IHttpActionResult UniqueEmailCount([FromBody]string[] emailList)
        {

            //Return 0 if the array is empty or null
            if (emailList == null || emailList.Count() == 0)
            {
                return Ok(0);
            }
            else if (emailList.Count() == 1) //return 1 if the size of the list is 1
            {
                return Ok(1);
            }
            else // Process the list if the count is greater than 1
            {
                // Adding an element is O(1) 
                HashSet<string> setEmail = new HashSet<string>();


                // Time complexity is O(n) for looping n item in the list
                foreach (string email in emailList)
                {

                    StringBuilder sbEmail = new StringBuilder();

                    //Assuming c is the length of the longest email address Time Complexity is O(c) for this operation
                    string strUser = email.Split('@')[0];

                    //Assuming c is the length of the longest email address Time Complexity is O(c) for this operation
                    string strDomain = email.Split('@')[1];

                    //Assuming c is the length of the longest email address Time Complexity is O(c) for this loop
                    foreach (char c in strUser)
                    {
                        if (c == '.')
                        {
                            continue;
                        }
                        else if (c == '+')
                        {
                            break;
                        }
                        else
                        {
                            sbEmail.Append(c.ToString().ToLower());
                        }
                    }

                    sbEmail.Append(strDomain);

                    setEmail.Add(sbEmail.ToString());
                } //Overall Time complexity is O(n * c) where n is the no of emails in the list and c is the length of the longest email address

                return Ok(setEmail.Count);
            }

        }     
    }
}
