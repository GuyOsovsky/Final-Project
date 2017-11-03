using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;

namespace PoliceVolnteerBL
{
    public class MediaBL
    {
        public string FileName { get; set; }
        public int ActivityCode { get; set; }
        public string FilePath { get; set; }
        public int FileType { get; set; }

        public MediaBL(string FileName, int ActivityCode, string FilePath, int FileType)
        {
            this.FileName = FileName;
            this.ActivityCode = ActivityCode;
            this.FilePath = FilePath;
            this.FileType = FileType;
            MediaDAL.AddMedia(FileName, ActivityCode, FilePath, FileType);
        }

        public MediaBL(string FileName)
        {
            this.FileName = FileName;
            DataSet ds = MediaDAL.GetTable(new FieldValue<MediaField>(MediaField.FileName, FileName, FieldType.String, OperatorType.Equals));
            //this.ActivityName = (string)ds.Tables[0].Rows[0]["ActivityName"];
            this.ActivityCode = (int)ds.Tables[0].Rows[0]["ActivityCode"];
            this.FilePath = (string)ds.Tables[0].Rows[0]["FilePath"];
            this.FileType = (int)ds.Tables[0].Rows[0]["FileType"];
        }
    }
}
