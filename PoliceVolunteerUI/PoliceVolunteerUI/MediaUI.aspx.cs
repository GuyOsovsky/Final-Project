using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PoliceVolnteerBL;
using PoliceVolnteerDAL;

namespace PoliceVolunteerUI
{
    public partial class MediaUI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() == "")
            {
                Response.Redirect("HomePageUI.aspx");
            }
            
            //show media - gridviews and labels
            ShowMedia(hey);

            if (!IsPostBack)
            {
                //fill activity DropDownList
                FillActivityList();
                //post limitation string
                limitationLabel.Text = CreateLimitationString();
            }
        }

        //create gridview for a files in a specific folder
        protected void CreateGridView(Control container, string folderName)
        {
            //create gridview object
            GridView objGV = new GridView();

            //set gridview properties
            objGV.AutoGenerateColumns = false;
            objGV.Style["z-index"] = "101";
            objGV.Style["position"] = "relative";
            objGV.Style["top"] = "9px";
            objGV.BorderColor = System.Drawing.ColorTranslator.FromHtml("#000099");
            objGV.CellPadding = 4;
            objGV.GridLines = new GridLines();
            objGV.Width = new Unit("100%");
            objGV.ForeColor = System.Drawing.Color.FromName("Black");
            objGV.BackColor = System.Drawing.Color.FromName("LightBlue");
            objGV.Font.Bold = true;

            objGV.AlternatingRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#dbffe5");

            objGV.RowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f4fbff");
            objGV.RowStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");

            objGV.SelectedRowStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
            objGV.SelectedRowStyle.Font.Bold = true;
            objGV.SelectedRowStyle.ForeColor = System.Drawing.Color.FromName("Navy");

            objGV.PagerStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFCC66");
            objGV.PagerStyle.ForeColor = System.Drawing.ColorTranslator.FromHtml("#333333");
            objGV.PagerStyle.HorizontalAlign = HorizontalAlign.Center;


            //create BoundField for a new column with name of file
            BoundField nameField = new BoundField();
            nameField.DataField = "Text";
            nameField.HeaderText = "שם הקובץ";
            //add field to gridview
            objGV.Columns.Add(nameField);

            //create BoundField for a new column with format of file
            BoundField formatField = new BoundField();
            formatField.DataField = "Value";
            formatField.HeaderText = "סוג קובץ(פרומט)";
            //add field to gridview
            objGV.Columns.Add(formatField);

            //make gridview run at server
            objGV.Attributes.Add("runat", "server");
            //add gridview to container Control
            container.Controls.Add(objGV);
            
            //get all files from a folder
            string[] filePaths = System.IO.Directory.GetFiles(Server.MapPath("~/Files/" + folderName));

            //create list of listitems for gridview
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filePaths)
            {
                //get filename
                string fileName = System.IO.Path.GetFileName(filePath);
                //get formatname
                string fileFormat = System.IO.Path.GetExtension(fileName);
                //edit filename ande foramtname
                fileName = fileName.Substring(0, fileName.Length - fileFormat.Length);
                fileFormat = fileFormat.Substring(1);
                //add item to list
                files.Add(new ListItem(fileName, fileFormat));
            }
            //add list to datasource and bind it
            objGV.DataSource = files;
            objGV.DataBind();

            //for each row add - delete file and show file
            foreach (GridViewRow row in objGV.Rows)
            {
                //add show cell
                TableCell showCell = new TableCell();
                //add show link
                HyperLink showLink = new HyperLink();
                showLink.Text = "הצג";

                //create legal url
                folderName.Replace(" ", "20%");
                showLink.NavigateUrl = "/Files/" + folderName + "/" + row.Cells[0].Text.Replace("amp;", "") + "." + row.Cells[1].Text;
                showLink.Target = "_blank";
                
                //add link to cell controls
                showCell.Controls.Add(showLink);
                //add cell to row cells
                row.Cells.Add(showCell);

                if (IsAbleVolunteer())
                {
                    //add delete cell
                    TableCell deleteCell = new TableCell();
                    //add delete button
                    Button deleteBtn = new Button();
                    deleteBtn.Text = "מחק";

                    //add click event for deleting file
                    deleteBtn.Click += deleteBtn_Click;

                    //add button to cell controls
                    deleteCell.Controls.Add(deleteBtn);
                    //add cell to row cells
                    row.Cells.Add(deleteCell);
                }

            }
        }

        //is admin and is able to manage media
        protected bool IsAbleVolunteer()
        {
            if (Session["User"].ToString() == "") return false;
            VolunteerBL volunteer = new VolunteerBL(Session["User"].ToString());
            return volunteer.Type.PermmisionVolunteer;
        }

        //delete file from database and physically from server
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //get selected button
            Button deleteBtn = (Button)sender;
            //get row of selected button
            GridViewRow row = (GridViewRow)deleteBtn.NamingContainer;

            //get fullname(filename + formatname) of selected file 
            string fileName = row.Cells[0].Text + "." + row.Cells[1].Text;

            //try to delete file
            try
            {
                //delete file
                MediaBL.DeleteFile(new MediaBL(fileName).ActivityCode, fileName);
                //redirect to media page for refresh gridview
                Response.Redirect("http://localhost:60437/MediaUI.aspx");
            }
            catch (Exception ex)
            {
                //throw error
                ErrorsLabel1.Text = "ERROR: " + ex.Message.ToString();
            }
        }

        //create gridview for eace folder of activity from the main folder
        protected void ShowMedia(Control container)
        {
            //get all the subfolders from the main folder
            string[] subDirs = System.IO.Directory.GetDirectories(Server.MapPath("~/Files/"));

            //for eace subfolder get foldername
            string[] foldersName = new string[subDirs.Length];
            for (int i = 0; i < subDirs.Length; i++)
            {
                int from = 0;
                bool found = false;
                for (int j = subDirs[i].Length-1; j >= 0; j--)
                {
                    //get first char of the folder name
                    if (subDirs[i][j] == '\\' || subDirs[i][j] == '/')
                    {
                        from = j;
                        found = true;
                        break;
                    }
                }
                //substring - cut all path before the foldername, and stay with foldername
                if(found)
                    foldersName[i] = subDirs[i].Substring(from+1);
            }

            //for each foldername create a label
            string[] labelsName = new string[foldersName.Length];
            for (int i = 0; i < foldersName.Length; i++)
            {
                //get activitycode by foldername(activityname + activitycode)
                int activityCode = int.Parse(foldersName[i].Split(' ').Last());
                //get activity details
                ActivityBL act = new ActivityBL(activityCode);
                //create lable by activityname, place and date(without hour)
                labelsName[i] = act.ActivityName + " - " + act.Place + " - " + act.ActivityDate.ToString().Substring(0, act.ActivityDate.ToString().Length - 9);
            }

            //add all gridviews and labels to container Controls
            for (int i = 0; i < foldersName.Length; i++)
            {
                //create label
                Label nameLb = new Label();
                //add label name and h4(heading size: 4) brackets
                nameLb.Text = "<h4>" + labelsName[i] + "</h4>";
                //add label to container Controls
                container.Controls.Add(nameLb);

                //create gridview for folder
                CreateGridView(hey, foldersName[i]);

                //create break label for more beautiful page look
                Label breakLb = new Label();
                breakLb.Text = "<br /><br /><br />";
                //add break label to container Controls
                container.Controls.Add(breakLb);
            }
            
        }

        //upload file to server and database, and in some cases create new activity folder/directory.
        protected void BtnUpload1(object sender, EventArgs e)
        {
            //check if server can get chosen file
            if (FileUpload1.HasFile)
            {
                //get filetype
                int fileType = MediaBL.CheckFormatValidation(FileUpload1.PostedFile.FileName);
                //check if filetype is valid
                if (fileType != -1)
                {
                    string fullPath = "";
                    //get activitycode by chosen activity from DropDownList
                    int activityCode = int.Parse(ChooseActivity.SelectedValue.ToString());
                    //try to get or create new activity folder/directory.
                    try
                    {
                        //check if activity was chosen
                        if (ChooseActivity.Text != "")
                        {
                            //get full path and in some cases create new activity folder/directory
                            fullPath = MediaBL.NewActivityDir(activityCode);
                        }
                        else
                        {
                            //throw an error message and return
                            ErrorsLabel1.ForeColor = System.Drawing.Color.Red;
                            ErrorsLabel1.Text = "בחר פעילות משויכת!";
                            return;
                        }
                    }
                    catch (Exception e1)
                    {
                        //throw an error message
                        ErrorsLabel1.Text = "ERROR: " + e1.Message.ToString();
                    }
                    //try to check file size and save file in server
                    try
                    {
                        //if file size is bigger than 100mb - throw an error
                        if(FileUpload1.PostedFile.ContentLength > 100000000)
                        {
                            //throw an error message
                            ErrorsLabel1.ForeColor = System.Drawing.Color.Red;
                            ErrorsLabel1.Text = "קובץ גדול מהגודל המותר להעלאה!";
                        }
                        //else if file path is valid - save in server and in database
                        else if (fullPath != "")
                        {
                            //save file in activity folder that chosen before(in sever)
                            FileUpload1.SaveAs(fullPath + "\\" + FileUpload1.FileName);

                            //save file details in database
                            MediaBL newFile = new MediaBL(FileUpload1.PostedFile.FileName, activityCode, fileType);
                            
                            //redirect to media page for refresh gridview
                            Response.Redirect("http://localhost:60437/MediaUI.aspx");
                        }
                        else
                        {
                            //throw an error message
                            ErrorsLabel1.ForeColor = System.Drawing.Color.Red;
                            ErrorsLabel1.Text = "קובץ לא נשמר בהצלחה..";
                        }
                    }
                    catch (Exception e2)
                    {
                        //throw an error message
                        ErrorsLabel1.Text = "ERROR: " + e2.Message.ToString();
                    }
                }

                else
                {
                    //throw an error message
                    ErrorsLabel1.ForeColor = System.Drawing.Color.Red;
                    ErrorsLabel1.Text = "פורמט לא חוקי!";
                }
            }

            else
            {
                //throw an error message
                ErrorsLabel1.Text = "קובץ לא זוהה כראוי..";
            }
        }

        //fill activity DropDownList with activitys for media
        protected void FillActivityList()
        {
            //clear list
            ChooseActivity.Items.Clear();
            //load activitys
            ActivitysBL activitys = new ActivitysBL();
            //add all activitys to the list
            foreach (DataRow act in activitys.Activitys.Tables[0].Rows)
            {
                //create identify for each activity item text
                string text = act["ActivityName"].ToString() + " - " + act["Place"].ToString() + " - " + act["ActivityDate"].ToString().Substring(0, act["ActivityDate"].ToString().Length - 9);
                //add item to DropDownList
                ChooseActivity.Items.Add(new ListItem(text, act["ActivityCode"].ToString()));
            }
            //bind items
            ChooseActivity.DataBind();
        }

        //create and return a limitation string for uploaded files
        protected string CreateLimitationString()
        {
            //valid files
            string limitString = "סוגי הקבצים שניתן להעלות:<br />";
            limitString += MediaBL.LimitString() + "<br /><br />";
            //max size of file
            limitString += "גודל מקסימלי מותר להעלאה: 100mb";
            return limitString;
        }
    }
}