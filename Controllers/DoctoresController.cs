using DoctoresClientAPI.Models;
using DoctoresClientAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctoresClientAPI.Controllers
{
    public class DoctoresController : Controller
    {
        ServiceApiDoctores service;

        public DoctoresController(ServiceApiDoctores service)
        {
            this.service = service;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DoctoresClienteAjax()
        {
            return View();
        }

        public async Task<IActionResult> DoctoresServidor(string? especialidad)
        {
            if (especialidad == null)
            {
                List<Doctor> doctores = await this.service.GetDoctoresAsync();
                List<String> especialidades = await this.service.GetEspecialidadesAsync();

                ViewData["ESPECIALIDADES"] = especialidades;
                return View(doctores);

            }
            else
            {
                List<Doctor> doctores = await this.service.GetDoctoresAsync();
                List<String> especialidades = await this.service.GetEspecialidadesAsync();
                var doctoresEspecialidda = doctores.Where(C => C.Especialidad == especialidad).ToList();
                ViewData["ESPECIALIDADES"] = especialidades;
                return View(doctoresEspecialidda);

            }


        }

    }
}
