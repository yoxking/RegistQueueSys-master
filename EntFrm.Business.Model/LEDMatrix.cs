using System;

namespace EntFrm.Business.Model
{
  public class LEDMatrix
  {
     public LEDMatrix(){ }

       private int ID;
       public int iID
       {
           set { this.ID =value;}
           get { return this.ID;}
        }

       private string MatrixNo;
       public string sMatrixNo
       {
           set { this.MatrixNo =value;}
           get { return this.MatrixNo;}
        }

       private string MatrixName;
       public string sMatrixName
       {
           set { this.MatrixName =value;}
           get { return this.MatrixName;}
        }

       private string MatrixModel;
       public string sMatrixModel
       {
           set { this.MatrixModel =value;}
           get { return this.MatrixModel;}
        }

       private string ServiceNos;
       public string sServiceNos
       {
           set { this.ServiceNos =value;}
           get { return this.ServiceNos;}
        }

       private int PhyAddr;
       public int iPhyAddr
       {
           set { this.PhyAddr =value;}
           get { return this.PhyAddr;}
        }

       private string Protocol;
       public string sProtocol
       {
           set { this.Protocol =value;}
           get { return this.Protocol;}
        }

       private string SerialPort;
       public string sSerialPort
       {
           set { this.SerialPort =value;}
           get { return this.SerialPort;}
        }

       private int Baudrate;
       public int iBaudrate
       {
           set { this.Baudrate =value;}
           get { return this.Baudrate;}
        }

       private string IpAddress;
       public string sIpAddress
       {
           set { this.IpAddress =value;}
           get { return this.IpAddress;}
        }

       private string LocalPort;
       public string sLocalPort
       {
           set { this.LocalPort =value;}
           get { return this.LocalPort;}
        }

       private string ParamFormat;
       public string sParamFormat
       {
           set { this.ParamFormat =value;}
           get { return this.ParamFormat;}
        }

       private int TimeoutSec;
       public int iTimeoutSec
       {
           set { this.TimeoutSec =value;}
           get { return this.TimeoutSec;}
        }

       private int DisplayRows;
       public int iDisplayRows
       {
           set { this.DisplayRows =value;}
           get { return this.DisplayRows;}
        }

       private string DisplayFormat;
       public string sDisplayFormat
       {
           set { this.DisplayFormat =value;}
           get { return this.DisplayFormat;}
        }

       private string BranchNo;
       public string sBranchNo
       {
           set { this.BranchNo =value;}
           get { return this.BranchNo;}
        }

       private string AddOptor;
       public string sAddOptor
       {
           set { this.AddOptor =value;}
           get { return this.AddOptor;}
        }

       private DateTime AddDate;
       public DateTime dAddDate
       {
           set { this.AddDate =value;}
           get { return this.AddDate;}
        }

       private string ModOptor;
       public string sModOptor
       {
           set { this.ModOptor =value;}
           get { return this.ModOptor;}
        }

       private DateTime ModDate;
       public DateTime dModDate
       {
           set { this.ModDate =value;}
           get { return this.ModDate;}
        }

       private int ValidityState;
       public int iValidityState
       {
           set { this.ValidityState =value;}
           get { return this.ValidityState;}
        }

       private string Comments;
       public string sComments
       {
           set { this.Comments =value;}
           get { return this.Comments;}
        }

       private string AppCode;
       public string sAppCode
       {
           set { this.AppCode =value;}
           get { return this.AppCode;}
        }

       private string Version;
       public string sVersion
       {
           set { this.Version =value;}
           get { return this.Version;}
        }

    }
  }

