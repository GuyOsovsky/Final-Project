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
using System.Web;

namespace PoliceVolnteerBL
{
    public class MediaBL
    {
        public string FileName { get; set; }
        public int ActivityCode { get; set; }
        public int FileType { get; set; }

        //buid media object and save it in database
        public MediaBL(string fileName, int activityCode, int fileType)
        {
            //fullname (filename + .formatname)
            this.FileName = fileName;

            this.ActivityCode = activityCode;

            this.FileType = fileType;

            //add to database
            MediaDAL.AddMedia(fileName, activityCode, fileType);
        }

        //return filetype from types table
        static public int CheckFormatValidation(string fileFullName)
        {
            //get format name (.txt,.mp3 ...)
            string fileFormat = Path.GetExtension(fileFullName);

            //get types table
            DataTable typesTable = FileTypesDAL.GetTable().Tables[0];

            //check if file format is fits to one of the formats in database, and return type  code
            foreach (DataRow dataRow in typesTable.Rows)
            {
                if (dataRow["TypeName"].ToString() == fileFormat)
                    return int.Parse(dataRow["TypeCode"].ToString());
            }
            //if not exists in database return -1
            return -1;
        }

        //create valid file types limit string for uploaded files 
        static public string LimitString()
        {
            string result = " | ";
            ////get types table
            DataTable typesTable = FileTypesDAL.GetTable().Tables[0];
            foreach (DataRow dataRow in typesTable.Rows)
            {
                //add existing types to string from database
                result += dataRow["TypeName"].ToString() + " | ";
            }
            return result;
        }

        /// <summary>
        /// build object from the database
        /// </summary>
        /// <param name="FileName"></param>
        public MediaBL(string FileName)
        {
            this.FileName = FileName;
            //get row of dataset by filename
            DataSet mediaDataSet = MediaDAL.GetTable(new FieldValue<MediaField>(MediaField.FileName, FileName, Table.Media, FieldType.String, OperatorType.Equals));
            this.ActivityCode = (int)mediaDataSet.Tables[0].Rows[0]["ActivityCode"];
            this.FileType = (int)mediaDataSet.Tables[0].Rows[0]["FileType"];
        }

        /// <summary>
        /// delete file from server and from database by activity code and file name, and in some cases delete directory/folder too
        /// </summary>
        public static bool DeleteFile(int activityCode, string fileName)
        {
            //get directory name
            string targetPath = NewActivityDir(activityCode);
            //try to delete file and in some cases directory/folder too
            try
            {
                //if sum of files in new directory equals zero - delete this directory/folder
                if (Directory.GetFiles(targetPath).Length == 0)
                {
                    //delete directory/folder
                    Directory.Delete(targetPath);
                    return false;
                }
                //get path for the file that need to be deleted
                string deletePath = Path.Combine(targetPath, fileName);
                //if exists
                if (File.Exists(deletePath))
                {
                    //delete file from server
                    File.Delete(deletePath);

                    //delete file from database
                    MediaDAL.DelMedia(fileName);
                    
                    //if after we deleted the file and now the folder is empty, delete folder too
                    if (Directory.GetFiles(targetPath).Length == 0)
                    {
                        //delete directory/folder
                        Directory.Delete(targetPath);
                    }
                    return true;
                }
                return false;
            }
            catch(Exception e)
            {
                //throw error to upper layer
                throw e;
            }
        }

        /// <summary>
        /// create new folder by activitycode if not exist, and return folder path
        /// </summary>
        public static string NewActivityDir(int activityCode)
        {
            //get acticity object by activity code
            ActivityBL activity = new ActivityBL(activityCode);
            
            //create folder name by activityname and activitycode
            string folderName = activity.ActivityName + " " + activity.ActivityCode;

            //get main folder for server files
            string targetPath = HttpContext.Current.Server.MapPath("~/Files/");

            //combine folder name and main folder path
            string pathString = System.IO.Path.Combine(targetPath, folderName);

            //try to create the new directory/folder and return the new path
            try
            {
                System.IO.Directory.CreateDirectory(pathString);
                return pathString;
            }
            catch(Exception e)
            {
                //throw error to upper layer
                throw e;
            }
        }
    }
}
