using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TurnCapchaOnOff(MessageClass.IsSpammer());

           
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
        bool boolReturn = true;
        
       

        if (MessageClass.IsSpammer())
        {
            bool isHuman = err_captchaBox.Validate(err_txtCaptcha.Text);
            err_txtCaptcha.Text = null;

            if (!isHuman)
            {
                err_lblError.Text = "Invalid Captcha!";
                boolReturn = false;
            }
        }

        
     
            if (string.IsNullOrEmpty(err_txtName.Text) && boolReturn)
            {

                err_lblError.Text = "Please Enter Name";
                boolReturn = false;
            }

            if ((string.IsNullOrEmpty(err_txtEmail.Text) || !MessageClass.IsValid(err_txtEmail.Text)) && boolReturn)
            {
                err_lblError.Text = "Invalid Email!";
                boolReturn = false;
            }

            if (string.IsNullOrEmpty(err_txtMessage.Text) && boolReturn)
            {
                err_lblError.Text = "Please Enter Description";
                boolReturn = false;
            }



        //if (!string.IsNullOrEmpty(txtName_CN.Text))
        //{
        //    lblError.Text = "Please Enter Glossary";
        //    boolReturn = false;
        //}

        if (boolReturn)
        {

            string strID = MessageClass.InsertFeedback(3, err_txtName.Text, err_txtEmail.Text, err_txtMessage.Text, null, HttpContext.Current.Request.Url.AbsoluteUri);
            string body = "Error reported from " + err_txtName.Text + "\n\n";
            body += "Error message: \n " + err_txtMessage.Text + "\n\n";
            body += "Contact email: " + err_txtEmail.Text;
            MessageClass.SendMail("Error reported on: " + HttpContext.Current.Request.Url.AbsoluteUri, "kearacole@my.uri.edu", body, false);
            err_txtName.Enabled = false;
            err_txtEmail.Enabled = false;
            err_txtMessage.Enabled = false;
            err_btnOkay.Enabled = false;
            err_txtCaptcha.Enabled = false;
            err_lblError.Text = "Thank You!";
        }
        else {
            TurnCapchaOnOff(MessageClass.IsSpammer());
        }

        
            
    }   

    protected void clearAllERR()
    {
       
        err_txtName.Text = "";
        err_txtEmail.Text = "";
        err_txtMessage.Text = "";

        err_txtName.Enabled = true;
        err_txtEmail.Enabled = true;
        err_txtMessage.Enabled = true;
        err_btnOkay.Enabled = true;
        err_txtCaptcha.Enabled = true;





        err_lblError.Text = "";
     
    }

    protected void ERRbtnCancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
        clearAllERR();
        TurnCapchaOnOff(MessageClass.IsSpammer());
    }


    protected void TurnCapchaOnOff(bool isSpam) {
        if (isSpam) {
           err_captchaBox.Visible = true;
           err_Captchalbl.Visible = true;
           err_txtCaptcha.Visible = true;
        }
        else {
            err_captchaBox.Visible = false;
            err_Captchalbl.Visible = false;
            err_txtCaptcha.Visible = false;
        }
    }
}