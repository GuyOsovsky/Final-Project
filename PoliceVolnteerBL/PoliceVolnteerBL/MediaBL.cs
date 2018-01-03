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

        //build and adding to database, create new folder if necessary, create file from FileBytes in the new folder that created before, in "files" folder.
        public MediaBL(int ActivityCode, string fileName, byte[] FileBytes)
        {
            //get new directory path
            string newTargetPath = GetNewActivityDir(ActivityCode);

            //get format name (.txt,.mp3 ...)
            string fileFormat = Path.GetExtension(fileName);

            this.FileName = fileName;

            //is file format Valid
            bool isValid = false;
            
            DataTable typesTable = FileTypesDAL.GetTable().Tables[0];
            
            //check if file format is fits to one of the formats in database 
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
                //Create new directory
                System.IO.Directory.CreateDirectory(newTargetPath);
                //create new file from byte array in the new directory
                using (var fileStream = new FileStream(Path.Combine(newTargetPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(FileBytes, 0, FileBytes.Length);
                }
            }
            catch (Exception e)
            {
                //if sum of files in new directory equals zero delete this new directory(folder)
                if (Directory.GetFiles(newTargetPath).Length == 0)
                {
                    Directory.Delete(newTargetPath);
                }
                throw e;
            }

            MediaDAL.AddMedia(FileName, ActivityCode, FileType);
        }

        //build from the database
        public MediaBL(string FileName)
        {
            this.FileName = FileName;
            DataSet ds = MediaDAL.GetTable(new FieldValue<MediaField>(MediaField.FileName, FileName, FieldType.String, OperatorType.Equals));
            this.ActivityCode = (int)ds.Tables[0].Rows[0]["ActivityCode"];
            this.FileType = (int)ds.Tables[0].Rows[0]["FileType"];
        }

        //delete file from "files" by activity code and file name
        public static bool DeleteFile(int activityCode, string fileName)
        {
            //get directory name
            string targetPath = GetNewActivityDir(activityCode);
            try
            {
                //if sum of files in new directory equals zero delete this directory(folder)
                if (Directory.GetFiles(targetPath).Length == 0)
                {
                    Directory.Delete(targetPath);
                    return false;
                }
                //get path for the file that need to be deleted
                string deletePath = Path.Combine(targetPath, fileName);
                if (File.Exists(deletePath))
                {
                    File.Delete(deletePath);
                    ActivityDAL.DelActivity(activityCode);
                    //if after we deleted the file, the folder is empty, delete folder
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

        //return new combined path for creating a new folder for new activity
        private static string GetNewActivityDir(int ActivityCode)
        {
            ActivityBL activity = new ActivityBL(ActivityCode);
            //create name
            string activityName = activity.ActivityName;
            string folderName = activityName + " " + activity.ActivityCode;
            //get local path("files" folder path)
            string localPath = System.IO.Directory.GetCurrentDirectory();
            localPath = localPath.Remove(localPath.Length - (5 + 3 + (2 * 1)));
            string targetPath = localPath + @"\Files";
            //combine name and new path
            string pathString = System.IO.Path.Combine(targetPath, folderName);
            return pathString;
        }

    }
}
