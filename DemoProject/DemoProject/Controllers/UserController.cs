using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult saveUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult saveUser(UserModel foUserModel)
        {

            if (ModelState.IsValid)
            {

                try
                {
                    UserDbContext loUserContext = new UserDbContext();
                    if (loUserContext != null)
                    {

                        string lstUserName = combineString(foUserModel.stFirstName, foUserModel.stLastName);
                        SqlParameter[] loSqlParameter = new SqlParameter[]
                        {
                    new SqlParameter("@inCount",foUserModel.inCount = 0),
                    new SqlParameter("@inUserId",foUserModel.inUserId),
                    new SqlParameter("@stUserName",lstUserName.Trim()),
                    new SqlParameter("@dtUserBirthDate",foUserModel.dtUserBirthDate),
                    new SqlParameter("@stUserEmail",foUserModel.stUserEmail.Trim()),
                    new SqlParameter("@stUserPassword",foUserModel.stUserPassword.Trim()),
                    new SqlParameter("@stCreatedBy",foUserModel.stCreatedBy = "User"),
                    new SqlParameter("@dtCreationDate", foUserModel.dtUserCreationDate = DateTime.Now),
                    new SqlParameter("@dtUserModificationDate", foUserModel.dtUserModificationDate = "Not Modified Yet"),
                    new SqlParameter("@flgIsActive", foUserModel.flgIsActive = true)
                        };

                        var loSaveUser = loUserContext.Database.SqlQuery<UserDbContext>("saveUser @inCount, @inUserId, @stUserName, @dtUserBirthDate, @stUserEmail, @stUserPassword, @stCreatedBy, @dtCreationDate, @dtUserModificationDate, @flgIsActive", loSqlParameter).ToList();
                        if (loSaveUser != null)
                        {
                            ModelState.Clear();
                            return RedirectToAction("userListing");

                        }
                    }
                }
                catch (SqlException)
                {
                    ViewBag.Msg = "Email Already Exists";

                }
            }
            return View();
        }

        private string combineString(string fsFirstName, string fsLastName)
        {
            return fsFirstName + " " + fsLastName;
        }
        public ActionResult userListing(string fsSearch, string fstSortOrder="", string fstSortColumn = "", int inPageIndex = 1)
        {

            if (ModelState.IsValid)
            {
                UserDbContext loUserContext = new UserDbContext();

                if (loUserContext != null)
                {

                    ViewBag.stSortOrder = fstSortOrder;
                    ViewBag.inPageIndex = inPageIndex;
                    //Sorting ASC DESC
                    sortOrder(fstSortOrder, loUserContext);
                    string lsSearch = "";



                    List<UserDbContext> loTotalRecordCount = loUserContext.Database.SqlQuery<UserDbContext>("[getTotalRecordCount]").ToList();
                    if (loTotalRecordCount.Count > 0)
                    {
                        double ldTtotalCount = loTotalRecordCount.Count;
                        ViewBag.Msg = ldTtotalCount;
                        ViewBag.lblPages = Math.Ceiling(ldTtotalCount / 10.0);
                        double liPageCount = ViewBag.lblPages;


                        if (fsSearch != null)
                        {
                            lsSearch = loUserContext.loUserModel.First().stSearch = fsSearch;
                        }


                        SqlParameter[] loSqlParameter = new SqlParameter[]
                        {
                    new SqlParameter("@stSearch",lsSearch),
                    new SqlParameter("@stSortColumn",loUserContext.loUserModel.First().stSortColumn = fstSortColumn.ToString()),
                    new SqlParameter("@stSortOrder",loUserContext.loUserModel.First().stSortOrder ),
                    new SqlParameter("@inPageIndex",loUserContext.loUserModel.First().inPageIndex = inPageIndex),
                    new SqlParameter("@inPageSize",10)

                        };

                        List<UserModel> loData = loUserContext.Database.SqlQuery<UserModel>("getUsers @stSearch, @stSortColumn, @stSortOrder, @inPageIndex, @inPageSize", loSqlParameter).ToList();

   
                        ModelState.Clear();
                        return View(loData);
                    }

                }
            }

            return View();
        }

        private static void sortOrder(string fstSortOrder, UserDbContext loUserContext)
        {
            switch (fstSortOrder)
            {
                case "ASC":
                    {
                        loUserContext.loUserModel.First().stSortOrder = "DESC";
                        break;
                    }
                case "DESC":
                    {
                        loUserContext.loUserModel.First().stSortOrder = "ASC";
                        break;
                    }
                default:
                    {
                        loUserContext.loUserModel.First().stSortOrder = "ASC";
                        break;
                    }
            }
        }

        public ActionResult editUser(int fiId)
        {
            if (fiId > 0)
            {
                UserDbContext loUserContext = new UserDbContext();
                UserModel loUserModel = loUserContext.loUserModel.Find(fiId);


                if (loUserModel != null)
                {

                    if (loUserModel.stUserName.Contains(" "))
                    {
                        string lsFullName = loUserModel.stUserName;
                        string lsFirstName = lsFullName.Substring(0, lsFullName.IndexOf(" "));
                        var lsLastName = lsFullName.Substring(lsFullName.IndexOf(" ") + 1);
                        loUserModel.stFirstName = lsFirstName;
                        loUserModel.stLastName = lsLastName;
                    }

                   // loUserModel.dtUserBirthDate.;

                    return View(loUserModel);
                }

            }
            return View();
        }

        [HttpPost]
        public ActionResult editUser(UserModel foUserModel)
        {

            if (ModelState.IsValid)
            {
                UserDbContext loUserContext = new UserDbContext();

                if (loUserContext != null)
                {
                    loUserContext.loUserModel.Add(foUserModel);
                    string lstUserName = combineString(foUserModel.stFirstName, foUserModel.stLastName);
                    SqlParameter[] loSqlParameter = new SqlParameter[]
                    {
                    new SqlParameter("@inCount",foUserModel.inCount = 1),
                    new SqlParameter("@inUserId",foUserModel.inUserId),
                    new SqlParameter("@stUserName",lstUserName),
                    new SqlParameter("@dtUserBirthDate",foUserModel.dtUserBirthDate),
                    new SqlParameter("@stUserEmail",foUserModel.stUserEmail.Trim()),
                    new SqlParameter("@stUserPassword",foUserModel.stUserPassword.Trim()),
                    new SqlParameter("@stCreatedBy",foUserModel.stCreatedBy = "Admin"),
                    new SqlParameter("@dtCreationDate", foUserModel.dtUserCreationDate = DateTime.Now),
                    new SqlParameter("@dtUserModificationDate", foUserModel.dtUserModificationDate = DateTime.Now.ToString()),
                    new SqlParameter("@flgIsActive", foUserModel.flgIsActive = true)
                    };

                    var loSaveUserData = loUserContext.Database.SqlQuery<UserDbContext>("saveUser @inCount, @inUserId, @stUserName, @dtUserBirthDate, @stUserEmail, stUserPassword, @stCreatedBy, @dtCreationDate, @dtUserModificationDate, @flgIsActive", loSqlParameter).ToList();
                    if (loSaveUserData != null)
                    {
                        ModelState.Clear();
                        return RedirectToAction("userListing");
                    }

                }
            }
            return View();
        }
        public ActionResult deleteUser(int fiId)
        {

            if (fiId > 0)
            {

                UserDbContext loUserContext = new UserDbContext();

                if (loUserContext != null)
                {
                    UserModel loUserModel = loUserContext.loUserModel.Find(fiId);

                    if (ModelState.IsValid)
                    {
                        SqlParameter[] loSqlParameter = new SqlParameter[]
                    {
                            new SqlParameter("@inUserId",loUserModel.inUserId),
                            new SqlParameter("@dtUserDeletionDate",loUserModel.dtUserDeletionDate = DateTime.Now),
                            new SqlParameter("@flgIsActive",loUserModel.flgIsActive = false)
                    };
                        var loData = loUserContext.Database.SqlQuery<UserDbContext>("deleteUser @inUserId, @dtUserDeletionDate, @flgIsActive", loSqlParameter).ToList();
                        if (loData != null)
                        {
                            ModelState.Clear();
                            return RedirectToAction("userListing");
                        }

                    }

                }
            }


            return RedirectToAction("userListing");

        }


    }
}