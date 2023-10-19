using AutoMapper;
using IPStack.API.Entities;
using IPStack.API.Services;
using IPStackLibrary.Models;
using IPStackLibrary.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IPStackAPI.Controllers
{
    [Route("ipdetails")]
    [ApiController]
    public class IPDetailsController : ControllerBase
    {
        private readonly IPInfoProvider client;
        private readonly IMemoryCache cache;
        private readonly IIPDetailsRepository repository;
        private readonly IMapper mapper;

        public IPDetailsController(IPInfoProvider client, IMemoryCache cache, 
            IIPDetailsRepository repository, IMapper mapper)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/<IPDetailsController>
        [HttpGet]
        public async Task<IActionResult> Get(string ip)
        {
            IPDetailsEntity? result;

            //get from cache
            if (!cache.TryGetValue(ip, out result))
            {
                //get from db
                var dbresult = await repository.GetIPDetailsAsync(ip);

                if (dbresult is null)
                {
                    //get from library
                    var apiresult = await client.GetDetails(ip);   
                    
                    if (apiresult is null)
                    {
                        return StatusCode(500);
                    }
                    else
                    {
                        //write to db
                        result = mapper.Map<IPDetailsEntity>(apiresult);
                        await repository.AddIPDetailsAsync(result);
                        cache.Set(ip, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
                    }                        
                }
                else
                {
                    result = dbresult;
                    cache.Set(ip, result, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60)));
                }
            }

            return Ok(result);
        }

    }
}
