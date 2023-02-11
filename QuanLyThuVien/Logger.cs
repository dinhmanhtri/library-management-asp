using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace QuanLyThuVien
{
    public static class Logger
    {
        public static void LogException(Exception ex)
        {
            string filePath = @"D:\ExceptionLog.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}