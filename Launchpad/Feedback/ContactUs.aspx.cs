using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ContactUs : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TurnCapchaOnOff(MessageClass.IsSpammer());


        }
    }

    protected void btnContact_Click(object sender, EventArgs e)
    {
       
        ModalPopupExtender1.Show();
        bool boolReturn = true;
        
        
        if (string.IsNullOrEmpty(cu_txtName.Text) && boolReturn)
        {
            
            cu_lblError.Text = "Please Enter Name";
            boolReturn = false;
            
        }

        if ((string.IsNullOrEmpty(cu_txtEmail.Text) || !MessageClass.IsValid(cu_txtEmail.Text)) && boolReturn)
        {

            cu_lblError.Text = "Invalid Email";
            boolReturn = false;
        }

        if (string.IsNullOrEmpty(cu_txtMessage.Text) && boolReturn)
        {

            cu_lblError.Text = "Please Enter Message";
            boolReturn = false;
        }

        if (MessageClass.IsSpammer())
        {
            bool isHuman = cu_captchaBox.Validate(cu_txtCaptcha.Text);
            cu_txtCaptcha.Text = null;

            if (!isHuman)
            {
                if (string.IsNullOrEmpty(cu_txtName.Text))
                {
                    cu_lblError.Text = "Please Enter a Name!";
                    boolReturn = false;
                }
                else if (string.IsNullOrEmpty(cu_txtEmail.Text))
                {
                    cu_lblError.Text = "Invalid Email";
                    boolReturn = false;
                }
                else if (string.IsNullOrEmpty(cu_txtMessage.Text))
                {
                    cu_lblError.Text = "Please Enter Message";
                    boolReturn = false;
                }
                else 
                cu_lblError.Text = "Invalid Captcha!";
                boolReturn = false;
            }
        }

        //if (!string.IsNullOrEmpty(txtName_CN.Text))
        //{
        //    lblError.Text = "Please Enter Glossary";
        //    boolReturn = false;
        //}

        if (boolReturn)
        {


            string strID = MessageClass.InsertFeedback(1, cu_txtName.Text, cu_txtEmail.Text, cu_txtMessage.Text, null, null);

            string body = "Users name: " + cu_txtName.Text + "\n\n";
            body += "Message: \n " + cu_txtMessage.Text + "\n\n";
            body += "Contact email: " + cu_txtEmail.Text;
            MessageClass.SendMail("New message from user! ", "kearacole@my.uri.edu", body, false);


            cu_txtName.Enabled = false;
            cu_txtEmail.Enabled = false;
            cu_txtMessage.Enabled = false;
            cu_btnOkay.Enabled = false;
            cu_txtCaptcha.Enabled = false;
            cu_lblError.Text = "Thank You!";
        }
        else {
            TurnCapchaOnOff(MessageClass.IsSpammer());
        }
      


    }

    protected void clearAllCU()
    {
        cu_txtName.Text = "";
        cu_txtEmail.Text = "";
        cu_txtMessage.Text = "";

        cu_txtName.Enabled = true;
        cu_txtEmail.Enabled = true;
        cu_txtMessage.Enabled = true;
        cu_btnOkay.Enabled = true;
        cu_txtCaptcha.Enabled = true;




        cu_lblError.Text = "";
  
    }

    protected void CUbtnCancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        clearAllCU();
        TurnCapchaOnOff(MessageClass.IsSpammer());
    }

    protected void TurnCapchaOnOff(bool isSpam)
    {
        if (isSpam)
        {
            cu_captchaBox.Visible = true;
            cu_Captchalbl.Visible = true;
            cu_txtCaptcha.Visible = true;
        }
        else
        {
            cu_captchaBox.Visible = false;
            cu_Captchalbl.Visible = false;
            cu_txtCaptcha.Visible = false;
        }
    }
}