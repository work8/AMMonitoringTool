using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AMMonitoringTool.ClassList
{
    
        public class Ship
        { 
            OutputConnectUser.User[] userList;

            public string name;
            string path;
            public int counter;
            string shipCODE;
            string type;
            string nonDABACON;
            string DABACON;
            string Driver;
            public long TotalSize;

            public string getShipCODE()
            {
                return shipCODE;
            }

            public string getName()
            {
                return name;

            }


            public string getDriver()
            {
                return Driver;

            }


            public OutputConnectUser.User[] getUserList()
            {
                return userList;
            }


            public int getCounter()
            {
                return counter;

            }

            public string getShipType()
            {
                return type;
            }

            public string getNonDABACONPath()
            {
                return nonDABACON;

            }

            public string getDABACONPath()
            {
                return DABACON;

            }



            public Ship(string name, string path)
            {
                this.name = name;
                this.path = path;
            }

            public Ship(string name, Decimal counter)
            {
                this.name = name;
                this.counter = Convert.ToInt32(counter);
            }

            public Ship(string name, string path, string type) : this(name, path)
            {

                this.type = type;

            }

            public Ship(string name, string path, string type, string nonDABACON) : this(name, path, type)
            {
                this.nonDABACON = @path.Substring(0, 2) + nonDABACON.Substring(0, nonDABACON.Length - 3);
                this.shipCODE = nonDABACON.Substring(nonDABACON.Length - 3, 3);
                this.DABACON = path.Substring(0, path.Length - 13);

                this.Driver = path.Substring(0, 1);
            }

            //






            public void Calculator()
            {
                userList = OutputConnectUser.DsmeConnectUser.Output(path);
                try
                {
                    counter = userList.Length;
                }
                catch (NullReferenceException)
                {
                    return;
                }
            }



            public long getTotalSize()
            {
                long size = 0;

                //Scripting.FileSystemObject fso = new Scripting.FileSystemObject();
                //Scripting.Folder Dabacon = fso.GetFolder(@DABACON);
                try
                {
                    
                //Interop.Scriting dll 파일 필요
                    var SizeDabacon = new Scripting.FileSystemObject().GetFolder(@DABACON).Size;
                    var SizeNonDabacon = new Scripting.FileSystemObject().GetFolder(@nonDABACON).Size;

                    //Scripting.Folder nonDabacon = fso.GetFolder(@nonDABACON);
                    //var SizeNonDabacon = nonDabacon.Size ;

                    //String temDA = SizeDabacon.GetType().ToString();
                    // string temNonDA= SizeNonDabacon.GetType().ToString();

                    long realDabacon = 0;
                    long realNonDabacon = 0;


                    if (SizeDabacon.GetType().ToString().Equals("System.Double"))
                    {
                        //     Console.WriteLine("1");
                        long temp = (long)((System.Double)SizeDabacon);
                        realDabacon = temp / 1024 / 1024;
                    }
                    else
                    {
                        //    Console.WriteLine("2");
                        realDabacon = ((System.Int32)SizeDabacon) / 1024 / 1024;

                    }


                    if (SizeNonDabacon.GetType().ToString().Equals("System.Double"))
                    {
                        //    Console.WriteLine("3");
                        long temp = (long)((System.Double)SizeNonDabacon);
                        realNonDabacon = temp / 1024 / 1024;
                    }
                    else
                    {
                        //   Console.WriteLine("4");
                        realNonDabacon = ((System.Int32)SizeNonDabacon) / 1024 / 1024;
                    }



                    TotalSize = realDabacon + realNonDabacon;

                    // Console.WriteLine(TotalSize);

                    //System.Int64 totalSize = (System.Int64)(Math.Round((System.Double)SizeDabacon) +  Math.Round((System.Double)SizeNonDabacon));
                    // TotalSize = TotalSize / 1024 / 1024;
                    return TotalSize;

                    //catch (System.InvalidCastException e)
                    //{
                    //    Console.WriteLine("sizeDabacon :" + SizeDabacon);
                    //    Console.WriteLine(SizeDabacon.GetType());
                    //    Console.WriteLine("size NonDabacon :" + SizeNonDabacon);
                    //    Console.WriteLine(SizeNonDabacon.GetType());
                    //    //Console.WriteLine("-----------------------------------");


                    //    //if((SizeDabacon.GetType().ToString().Equals("System.Int32")) && (SizeNonDabacon.GetType().ToString().Equals("System.Int32"))){

                    //    //        long temp = (System.Int32)SizeDabacon + (System.Int32)SizeNonDabacon;

                    //    //        Console.WriteLine("result :" + temp);

                    //    //}

                    //    //f(SizeDabacon.GetType().Equals("System.Int32") || si)
                    //    //Console.WriteLine("total "  + ((System.Double)SizeDabacon + (System.Double)SizeNonDabacon));

                    //   // Console.WriteLine(e.Data.ToString());
                    //    return 0;
                    //}
                    /*
                    System.IO.DirectoryInfo mDABACONInfo = new System.IO.DirectoryInfo(@DABACON);
                    foreach (System.IO.FileInfo fileInfo in mDABACONInfo.GetFiles("*",System.IO.SearchOption.AllDirectories))
                    {
                        size += (long)fileInfo.Length;
                    }

                
                    System.IO.DirectoryInfo mNONDABACONInfo = new System.IO.DirectoryInfo(@nonDABACON);
                    foreach (System.IO.FileInfo fileInfo in mNONDABACONInfo.GetFiles("*",System.IO.SearchOption.AllDirectories))
                    {
                        size += (long)fileInfo.Length;
                    }

                    */
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    return 0;
                }
















            }
        }
    
}
