namespace SM
{
    /// <summary>
    /// Remove exe after set time from fearst start
    /// </summary>
    public class SaftyModuleForFreelance
    {
        public long DemoTime { get; set; } = 30 * 60;
        string a = System.AppDomain.CurrentDomain.FriendlyName + " " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        DriveInfo[] drives = DriveInfo.GetDrives();
        public bool isPass=true;
        List<List<string>> readFromDisksData = new List<List<string>>();
        public SaftyModuleForFreelance()
        {
            drives = drives.Where(C => C.DriveType.ToString() == "Fixed").ToArray();// оставляем только жесткие диски
            int errorcount = 0; // количество дисков на которых отсутствует нужный нам файл
            for(int i =0; i < drives.Length; i++)
            {
                try
                {
                    List<string> strs = new List<string>();
                    StreamReader sr = new StreamReader(drives[i].Name + "\\MetaNixData.dat");
                    while (!sr.EndOfStream)
                    {
                        strs.Add(encode(sr.ReadLine()));
                    }
                    readFromDisksData.Add(strs);
                    sr.Close();
                }
                catch (Exception)
                {
                    errorcount++;
                }
                
            }
            if (errorcount == drives.Length)
                WriteStartData();
            else if (readFromDisksData.Count != drives.Length)
                isPass = false;
            else
            {
                readFromDisksData = readFromDisksData.Where(c => c.Contains(a)).ToList();
                if(readFromDisksData.Count != drives.Length && readFromDisksData.Count!=0)
                    isPass = false;
                else
                {
                    foreach(var dis in readFromDisksData)
                    {
                        var sttime = dis.IndexOf(a);
                        var time = DateTime.Now;
                        var stt = new DateTime();
                        try
                        {
                             stt = DateTime.Parse(dis[sttime+1]);

                        }
                        catch
                        {
                            isPass = false;
                            break;
                        }
                        TimeSpan def = time.Subtract(stt);
                        var res = TimeWork.getTimeInSecods(def);
                        if (res >= DemoTime)
                            isPass = false;
                    }
                }
            }
        }

        private void WriteStartData()
        {
            foreach (var dr in drives)
            {
                StreamWriter sw = new StreamWriter(dr.Name + "\\MetaNixData.dat");
                string str = a;
                sw.WriteLine(encode(a));
                sw.WriteLine(encode(DateTime.UtcNow.ToString()));
                sw.Close();
            }
        } 
        private string encode(string inp)
        {
            ushort secretKey = 0x0088; // Секретный ключ (длина - 16 bit).


            //это строка которую мы зашифруем

            string str = EncodeDecrypt(inp, secretKey); //производим шифрование
             

            //str = EncodeDecrypt(str, secretKey); //производим рассшифровку 
            return str;

        }

        private string EncodeDecrypt(string str, ushort secretKey)
        {
            var ch = str.ToArray(); //преобразуем строку в символы
            string newStr = "";      //переменная которая будет содержать зашифрованную строку
            foreach (var c in ch)  //выбираем каждый элемент из массива символов нашей строки
                newStr += TopSecret(c, secretKey);  //производим шифрование каждого отдельного элемента и сохраняем его в строку
            return newStr;
        }

        private char TopSecret(char character, ushort secretKey)
        {
            character = (char)(character ^ secretKey); //Производим XOR операцию
            return character;
        }
    }
}