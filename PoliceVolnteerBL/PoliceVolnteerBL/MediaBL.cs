using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceVolnteerDAL;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace PoliceVolnteerBL
{
    public class MediaBL
    {
        public string FileName { get; set; }
        public int ActivityCode { get; set; }
        public int FileType { get; set; }

        public MediaBL(int ActivityCode, string fileName, byte[] FileBytes)
        {
            string newTargetPath = GetNewActivityDir(ActivityCode);

            string fileFormat = Path.GetExtension(fileName);
            this.FileName = fileName;

            bool isValid = false;
            DataTable typesTable = FileTypesDAL.GetTable().Tables[0];
            foreach (DataRow dataRow in typesTable.Rows)
            {
                if (dataRow["TypeName"].ToString() == fileFormat)
                {
                    this.FileType = int.Parse(dataRow["TypeCode"].ToString());
                    isValid = true;
                    break;
                }
            }
            if (!isValid)
            {
                Console.WriteLine("{0} is not a valid format!", fileFormat);
                return;
            }

            try
            {
                System.IO.Directory.CreateDirectory(newTargetPath);
                using (var fileStream = new FileStream(Path.Combine(newTargetPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(FileBytes, 0, FileBytes.Length);
                }
            }
            catch (Exception e)
            {
                if (Directory.GetFiles(newTargetPath).Length == 0)
                {
                    Directory.Delete(newTargetPath);
                }
                throw e;
            }

            MediaDAL.AddMedia(FileName, ActivityCode, FileType);
        }

        public MediaBL(string FileName)
        {
            this.FileName = FileName;
            DataSet ds = MediaDAL.GetTable(new FieldValue<MediaField>(MediaField.FileName, FileName, FieldType.String, OperatorType.Equals));
            this.ActivityCode = (int)ds.Tables[0].Rows[0]["ActivityCode"];
            this.FileType = (int)ds.Tables[0].Rows[0]["FileType"];
        }

        
        public static bool DeleteFile(int activityCode, string fileName)
        {
            string targetPath = GetNewActivityDir(activityCode);
            try
            {
                if (Directory.GetFiles(targetPath).Length == 0)
                {
                    Directory.Delete(targetPath);
                    return false;
                }
                string deletePath = Path.Combine(targetPath, fileName);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                    ActivityDAL.DelActivity(activityCode);
                    if (Directory.GetFiles(targetPath).Length == 0)
                    {
                        Directory.Delete(targetPath);
                    }
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static string GetNewActivityDir(int ActivityCode)
        {
            ActivityBL activity = new ActivityBL(ActivityCode);
            string activityName = activity.ActivityName;
            string folderName = activityName + " " + activity.ActivityCode;
            string localPath = System.IO.Directory.GetCurrentDirectory();
            localPath = localPath.Remove(localPath.Length - (5 + 3 + (2 * 1)));
            string targetPath = localPath + @"\Files";
            string pathString = System.IO.Path.Combine(targetPath, folderName);
            return pathString;
        }

    }
}
