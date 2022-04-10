using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Typing
{
    class Configure
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private string configFileName = $"{Path.Combine(Directory.GetCurrentDirectory(), "typing.ini")}";
        private string defaultSectionName = "DJMAX_TYPING";

        public enum ParseDateType
        {
            BOOL,
            INT,
            STRING
        }

        public Configure(string fileName=null, string sectionName=null)
        {
            if (fileName != null) this.configFileName = fileName;
            if (sectionName != null) this.defaultSectionName = sectionName;
        }

        public object Get(string keyName, string defaultValue="", ParseDateType type=ParseDateType.STRING, string sectionName=null)
        {
            /*
             * INI 파일의 키에 대응되는 값을 가져온다
             */

            StringBuilder sb = new StringBuilder(defaultValue);

            // ini 읽기
            GetPrivateProfileString((sectionName == null ? this.defaultSectionName : sectionName), keyName, defaultValue, sb, (sb.MaxCapacity + 1), this.configFileName);

            string tempVal = sb.ToString(); // 임시값

            object resultVal = null; // 최종 반환값

            // 원하는 자료형에 따라 값 변환
            switch (type)
            {
                case ParseDateType.BOOL:
                    resultVal = (bool)(tempVal.Equals("true") || tempVal.Equals("1"));
                    break;
                case ParseDateType.INT:
                    resultVal = (int)int.Parse(tempVal);
                    break;
                case ParseDateType.STRING:
                default:
                    resultVal = (string)tempVal;
                    break;
            }

            return resultVal;
        }

        public void Put(string keyName, string value, string sectionName=null)
        {
            WritePrivateProfileString((sectionName == null ? this.defaultSectionName : sectionName), keyName, value, this.configFileName);
        }
    }
}
