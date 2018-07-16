using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{
    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    //public object ValidationLabelPassed { get; private set; }   

    protected void Page_Init(object sender, EventArgs e)
    {
        // The code below helps to protect against XSRF attacks
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        Guid requestCookieGuidValue;
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            // Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            // Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;

            var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            {
                HttpOnly = true,
                Value = _antiXsrfTokenValue
            };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }
            Response.Cookies.Set(responseCookie);
        }
        
        Page.PreLoad += master_Page_PreLoad;
    }

    protected void master_Page_PreLoad(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            // Set Anti-XSRF token
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            
        }
        else
        {
            // Validate the Anti-XSRF token
            if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            {
                throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                
            }
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ModalPopupExtender1.Hide();
        if (!Page.IsPostBack)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh-CN");
            TurnCapchaOnOff(MessageClass.IsSpammer());
        }
    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        Context.GetOwinContext().Authentication.SignOut();
    }



    protected void lbEnglish_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["EnglishButtonID"];
            if (cookie == null || cookie.Value != lbEnglish.UniqueID)
            {
                cookie = new HttpCookie("EnglishButtonID");
                cookie.Value = lbEnglish.UniqueID;
                Response.SetCookie(cookie);
                //Response.Write(" English cookie: " + cookie.Value + "<br/>");
            }
        }

        //Response.Write(" English button: " + lbEnglish.UniqueID + "<br/>");
        var language = Thread.CurrentThread.CurrentUICulture.Name;
        if (language == lbEnglish.CommandArgument)
        {
            lbEnglish.Visible = false;
            lbChinese.Visible = true;
            //lbJapanese.Visible = true;
        }
    }

    protected void lbChinese_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["ChineseButtonID"];
            if (cookie == null || cookie.Value != lbChinese.UniqueID)
            {
                cookie = new HttpCookie("ChineseButtonID");
                cookie.Value = lbChinese.UniqueID;
                Response.SetCookie(cookie);
            }
        }

        var language = Thread.CurrentThread.CurrentUICulture.Name;
        if (language == lbChinese.CommandArgument)
        {
            lbEnglish.Visible = true;
            lbChinese.Visible = false;
            //lbJapanese.Visible = true;
        }
    }


    protected void lbEnglish_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["SelectedLanguage"];

        if (cookie == null)
            cookie = new HttpCookie("SelectedLanguage");

        cookie.Value = lbEnglish.CommandArgument;
        cookie.Expires = DateTime.Today.AddMonths(6);
        Response.SetCookie(cookie);
    }



    protected void lbChinese_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["SelectedLanguage"];

        if (cookie == null)
            cookie = new HttpCookie("SelectedLanguage");

        cookie.Value = lbChinese.CommandArgument;
        cookie.Expires = DateTime.Today.AddMonths(6);
        Response.SetCookie(cookie);
    }


    //*********************************************************


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
        else
        {
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