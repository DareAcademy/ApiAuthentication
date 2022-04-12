using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiSession1912.data;

namespace WebApiSession1912.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        
        [Authorize]
        [HttpGet]
        [Route("Index")]
        public List<string> Index(int id)
        {
            List<string> x = new List<string>();
            x.Add("hi");
            x.Add("hi1");
            return x;
        }

        [HttpGet]
        [Route("load/{abc}")]
        public string Index33(int abc)
        {
            return "hello world";
        }

        [HttpPost]
        [Route("xyz")]
        public void Index1(patient p)
        {

        }

        [HttpPost]
        [Route("abc")]
        public void Index2()
        {

        }

    }
}
