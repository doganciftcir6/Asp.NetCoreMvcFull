using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Business.Interfaces;
using Udemy.AdvertisementApp.Common.Enums;
using Udemy.AdvertisementApp.Dtos;
using Udemy.AdvertisementApp.UI.Extensions;
using Udemy.AdvertisementApp.UI.Models;

namespace Udemy.AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IAdvertisementAppUserService _advertisementAppUserService;

        public AdvertisementController(IAppUserService appUserService, IAdvertisementAppUserService advertisementAppUserService)
        {
            _appUserService = appUserService;
            _advertisementAppUserService = advertisementAppUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        // asp-route-advertisementId ile bu parametreye advertisementId bilgisini düşürmek.
        public async Task<IActionResult> Send(int advertisementId)
        {
            //userId bilgisini çekmek.
            var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
            //user bilgisini çekmek.
            var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(userId);
            //gender bilgisini çekmek.
            ViewBag.GenderId = userResponse.Data.GenderId;

            //enum
            var items = Enum.GetValues(typeof(MilitaryStatusType));
            var list = new List<MilitaryStatusListDto>();

            foreach (int item in items)
            {
                list.Add(new MilitaryStatusListDto
                {
                    Id = item,
                    Definiton = Enum.GetName(typeof(MilitaryStatusType),item),
                });
            }
            //valuem ve gösteceğim şey Definiton
            ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definiton");

            return View(new AdvertisementAppUserCreateModel
            {
                AdvertisementId = advertisementId,
                AppUserId = userId,
            });
        }
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            //başvuru yapma durumu
            //yapılan başvuruları gösterme (state = başvuru) reddilen onaylanan şeklinde
            AdvertisementAppUserCreateDto dto = new AdvertisementAppUserCreateDto();

            if (model.CvFile != null)
            {
                //upload işlemi yapıcaz gönderlien cv dosyası boş değil
                //dosyanın benzersiz adı
                var fileName = Guid.NewGuid().ToString();
                //dosyanın uzantısı
                var extName = Path.GetExtension(model.CvFile.FileName);
                //bu iki bilgiyi birleştiricez
                string path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","cvFiles", fileName + extName);
                var stream = new FileStream(path, FileMode.Create);
                await model.CvFile.CopyToAsync(stream);
                dto.CvPath = path;
            }
            dto.AdvertisementAppUserStatusId = model.AdvertisementAppUserStatusId;
            dto.AdvertisementId = model.AdvertisementId;
            dto.AppUserId = model.AppUserId;
            dto.EndDate = model.EndDate;
            dto.MilitaryStatusId = model.MilitaryStatusId;
            dto.WorkExperience = model.WorkExperience;

            var response = await _advertisementAppUserService.CreateAsync(dto);
            if(response.ResponseType == Common.ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                //userId bilgisini çekmek.
                var userId = int.Parse((User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)).Value);
                //user bilgisini çekmek.
                var userResponse = await _appUserService.GetByIdAsync<AppUserListDto>(userId);
                //gender bilgisini çekmek.
                ViewBag.GenderId = userResponse.Data.GenderId;
                //enum
                var items = Enum.GetValues(typeof(MilitaryStatusType));
                var list = new List<MilitaryStatusListDto>();

                foreach (int item in items)
                {
                    list.Add(new MilitaryStatusListDto
                    {
                        Id = item,
                        Definiton = Enum.GetName(typeof(MilitaryStatusType), item),
                    });
                }
                //valuem ve gösteceğim şey Definiton
                ViewBag.MilitaryStatus = new SelectList(list, "Id", "Definiton");
                return View(model);
            }
            else
            {
                return RedirectToAction("HumanResource", "Home");
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            //sadece ilana başvuran kişileri sayfamda göstereceğim.
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.Basvurdu);
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetStatus(int advertisementAppUserId, AdvertisementAppUserStatusType type)
        {
            await _advertisementAppUserService.SetStatusAsync(advertisementAppUserId, type);
            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApprovedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.MulakataCagirildi);
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectedList()
        {
            var list = await _advertisementAppUserService.GetList(AdvertisementAppUserStatusType.OlumsuzSonuclandi);
            return View(list);
        }
    }
}
