using MonitorNetwork.Database;
using MonitorNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace MonitorNetwork.Controllers
{
    public class LoginController : Controller
    {
        private MNDatabase db = new MNDatabase();

        //Login GET Method 

        [HttpGet]
        public ActionResult Index(string returnURL)
        {
            var userinfo = new LoginModel();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.returnURL = returnURL;

                return View(userinfo);
            }
            catch
            {
                throw;
            }
        }

        // Login POST Method

        [HttpPost]
        public ActionResult Index(LoginModel entity)
        {
            // Ensure we have a valid viewModel to work with
            if (!ModelState.IsValid)
                return View(entity);

            try
            {
                //Retrive Stored HASH Value From Database According To Username (one unique field)
                var userInfo = db.user.Where(s => s.username == entity.username.Trim() && !s.isBlocked).FirstOrDefault();

                if (userInfo != null && userInfo.password.Equals(entity.password))
                {

                    //Set A Unique ID in session
                    Session["UserID"] = userInfo.userID;

                    // If we got this far, something failed, redisplay form
                    // return RedirectToAction("Index", "Dashboard");

                    TempData["username"] = userInfo.username;
                    TempData["returnURL"] = entity.returnURL;

                    return RedirectToAction("SecurityQuestion");
                }
                else
                {
                    //Login Fail
                    TempData["ErrorMSG"] = "Access Denied! Wrong Credentials";
                    return View(entity);
                }
            }
            catch
            {
                throw;
            }

        }

        //POST: Logout
        [HttpPost]
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult SecurityQuestion()
        {
            if(TempData["username"] == null)
            {
                return RedirectToAction("Index");
            }

            string username = TempData["username"].ToString();
            string returnURL = TempData["returnURL"]?.ToString();

            var user = db.user.Where(x => x.username.Equals(username)).FirstOrDefault();

            var viewmodel = new SecurityQuestion()
            {
                username = username,
                returnURL = returnURL
            };

            SetRandomSecurityQuestion(user, viewmodel);

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult SecurityQuestion(SecurityQuestion viewmodel)
        {
            if (!ModelState.IsValid)
                return View(viewmodel);

            var user = db.user.Where(x => x.username.Equals(viewmodel.username)).FirstOrDefault();

            int askedQuestionId = viewmodel.askedQuestions.Last();

            bool isCorrectAnswer = false;

            switch(askedQuestionId)
            {
                case 1:
                    isCorrectAnswer = viewmodel.answer.Equals(user.answer1);
                    break;
                case 2:
                    isCorrectAnswer = viewmodel.answer.Equals(user.answer2);
                    break;
                case 3:
                    isCorrectAnswer = viewmodel.answer.Equals(user.answer3);
                    break;
            }

            if (isCorrectAnswer)
            {
                //Login Success
                //For Set Authentication in Cookie (Remeber ME Option)
                SignInRemember(viewmodel.username);

                return RedirectToLocal(viewmodel.returnURL);
            }

            if(viewmodel.askedQuestions.Count >= 2)
            {
                user.isBlocked = true;

                db.SaveChanges();

                TempData["ErrorMSG"] = "User was blocked!";
                return RedirectToAction("Index");
            }

            SetRandomSecurityQuestion(user, viewmodel);

            return View(viewmodel);
        }

        private void SetRandomSecurityQuestion(user user, SecurityQuestion viewmodel)
        {
            Random rnd = new Random();
            int randomSecurityQuestion = rnd.Next(1, 4);

            while(viewmodel.askedQuestions.Contains(randomSecurityQuestion))
            {
                randomSecurityQuestion = rnd.Next(1, 4);
            }

            switch (randomSecurityQuestion)
            {
                case 1:
                    viewmodel.question = user.security1;
                    viewmodel.askedQuestions.Add(1);
                    break;
                case 2:
                    viewmodel.question = user.security2;
                    viewmodel.askedQuestions.Add(2);
                    break;
                case 3:
                    viewmodel.question = user.security3;
                    viewmodel.askedQuestions.Add(3);
                    break;
            }
        }

        //GET: EnsureLoggedOut
        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                throw;
            }
        }

        //GET: SignInAsync   
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }

    }
}