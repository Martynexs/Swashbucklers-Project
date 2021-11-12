﻿using DataLibrary.Exceptions;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary
{
    public class EncounterProcessor
    {
        private const string _apiAdress = "https://localhost:44309";

        private static readonly Lazy<EncounterProcessor> _encounterProcessor =
            new Lazy<EncounterProcessor>(() => new EncounterProcessor());
        public static EncounterProcessor Instanse { get => _encounterProcessor.Value; }
        private EncounterProcessor()
        {
        }

        private readonly ApiHelper _apiHelper = new ApiHelper();

        public event Action UnauthorisedHttpRequestEvent;

        public void EnableJWTAuthetication(string jwt)
        {
            _apiHelper.SetJWT(jwt);
        }

        public async Task<Route> GetRoute(long id)
        {
            var url = $"{ _apiAdress }/api/route/{ id }";

            var route = await _apiHelper.HttpGet<Route>(url);
            return route;
        }

        public async Task<List<Route>> GetAllRoutes()
        {
            var url = $"{ _apiAdress }/api/route";

            var routes = await _apiHelper.HttpGet<List<Route>>(url);
            return routes;
        }

        public async Task<List<Waypoint>> GetWaypoints(long routeId)
        {
            var url = $"{ _apiAdress }/api/route/{ routeId }/Waypoints";

            var waypoints = await _apiHelper.HttpGet<List<Waypoint>>(url);
            return waypoints;
        }

        public async Task<Route> CreateRoute(Route route)
        {
            var url = $"{ _apiAdress }/api/route";

            var createdRoute = await _apiHelper.HttpPost<Route>(url, route);
            return createdRoute;
        }

        public async Task UpdateRoute(long id, Route route)
        {
            var url = $"{ _apiAdress }/api/route/{ id }";

            await _apiHelper.HttpPut<Route>(url, route);
        }

        public async Task DeleteRoute(long id)
        {
            var url = $"{ _apiAdress }/api/route/{ id }";

            await _apiHelper.HttpDelete(url);
        }

        public async Task<Waypoint> CreateWaypoint(Waypoint waypoint)
        {
            var url = $"{ _apiAdress }/api/waypoints";

            var createdWaypoint = await _apiHelper.HttpPost<Waypoint>(url, waypoint);
            return createdWaypoint;
        }

        public async Task UpdateWaypoint(long id, Waypoint waypoint)
        {
            var url = $"{ _apiAdress }/api/waypoints/{ id }";

            await _apiHelper.HttpPut<Waypoint>(url, waypoint);
        }

        public async Task DeleteWaypoint(long id)
        {
            var url = $"{ _apiAdress }/api/waypoints/{ id }";

            await _apiHelper.HttpDelete(url);
        }

        public async Task SubmitRating(Rating rating)
        {
            var url = $"{ _apiAdress }/api/ratings/{ rating.RouteId }/{ rating.Username }";

            await _apiHelper.HttpPut<Rating>(url, rating);
        }

        public async Task<Rating> GetRating(long routeId, string username)
        {
            var url = $"{ _apiAdress }/api/ratings/{ routeId }/{ username }";

            var rating = await _apiHelper.HttpGet<Rating>(url);
            return rating;
        }

        public async Task<User> GetUser(string username)
        {
            try
            {
                var url = $"{ _apiAdress }/User/{ username }";
                var user = await _apiHelper.HttpGet<User>(url);

                return user;
            }
            catch(UnauthorizedHttpRequestException)
            {
                UnauthorisedHttpRequestEvent.Invoke();
                return null;
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<User> RegisterUser(User user)
        {
            var url = $"{ _apiAdress }/Users";

            var createdUser = await _apiHelper.HttpPost<User>(url, user);
            return createdUser;
        }

        public async Task<string> GetAuthenticationToken(string username, string password)
        {
            var url = $"{ _apiAdress }/token?username={ username }&password={password}";

            var logininfo = await _apiHelper.HttpPost<LoginInfo>(url, new LoginInfo { Username = username, Password = password });
            var token = logininfo.Authentication_token;
            return token;
        }

    }
}