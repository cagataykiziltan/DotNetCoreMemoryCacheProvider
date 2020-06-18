using CacheProvider.Services.Services.Abstractions;
using CacheProvider.Manager.Models;
using CacheProvider.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CacheProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingController : ControllerBase
    {
        private readonly ICachingService _cachingManager;
        
        public CachingController(ICachingService cachingManager)
        {
            _cachingManager = cachingManager;
        }
        
        [HttpPost("SetCache")]
        public ResultModel<List<Student>> SetCache()
        {
            var studentApi = new StudentApi();
            var students = studentApi.GetStudents();
            var cacheKey = "studentCache";

            var result = _cachingManager.SetCache(students, cacheKey, DateTime.Now.AddMinutes(2));

            return result;
        }

        [HttpPost("GetFromCache")]
        public ResultModel<List<Student>> GetFromCache()
        {         
            var cacheKey = "studentCache";

            var result = _cachingManager.GetFromCache<List<Student>>(cacheKey);

            return result;
        }

        [HttpPost("RemoveCache")]
        public string RemoveCache()
        {
            var cacheKey = "studentCache";

            var result = _cachingManager.RemoveCache(cacheKey);

            return result;
        }


        [HttpPost("SetGetCache")]
        public List<Student> SetGetCache()
        {
            var studentApi = new StudentApi();
            var cacheKey = "studentCache";

            var result = _cachingManager.SetGetCache(cacheKey, DateTime.Now.AddMinutes(2), () =>
             {
                 var result = studentApi.GetStudents();
                 return result;
             });

            return result;
        }

    }


}
