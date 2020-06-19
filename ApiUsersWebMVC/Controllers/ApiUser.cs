﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureCloudPlatform.Client.V2.Api;
using PureCloudPlatform.Client.V2.Client;
using PureCloudPlatform.Client.V2.Extensions;
using PureCloudPlatform.Client.V2.Model;
using ApiUsersWebMVC.Models;


namespace ApiUsersWebMVC.Controllers
{
    public class ApiUser
    {

        public List<UsuariosActivos> cosumirUsuario()
        {
            var accessTokenInfo = Configuration.Default.ApiClient.PostToken(PureCloudConstants.CLIENT_ID,
                                PureCloudConstants.CLIENT_SECRET);
            Console.WriteLine("Access token=" + accessTokenInfo.AccessToken);
            var accessToken = accessTokenInfo.AccessToken;
            Configuration.Default.AccessToken = accessTokenInfo.AccessToken; 

            //SACAR USUARIOS

            UsersApi usersApi = new UsersApi();
            UserEntityListing userList = null;
            List<UsuariosActivos> listaUsuarios = new List<UsuariosActivos>();
            List<UsuariosActivos> listaUsuariosNoDisponible = new List<UsuariosActivos>();
            List<UsuariosActivos> listaTotal = new List<UsuariosActivos>();


            List<String> expand = new List<String>();
            expand.Add("presence");
            expand.Add("routingStatus");
          
            int page = 0; 
            //listExpands
            do
            {
                page++;
                userList = usersApi.GetUsers(30, page, null, null, null, null, null);
                foreach (var user in userList.Entities)
                {
                    var usuarioActivo = usersApi.GetUser(user.Id, expand, "active");
                    string urlImagen = "";
                    if (usuarioActivo.Images!=null)
                    {
                        urlImagen = usuarioActivo.Images.ElementAt(4).ImageUri;
                        
                    }
                    else {
                        urlImagen = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";
                        
                    }
                    if (usuarioActivo.Presence.PresenceDefinition.SystemPresence == "On Queue" && (usuarioActivo.RoutingStatus.Status.ToString() == "Idle" || usuarioActivo.RoutingStatus.Status.ToString() == "Interacting"))
                    {
                        listaUsuarios.Add(new UsuariosActivos
                        {
                            Id = usuarioActivo.Id,
                            Nombre = usuarioActivo.Name,
                            Correo = usuarioActivo.Email,
                            Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                            Especialidad = usuarioActivo.Department,
                            Imagen = urlImagen,
                            EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                        });
                    }
                    else
                    {
                        listaUsuariosNoDisponible.Add(new UsuariosActivos
                        {
                            Id = usuarioActivo.Id,
                            Nombre = usuarioActivo.Name,
                            Correo = usuarioActivo.Email,
                            Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                            Especialidad = usuarioActivo.Department,
                            Imagen = urlImagen,
                            EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                        });
                    }

                }

            } while (userList.Entities.Count() != 0);

            listaTotal = listaUsuarios.Union(listaUsuariosNoDisponible).ToList();
            return listaTotal;

        }

        public List<UsuariosActivos> filtrarUsuario(string filtro)
        {
            var accessTokenInfo = Configuration.Default.ApiClient.PostToken(PureCloudConstants.CLIENT_ID,
                                PureCloudConstants.CLIENT_SECRET);
            Console.WriteLine("Access token=" + accessTokenInfo.AccessToken);
            var accessToken = accessTokenInfo.AccessToken;
            Configuration.Default.AccessToken = accessTokenInfo.AccessToken;

            //SACAR USUARIOS

            UsersApi usersApi = new UsersApi();
            UserEntityListing userList = null;
            List<UsuariosActivos> listaUsuarios = new List<UsuariosActivos>();
            List<UsuariosActivos> listaUsuariosNoDisponible = new List<UsuariosActivos>();
            List<UsuariosActivos> listaTotal = new List<UsuariosActivos>();


            List<String> expand = new List<String>();
            expand.Add("presence");
            expand.Add("routingStatus");
            int page = 0;
            switch (filtro)
            {
                case "todos":
                    page = 0;
                    //listExpands
                    do
                    {
                        page++;
                        userList = usersApi.GetUsers(30, page, null, null, null, null, null);
                        foreach (var user in userList.Entities)
                        {
                            var usuarioActivo = usersApi.GetUser(user.Id, expand, "active");
                            string urlImagen = "";
                            if (usuarioActivo.Images != null)
                            {
                                urlImagen = usuarioActivo.Images.ElementAt(4).ImageUri;

                            }
                            else
                            {
                                urlImagen = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";

                            }
                            if (usuarioActivo.Presence.PresenceDefinition.SystemPresence == "On Queue" && (usuarioActivo.RoutingStatus.Status.ToString() == "Idle" || usuarioActivo.RoutingStatus.Status.ToString() == "Interacting"))
                            {
                                listaUsuarios.Add(new UsuariosActivos
                                {
                                    Id = usuarioActivo.Id,
                                    Nombre = usuarioActivo.Name,
                                    Correo = usuarioActivo.Email,
                                    Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                                    Especialidad = usuarioActivo.Department,
                                    Imagen = urlImagen,
                                    EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                                });
                            }
                            else
                            {
                                listaUsuariosNoDisponible.Add(new UsuariosActivos
                                {
                                    Id = usuarioActivo.Id,
                                    Nombre = usuarioActivo.Name,
                                    Correo = usuarioActivo.Email,
                                    Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                                    Especialidad = usuarioActivo.Department,
                                    Imagen = urlImagen,
                                    EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                                });
                            }

                        }

                    } while (userList.Entities.Count() != 0);

                    listaTotal = listaUsuarios.Union(listaUsuariosNoDisponible).ToList();
                    break;
                case "userDispo":
                    page = 0;
                    //listExpands
                    do
                    {
                        page++;
                        userList = usersApi.GetUsers(30, page, null, null, null, null, null);
                        foreach (var user in userList.Entities)
                        {
                            var usuarioActivo = usersApi.GetUser(user.Id, expand, "active");
                            string urlImagen = "";
                            if (usuarioActivo.Images != null)
                            {
                                urlImagen = usuarioActivo.Images.ElementAt(4).ImageUri;

                            }
                            else
                            {
                                urlImagen = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";

                            }
                            if (usuarioActivo.Presence.PresenceDefinition.SystemPresence == "On Queue" && (usuarioActivo.RoutingStatus.Status.ToString() == "Idle" || usuarioActivo.RoutingStatus.Status.ToString() == "Interacting"))
                            {
                                listaUsuarios.Add(new UsuariosActivos
                                {
                                    Id = usuarioActivo.Id,
                                    Nombre = usuarioActivo.Name,
                                    Correo = usuarioActivo.Email,
                                    Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                                    Especialidad = usuarioActivo.Department,
                                    Imagen = urlImagen,
                                    EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                                });
                            }
                           

                        }

                    } while (userList.Entities.Count() != 0);

                    listaTotal = listaUsuarios.ToList();
                    break;
                case "userNoDispo":
                    page = 0;
                    //listExpands
                    do
                    {
                        page++;
                        userList = usersApi.GetUsers(30, page, null, null, null, null, null);
                        foreach (var user in userList.Entities)
                        {
                            var usuarioActivo = usersApi.GetUser(user.Id, expand, "active");
                            string urlImagen = "";
                            if (usuarioActivo.Images != null)
                            {
                                urlImagen = usuarioActivo.Images.ElementAt(4).ImageUri;

                            }
                            else
                            {
                                urlImagen = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_960_720.png";

                            }
                            if (usuarioActivo.Presence.PresenceDefinition.SystemPresence != "On Queue" && (usuarioActivo.RoutingStatus.Status.ToString() != "Idle" || usuarioActivo.RoutingStatus.Status.ToString() != "Interacting"))
                            {
                                listaUsuariosNoDisponible.Add(new UsuariosActivos
                                {
                                    Id = usuarioActivo.Id,
                                    Nombre = usuarioActivo.Name,
                                    Correo = usuarioActivo.Email,
                                    Estado = usuarioActivo.RoutingStatus.Status.ToString(),
                                    Especialidad = usuarioActivo.Department,
                                    Imagen = urlImagen,
                                    EstadoActual = usuarioActivo.Presence.PresenceDefinition.SystemPresence
                                });
                            }
                            

                        }

                    } while (userList.Entities.Count() != 0);

                    listaTotal = listaUsuariosNoDisponible.ToList();
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
           
            return listaTotal;

        }
    }
}
