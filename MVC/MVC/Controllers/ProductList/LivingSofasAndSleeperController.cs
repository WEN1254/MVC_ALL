﻿using MVC.Models;
using MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers.ProductList
{
    public class LivingSofasAndSleeperController : Controller
    {
        // GET: LivingSofasAndSleeper
        public ActionResult Index()
        {
            using (mvcContext db = new mvcContext())
            {

                string sqlQuery = @"select p.ProductID ,p.ProductName,ps.Price,ps.Image,p.Quantity
                                    from Products p
                                    join  (select *
                                    from(
	                                    select ROW_NUMBER()OVER(PARTITION BY [ProductID],[Price] ORDER by [ProductID])as rown,*
	                                    FROM [ProductSpecifications])AS T1
	                                    WHERE rown = 1) ps 
                                    on ps.ProductID=p.ProductID
                                    where p.SmallClassificationID=1";
                var Results = db.Database.SqlQuery<ProductListViewModels>(sqlQuery).ToList();
                return View(Results);
            }
        }
    }
}