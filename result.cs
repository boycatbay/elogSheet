using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elogsheet
{
    public class result
    {
 
            public String result_name;
            public String resultss;



            public  result[] dynamicArray(result[] strArray, result strData)
            {
                int lenArray = strArray.Length + 1;
                result[] strReturn = new result[lenArray];
                for (int i = 0; i < strArray.Length; i++)
                {
                    strReturn[i] = strArray[i];
                }
                strReturn[lenArray - 1] = strData;
                return strReturn;
            }
        
    }
}