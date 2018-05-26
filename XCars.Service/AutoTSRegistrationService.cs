﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using XCars.Data.Infrastructure;
using XCars.Data.Repository.Interfaces;
using XCars.Model;
using XCars.Service.Interfaces;

namespace XCars.Service
{
    public class AutoTSRegistrationService : BaseService<AutoTSRegistration>, IAutoTSRegistrationService
    {
        public AutoTSRegistrationService(IAutoTSRegistrationRepository autoTSRegistrationRepository,
                           IUnitOfWork unitOfWork)
            : base(autoTSRegistrationRepository, unitOfWork)
        {
        }

        public List<SelectListItem> GetAllAsSelectList(int selected = 0)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (item.ID == selected) ? true : false
            }).ToList();
        }

        public List<SelectListItem> GetAllAsSelectListMultiple(int[] selected)
        {
            return GetAll().Select(item => new SelectListItem()
            {
                Value = item.ID.ToString(),
                Text = item.Name,
                Selected = (selected != null && selected.Contains(item.ID)) ? true : false,
            }).ToList();
        }
    }
}