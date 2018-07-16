using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Subscribe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            TurnCapchaOnOff(MessageClass.IsSpammer());

        }
    }

    protected void btnSubscribe_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Show();
        bool boolReturn = true;
        

       

        if (MessageClass.IsSpammer()) {
            bool isHuman = sub_captchaBox.Validate(sub_txtCaptcha.Text);
            sub_txtCaptcha.Text = null;

            if (!isHuman)
            {
                sub_lblError.Text = "Invalid Captcha!";
                boolReturn = false;
            }
        }

        
       
            if (string.IsNullOrEmpty(sub_txtName.Text) && boolReturn)
            {

                sub_lblError.Text = "Please Enter Name";
                boolReturn = false;

            }

            if ((string.IsNullOrEmpty(sub_txtEmail.Text)|| !MessageClass.IsValid(sub_txtEmail.Text)) && boolReturn)
            {
                sub_lblError.Text = "Invalid Email!";
                boolReturn = false;
            }



        //if (!string.IsNullOrEmpty(txtName_CN.Text))
        //{
        //    lblError.Text = "Please Enter Glossary";
        //    boolReturn = false;
        //}

        if (boolReturn)
        {


            string strID = MessageClass.InsertFeedback(2, sub_txtName.Text, sub_txtEmail.Text, null, sub_txtPhone.Text, null);
            string body = "New subscriber name: " + sub_txtName.Text + "\n";
            body += "Contact email: " + sub_txtEmail.Text + "\n";
            body += "Phone number: " + sub_txtPhone.Text;
            MessageClass.SendMail("New Subscriber!", "kearacole@my.uri.edu", body, false);

            sub_txtName.Enabled = false;
            sub_txtEmail.Enabled = false;
            sub_txtPhone.Enabled = false;
            sub_btnOkay.Enabled = false;
            sub_txtCaptcha.Enabled = false;
            sub_lblError.Text = "Thank You!";
        }
        else {
            TurnCapchaOnOff(MessageClass.IsSpammer());

        }

        

     

    }

    protected void clearAllSUB()
    {
  
        sub_txtName.Text = "";
        sub_txtEmail.Text = "";
        sub_txtPhone.Text = "";


        sub_txtName.Enabled = true;
        sub_txtEmail.Enabled = true;
        sub_txtPhone.Enabled = true;
        sub_btnOkay.Enabled = true;
        sub_txtCaptcha.Enabled = true;

        sub_lblError.Text = "";
    }

    protected void SUBbtnCancel_Click(object sender, EventArgs e)
    {
        ModalPopupExtender3.Hide();
        clearAllSUB();
        TurnCapchaOnOff(MessageClass.IsSpammer());


    }


    protected void TurnCapchaOnOff(bool isSpam)
    {
        if (isSpam)
        {
            sub_captchaBox.Visible = true;
            sub_Captchalbl.Visible = true;
            sub_txtCaptcha.Visible = true;
        }
        else
        {
            sub_captchaBox.Visible = false;
            sub_Captchalbl.Visible = false;
            sub_txtCaptcha.Visible = false;
        }
    }

}