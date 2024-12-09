using System;
using System.IO;

namespace ProjectLottery.V1.Helpers.Utils
{
    public class UtilsCommon
  {
    public static string LoadJson(string file)
    {
      try
      {
        using (StreamReader r = new StreamReader(file))
        {
          string json = r.ReadToEnd();
          return json;
        }
      }
      catch (Exception ex)
      {

        throw new Exception(ex.Message);
      }
     
    }
  }
}
