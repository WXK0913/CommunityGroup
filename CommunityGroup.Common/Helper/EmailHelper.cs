using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommunityGroup.Common
{
    public class EmailHelper
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="mail">邮箱地址</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static string SendMailResetPwd(string userName, string mail, string code)
        {
            var result = string.Empty;
            try
            {
                var smtp = new SmtpClient("smtp.163.com", 25)//默认的smtp协议地址和端口。如果有的企业邮箱他提供的不是25默认，请用提供的地址
                {
                    //注意此处为：邮箱的授权码，不是密码，前面这个参数是邮箱的用户名，不要写全称。如w15862528968@163.com，这个是全称，只写前面的用户名就可以了。w15862528968
                    Credentials = new NetworkCredential("xueyifan1018", "KUKNCUSUIIEPBUTE"),
                    EnableSsl = true
                };
                var mm = new MailMessage();
                //发送人
                mm.From = new MailAddress("xueyifan1018@163.com", "小薛的邮箱");//前面是发送邮箱的发送人地址，后面是发送人的名称
                
                //收件人
                mm.To.Add(new MailAddress(mail, userName));
                mm.Subject = "重置密码 - codoke客服";//邮箱的标题
                mm.SubjectEncoding = Encoding.UTF8;
                mm.IsBodyHtml = true;
                mm.BodyEncoding = Encoding.UTF8;
                var sbEmail = new StringBuilder();
               //string v= string.Format(System.IO.File.ReadAllText(""),code,userName,);
                //邮箱里是可以定义一个html标签、如链接、图片等等。这一块是否也可以做到配置文件里。但是这个要做一个txt的配置文件通过文件操作读取比较好。
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:15px;'> 您好，您正在重置登录用户名为 <span style='font-weight:bolder'>" + userName + "</span> 的生活团购账户密码。</div>");
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:15px;'> 您的验证码为: <span style='color:red'>" + code + "</span> ，有效时间为5分钟，请在有效时间内进行验证。</div>");
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:35px;'>【警告】：请不要把验证码泄露给其他人，因个人泄露验证码造成的损失，由个人负责！</div>");
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:15px;'> --</div>");
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:15px;'> 生活团购(www.codoke.com) - 生活团购平台</div>");
                sbEmail.Append("<div style='text-indent:24px;padding-bottom:35px;'> 让经验创造财富，让源码创造价值！</div>");
                mm.Body = sbEmail.ToString();
                //设置邮件发送格式
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}