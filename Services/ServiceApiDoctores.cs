using DoctoresClientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DoctoresClientAPI.Services
{
    public class ServiceApiDoctores
    {
        //NECESITAMOS LA URL DEL SERVICIO
        private String url;
        //NECESITAMOS UN OBJETO PARA INDICAR AL SERVICIO QUE 
        //CONSUMIREMOS JSON
        MediaTypeWithQualityHeaderValue header;

        public ServiceApiDoctores()
        {
            this.url = "https://apidoctoresagz.azurewebsites.net";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public ServiceApiDoctores(String url)
        {
            this.url = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //TODOS LOS METODOS SON ASINCRONOS
        public async Task<List<Doctor>> GetDoctoresAsync()
        {
            //NECESITAMOS UN OBJETO HttpClient
            using (HttpClient client = new HttpClient())
            {
                String request = "api/doctores";
                //EN EL CLIENTE, DEBEMOS INDICAR LA DIRECCION BASE
                //DE ACCESO AL SERVICIO
                client.BaseAddress = new Uri(this.url);
                //LIMPIAMOS LAS CABECERAS PARA CADA PETICION
                client.DefaultRequestHeaders.Clear();
                //AÑADIMOS EL FORMATO DE PETICION QUE VAMOS A REALIZAR (header)
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //REALIZAMOS LA PETICION UTILIZANDO EL METODO Get
                //DICHA PETICION NOS OFRECERA UNA RESPUESTA EN UN OBJETO
                //HttpResponseMessage
                HttpResponseMessage response =
await client.GetAsync(request);
                //PREGUNTAMOS SI LA RESPUESTA ES CORRECTA PARA DEVOLVER LOS DATOS
                if (response.IsSuccessStatusCode)
                {
                    //EN LA PROPIEDAD Content DE RESPONSE VIENEN LOS RESULTADOS
                    //DE LA PETICION Get (JSON)
                    //TENEMOS UN METODO ASINCRONO ReadAsAsync QUE TIENE POR DETRAS
                    //A NEWTONSOFT JSON PARA DESERIALIZAR LOS OBJETOS.
                    //TAMBIEN PODRIAMOS HACERLO MANUALMENTE CON NEWTONSOFT
                    List<Doctor> doctores =
await response.Content.ReadAsAsync<List<Doctor>>();
                    return doctores;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<String>> GetEspecialidadesAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                String request = "api/doctores/especialidades";
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    List<String> especialidades =
                    await response.Content.ReadAsAsync<List<String>>();
                    return especialidades;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}

