using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaSalesReportLib
{
    class Data
    {
        public Data(string name, double orders, double amount, string averageSalesRepresentation)
        {
            this.Name = name;
            this.Quantity = 0;
            this.Orders = orders;
            this.Amount = amount;
            this.Percent = 0;
            this.Average = 0;
            this.AverageSalesRepresentation = averageSalesRepresentation;
        }

        public Data(string name, double quantity, double amount, double percent)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Orders = 0;
            this.Amount = amount;
            this.Percent = percent;
            this.Average = 0;
            this.AverageSalesRepresentation = "";
        }

        public Data(string name, double quantity, double amount, double percent, double average)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Orders = 0;
            this.Amount = amount;
            this.Percent = percent;
            this.Average = average;
            this.AverageSalesRepresentation = "";
        }

        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Orders { get; set; }
        public double Amount { get; set; }
        public double Percent { get; set; }
        public double Average { get; set; }
        public string AverageSalesRepresentation { get; set; }
    }

    public class ReportData
    {
        #region Method
        public ReportData(string data)
        {
            this.Data = data;
        }
        public ReportData(string data, string data1)
        {
            this.Data = data;
            this.Data1 = data1;
        }
        public ReportData(string data, string data1, string data2)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
        }
        public ReportData(string data, string data1, string data2, string data3)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data6 = data7;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25, string data26)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
            this.Data26 = data26;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25, string data26, string data27)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
            this.Data26 = data26;
            this.Data27 = data27;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25, string data26, string data27, string data28)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
            this.Data26 = data26;
            this.Data27 = data27;
            this.Data28 = data28;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25, string data26, string data27, string data28, string data29)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
            this.Data26 = data26;
            this.Data27 = data27;
            this.Data28 = data28;
            this.Data29 = data29;
        }
        public ReportData(string data, string data1, string data2, string data3, string data4, string data5, string data6, string data7, string data8, string data9, string data10, string data11, string data12, string data13, string data14, string data15, string data16, string data17, string data18, string data19, string data20, string data21, string data22, string data23, string data24, string data25, string data26, string data27, string data28, string data29, string data30)
        {
            this.Data = data;
            this.Data1 = data1;
            this.Data2 = data2;
            this.Data3 = data3;
            this.Data4 = data4;
            this.Data5 = data5;
            this.Data6 = data6;
            this.Data7 = data7;
            this.Data8 = data8;
            this.Data9 = data9;
            this.Data10 = data10;
            this.Data11 = data11;
            this.Data12 = data12;
            this.Data13 = data13;
            this.Data14 = data14;
            this.Data15 = data15;
            this.Data16 = data16;
            this.Data17 = data17;
            this.Data18 = data18;
            this.Data19 = data19;
            this.Data20 = data20;
            this.Data21 = data21;
            this.Data22 = data22;
            this.Data23 = data23;
            this.Data24 = data24;
            this.Data25 = data25;
            this.Data26 = data26;
            this.Data27 = data27;
            this.Data28 = data28;
            this.Data29 = data29;
            this.Data30 = data30;
        }
        #endregion

        #region Property
        public string Data { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }
        public string Data7 { get; set; }
        public string Data8 { get; set; }
        public string Data9 { get; set; }
        public string Data10 { get; set; }
        public string Data11 { get; set; }
        public string Data12 { get; set; }
        public string Data13 { get; set; }
        public string Data14 { get; set; }
        public string Data15 { get; set; }
        public string Data16 { get; set; }
        public string Data17 { get; set; }
        public string Data18 { get; set; }
        public string Data19 { get; set; }
        public string Data20 { get; set; }
        public string Data21 { get; set; }
        public string Data22 { get; set; }
        public string Data23 { get; set; }
        public string Data24 { get; set; }
        public string Data25 { get; set; }
        public string Data26 { get; set; }
        public string Data27 { get; set; }
        public string Data28 { get; set; }
        public string Data29 { get; set; }
        public string Data30 { get; set; }
        #endregion Property

        public string DataIndex(int Index)
        {
            switch (Index)
            {
                case 0:
                    return Data;
                case 1:
                    return Data1;
                case 2:
                    return Data2;
                case 3:
                    return Data3;
                case 4:
                    return Data4;
                case 5:
                    return Data5;
                case 6:
                    return Data6;
                case 7:
                    return Data7;
                case 8:
                    return Data8;
                case 9:
                    return Data9;
                case 10:
                    return Data10;
                case 11:
                    return Data11;
                case 12:
                    return Data12;
                case 13:
                    return Data13;
                case 14:
                    return Data14;
                case 15:
                    return Data15;
                case 16:
                    return Data16;
                case 17:
                    return Data17;
                case 18:
                    return Data18;
                case 19:
                    return Data19;
                case 20:
                    return Data20;
                case 21:
                    return Data21;
                case 22:
                    return Data22;
                case 23:
                    return Data23;
                case 24:
                    return Data24;
                case 25:
                    return Data25;
                case 26:
                    return Data26;
                case 27:
                    return Data27;
                case 28:
                    return Data28;
                case 29:
                    return Data29;
                case 30:
                    return Data30;
            }
            return "";
        }

        public void InserByIndex(int Index, string Value, bool Additional = false)
        {
            switch (Index)
            {
                case 0:
                    Data = (Additional ? (Helpers.NullToFlt(Data) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 1:
                    Data1 = (Additional ? (Helpers.NullToFlt(Data1) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 2:
                    Data2 = (Additional ? (Helpers.NullToFlt(Data2) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 3:
                    Data3 = (Additional ? (Helpers.NullToFlt(Data3) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 4:
                    Data4 = (Additional ? (Helpers.NullToFlt(Data4) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 5:
                    Data5 = (Additional ? (Helpers.NullToFlt(Data5) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 6:
                    Data6 = (Additional ? (Helpers.NullToFlt(Data6) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 7:
                    Data7 = (Additional ? (Helpers.NullToFlt(Data7) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 8:
                    Data8 = (Additional ? (Helpers.NullToFlt(Data8) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 9:
                    Data9 = (Additional ? (Helpers.NullToFlt(Data9) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 10:
                    Data10 = (Additional ? (Helpers.NullToFlt(Data10) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 11:
                    Data11 = (Additional ? (Helpers.NullToFlt(Data11) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 12:
                    Data12 = (Additional ? (Helpers.NullToFlt(Data12) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 13:
                    Data13 = (Additional ? (Helpers.NullToFlt(Data13) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 14:
                    Data14 = (Additional ? (Helpers.NullToFlt(Data14) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 15:
                    Data15 = (Additional ? (Helpers.NullToFlt(Data15) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 16:
                    Data16 = (Additional ? (Helpers.NullToFlt(Data16) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 17:
                    Data17 = (Additional ? (Helpers.NullToFlt(Data17) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 18:
                    Data18 = (Additional ? (Helpers.NullToFlt(Data18) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 19:
                    Data19 = (Additional ? (Helpers.NullToFlt(Data19) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 20:
                    Data20 = (Additional ? (Helpers.NullToFlt(Data20) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 21:
                    Data21 = (Additional ? (Helpers.NullToFlt(Data21) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 22:
                    Data22 = (Additional ? (Helpers.NullToFlt(Data22) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 23:
                    Data23 = (Additional ? (Helpers.NullToFlt(Data23) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 24:
                    Data24 = (Additional ? (Helpers.NullToFlt(Data24) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 25:
                    Data25 = (Additional ? (Helpers.NullToFlt(Data25) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 26:
                    Data26 = (Additional ? (Helpers.NullToFlt(Data26) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 27:
                    Data27 = (Additional ? (Helpers.NullToFlt(Data27) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 28:
                    Data28 = (Additional ? (Helpers.NullToFlt(Data28) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 29:
                    Data29 = (Additional ? (Helpers.NullToFlt(Data29) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
                case 30:
                    Data30 = (Additional ? (Helpers.NullToFlt(Data30) + Helpers.NullToFlt(Value)).ToString() : Value);
                    break;
            }
        }

        public List<ReportData> SubData = new List<ReportData>();
    }

    public class ColumnHeader
    {
        public string Text { get; set; }
        public List<string> SubColumns = new List<string>();

        public ColumnHeader(string text)
        {
            Text = text;
        }
    }

    public struct StoreInfo
    {
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string StoreNumber { get; set; }
    }

}
