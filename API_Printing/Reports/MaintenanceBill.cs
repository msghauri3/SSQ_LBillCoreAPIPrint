using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace API_Printing.Reports
{
    public partial class MaintenanceBill : DevExpress.XtraReports.UI.XtraReport
    {
        public MaintenanceBill()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, CancelEventArgs e)
        {

        }
    }
}
