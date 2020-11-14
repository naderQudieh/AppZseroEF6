using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppZseroEF6.Model;
using AppZseroEF6.Service;
using AppZseroEF6.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppZseroEF6.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        
        private readonly IUserAddressService _userAddressService;

        public UserAddressController( IUserAddressService userAddressService)
        {
            
            _userAddressService = userAddressService;
        }

        
        [HttpPost]
        public async Task<ActionResult> createAddress(UserAddressCM model)
        {
            
            var address = model.Adapt<UserAddress>();
           
            _userAddressService.CreateUserAddress(address);
            _userAddressService.SaveChanges();
            return Ok();
        }
    }
}