using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMMonitoringTool.ClassList;

namespace AMMonitoringTool.Connection
{
    public class AmShipList
    {
        private static AmShipList _instance;
        private static List<ClassList.Ship> shipList;

        private AmShipList()
        {
            update();   
        }

        public AmShipList getInstance()
        {
            if (_instance == null)
            {
                _instance = new AmShipList();
            }

            return _instance;
        }

        #region am 호선 리스트 갱신 메서드
        public void update()
        {
            shipList = new List<ClassList.Ship>();
            
            DSME_AM_ProjectListDataSet ds = new DSME_AM_ProjectListDataSet();
            DSME_AM_ProjectListDataSetTableAdapters.DSMEShipListTableAdapter da = new DSME_AM_ProjectListDataSetTableAdapters.DSMEShipListTableAdapter();

            da.Fill(ds.DSMEShipList);
            
            foreach (DSME_AM_ProjectListDataSet.DSMEShipListRow row in ds.DSMEShipList)
            {
                //project의 CODE를 추출
                string projCode = row.FullPath.Trim();
                projCode = projCode.Substring(projCode.Length - 3, 3);

                shipList.Add(new ClassList.Ship(row.ShipName, row.FullPath.Trim() + @"000\" + projCode + "com", row.ShipType, row.NONDABACON_Path));

            }
            da.Dispose();


        }


        #endregion

    }


}

