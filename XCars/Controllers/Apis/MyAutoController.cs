using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Controllers.Apis
{
    [RoutePrefix("myauto")]
    public class MyAutoController : BaseApiController
    {
        public IAutoService AutoService { get; set; }
        public IUserService UserService { get; set; }
        public IAutoFavoriteService AutoFavoriteService { get; set; }

        public MyAutoController(IAutoService autoService, IUserService userService, IAutoFavoriteService autoFavoriteService)
        {
            AutoService = autoService;
            UserService = userService;
            AutoFavoriteService = autoFavoriteService;
        }

        [HttpPost]
        [Route("AddToFavorites")]
        //[ResponseType(typeof(Model.City))]
        public IHttpActionResult AddToFavorites(int id)
        {
            User user = UserService.GetUserByEmail(User.Identity.Name);

            Auto auto = AutoService.GetByID(id);
            if (auto == null)
                return NotFound();

            try
            {
                int result = 1;
                AutoFavorite fav = user.AutoFavorites.FirstOrDefault(f => f.AutoID == id);
                if (fav == null)
                {
                    user.AutoFavorites.Add(new AutoFavorite() { AutoID = id });
                    UserService.EditUser(user);
                }
                else
                {
                    AutoFavoriteService.Delete(fav);
                    result = -1;
                }

                AutoService.Edit(auto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}