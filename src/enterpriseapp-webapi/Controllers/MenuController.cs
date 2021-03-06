﻿#region Copyright ©2016, Click2Cloud Inc. - All Rights Reserved
/* ------------------------------------------------------------------- *
*                            Click2Cloud Inc.                          *
*                  Copyright ©2016 - All Rights reserved               *
*                                                                      *
*                                                                      *
*  Copyright © 2016 by Click2Cloud Inc. | www.click2cloud.net          *
*  All rights reserved. No part of this publication may be reproduced, *
*  stored in a retrieval system or transmitted, in any form or by any  *
*  means, photocopying, recording or otherwise, without prior written  *
*  consent of Click2cloud Inc.                                         *
*                                                                      *
*                                                                      *
* -------------------------------------------------------------------  */
#endregion Copyright ©2016, Click2Cloud Inc. - All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Click2Cloud.EnterpriseApp.WebAPI.Models;
using System.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Click2Cloud.EnterpriseApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        // GET: api/values
        [HttpGet]
        public List<AdUserDetail> GetMenus()
        {

            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = Files.Config.ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCmd.CommandText = "SpGetMenuItems";
            sqlCmd.Connection = myConnection;
            myConnection.Open();

            reader = sqlCmd.ExecuteReader();
            List<AdUserDetail> menus = new List<AdUserDetail>();
            while (reader.Read())
            {
                menus.Add(new AdUserDetail
                {
                    MenuId = (string.IsNullOrEmpty(reader["MenuId"].ToString()) ? 0 : Convert.ToInt32(reader["MenuId"])),
                    MenuName = (string.IsNullOrEmpty(reader["MenuName"].ToString())) ? "" : reader["MenuName"].ToString(),
                    Parent = (string.IsNullOrEmpty(reader["Parent"].ToString()) ? 0 : Convert.ToInt32(reader["Parent"])),
                    Child = (string.IsNullOrEmpty(reader["Child"].ToString()) ? 0 : Convert.ToInt32(reader["Child"])),
                    Url = (string.IsNullOrEmpty(reader["Url"].ToString())) ? "" : reader["Url"].ToString()
                });
            }
            myConnection.Close();
            return menus;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
