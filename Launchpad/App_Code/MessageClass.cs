using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;



/// <summary>
/// Summary description for MessageClass
/// </summary>
public class MessageClass
{
    public MessageClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SendMail(string subject, string toEmail, string body, bool IsHTML)
    {
        MailMessage objMailMessage = new MailMessage();

        MailAddress maFrom = new MailAddress("info@fintechhub.info", "Fintech Hub");

        objMailMessage.From = maFrom;
        objMailMessage.To.Add(new MailAddress(toEmail));
        objMailMessage.Bcc.Add(new MailAddress("skylineshanghai@yahoo.com"));

        objMailMessage.Subject = subject;
        objMailMessage.Body = body;
        objMailMessage.IsBodyHtml = IsHTML;

        SmtpClient client = new SmtpClient("smtp.exmail.qq.com");
        client.Credentials = new NetworkCredential("info@fintechhub.info", "Info123");

        client.Send(objMailMessage);
    }

    public static void SendMail(string cusEmail, string cusPsw)
    {
        string mailbody = CreateEmailCentenxt(cusEmail, cusPsw);
        string subject = "重置登录密码";
        SendMail(subject, "cliu@hqrms.com", mailbody, true);
    }

    public static string FormulateEmailBody(string strName, string strMessage) {
        string strReturn = "";

        return strReturn;
    }
    
    /// <summary>
    /// 创建邮件内容
    /// </summary>
    /// <returns></returns>
    private static String CreateEmailCentenxt(string cusEmail, string cusPsw)
    {
        string bodyContent = " <div style=\"width: 100%; background-color: #f5f5f5;margin: 10px auto;\">"
                 + "<div class=\"divto\" style=\"width: 80%; border: 1px solid #CDCDC1; background-color: #EEE9E9;margin: 10px auto;\">"
                     + "<div style=\"width: 680px; overflow: hidden; background-color:white;margin: 10px auto;\">"
                         + "<div style=\" margin: auto auto;\">"
                             + "<div style=\"width: 655px; margin: 20px auto;\">"
                                 + "<a href=\"http://www.hqrms.com\" target=\"_blank\" style=\"text-decoration:none;\">"
                                     + "<img src=\"cid:Pichqlogo\">"
                                 + "</a>"
                             + "</div>"
                         + "</div>"
                         + "<div style=\"text-align: left; margin: auto 1px;\">"
                             + "<div style=\"margin: auto 40px; \">"
                                 + "<span style=\"font-family: 宋体正文; font-size: 15px;\">"
                                 + "<br /><br />"
                                 + cusEmail + " 您好！"
                                  + "<br /><br />"
                                  + "为确保是您本人操作，您已选择通过该邮件地址获取验证码验证身份。"
                                  + "<br /><br />"
                                  + "请在邮件验证码输入框输入下方验证码：" + cusPsw
                                 + "<br /><br />"
                                 + "<br /><br />"
                                 + "<br /><br />"
                                 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                 + " 勿向任何人泄露您收到的验证码。验证码会在邮件发送30分钟后失效。"
                                 + "</span>"
                             + "</div>"
                         + "<div>"
                             + "<div style=\"float: left; padding: 0px 10px;\">"

                                 + "<br />"
                                 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                 + "<p style=\"font-size: 12pt; font-weight: bold;\">"
                                 + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                 + "*本邮件为系统自动发送，请勿回复。"
                                 + "</p>"

                             + "</div>"
                             + "<div style=\"clear: both\"></div>"
                         + "</div>"
                         + "</div>"
                     + "</div>"
                 + "</div>"
             + "</div>";

        return bodyContent = string.Format(bodyContent, cusEmail, cusPsw);
    }

    public static bool IsValid(string emailaddress)
    {
        try
        {
            System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static string InsertFeedback(int type, string name, string email, string msg, string phone, string url)
    {
        SqlConnection con1;
        SqlCommand cmdInsert = new SqlCommand();
        SqlParameter parmCompanyID;

        con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["feedback"].ConnectionString);
        cmdInsert.Connection = con1;
        cmdInsert.CommandType = CommandType.Text;
        if (type == 1)
        {
            cmdInsert.CommandText = "Insert into feedbackLog(feedbackType, email, fullName, feedbackMsg) values(1, @e,  @n, @m); SELECT @id = @@Identity";
            cmdInsert.Parameters.AddWithValue("@e", email);
            cmdInsert.Parameters.AddWithValue("@n", name);
            cmdInsert.Parameters.AddWithValue("@m", msg);
        }
        else if (type == 2)
        {
            cmdInsert.CommandText = "Insert into feedbackLog(feedbackType, email, fullName, phone) values(2, @e,  @n, @p); SELECT @id = @@Identity";
            cmdInsert.Parameters.AddWithValue("@e", email);
            cmdInsert.Parameters.AddWithValue("@n", name);
            cmdInsert.Parameters.AddWithValue("@p", phone);
        }
        else if (type == 3)
        {
            cmdInsert.CommandText = "Insert into feedbackLog(feedbackType, email, fullName, feedbackMsg, page) values(3, @e,  @n, @m, @u); SELECT @id = @@Identity";
            cmdInsert.Parameters.AddWithValue("@e", email);
            cmdInsert.Parameters.AddWithValue("@n", name);
            cmdInsert.Parameters.AddWithValue("@m", msg);
            cmdInsert.Parameters.AddWithValue("@u", url);
        }

        parmCompanyID = cmdInsert.Parameters.Add("@id", SqlDbType.Int);
        parmCompanyID.Direction = ParameterDirection.Output;

        con1.Open();
        cmdInsert.ExecuteNonQuery();
        con1.Close();

        return parmCompanyID.Value.ToString();
    }


    public static bool IsSpammer()
    {
        bool boolReturn = false;
        //
        boolReturn = false;

        return boolReturn;
    }

}