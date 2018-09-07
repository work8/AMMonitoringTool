using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Aveva.DSME.LicenseManager.LicenseManagerLib;


namespace AMMonitoringTool.Connection
{
    //AM License 목록을 갱신하여 현재 접속자 수에 대한 정보가 있는 class
    public class AMLicenseList
    {

        private static AMLicenseList _instance;

       private List<LicenseServer> LicenseServerList { get; set; }

          //singleton 방식으로 고유의 인스턴스 생성
        private AMLicenseList()
        {
            if (LicenseServerList == null)
            {
                LicenseServerList = new List<LicenseServer>();
            }
            else
            {
                LicenseServerList.Clear();
            }

            update();
        }

        public AMLicenseList getInstance()
        {
            if (_instance == null)
            {
                _instance = new AMLicenseList();
            }

            return _instance;
        }
        

        
        public void update()
        {
            //System.Configuration.dll 참조하여 사용
            string LicenseFilePath = System.Configuration.ConfigurationManager.AppSettings["AMLicensePath"];
            List<string> licenseList = LicenseFilePath.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string serverName in licenseList)
            {
                int port = 4545;

                try
                {

                
                Aveva.DSME.LicenseManager.LicenseManagerLib.AvevaLicenseProvider alp = 
                new Aveva.DSME.LicenseManager.LicenseManagerLib.AvevaLicenseProvider(serverName, port);

                List< Aveva.DSME.LicenseManager.LicenseManagerLib.Models.Feature> features =
                alp.GetFeatures().ToList();

                Aveva.DSME.LicenseManager.LicenseManagerLib.Models.Feature selectFeature = features.Find(X => X.Id.Equals("SPG_AP_MARINE_6"));


                LicenseServerList.Add(new LicenseServer() {
                    Name = serverName,
                    CurrentLicense = selectFeature.UsedLicense,
                    TotalLicense = selectFeature.TotalLicense
                    });
                }
                catch(SentinelRMSCore.RMSException.RMSLicenseCoreException e)
                {
                    //
                }

            }
        }


        public int getTotalLicenseValue()
        {
            int resultValue = 0;
            foreach(LicenseServer server in LicenseServerList)
            {
                resultValue += server.TotalLicense;
            }

            return resultValue;
        }

        public int getCurrenticenseValue()
        {
            int resultValue = 0;
            foreach (LicenseServer server in LicenseServerList)
            {
                resultValue += server.CurrentLicense;
            }

            return resultValue;
        }


        
    }





    class LicenseServer
    {
        public string Name { get; set; }
        public int CurrentLicense { get; set; }
        public int TotalLicense { get; set; }


    }
}
