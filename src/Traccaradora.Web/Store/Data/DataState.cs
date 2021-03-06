#nullable enable
using Blazored.LocalStorage;
using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Traccaradora.Web.Clients;
using System.Net;

namespace Traccaradora.Web.Store.Data
{
    public record DataState
    {
        public bool IsLoading { get; init; }
        public bool IsInitialized { get; init; }
        public List<TraccarDevice>? Devices { get; init; }
    }

    public class FetchDataAction
    {
    }

    public record FetchDataFinishAction
    {
        public List<TraccarDevice>? Devices { get; init; }
        public Dictionary<int, Position>? LastPositions { get; init; }
    }

    public class DataFeature : Feature<DataState>
    {
        public override string GetName()
        {
            return "Data";
        }

        protected override DataState GetInitialState()
        {
            return new DataState() { IsLoading = false, Devices = null, IsInitialized = false };
        }
    }

    public static class Reducers
    {
        [ReducerMethod]
        public static DataState ReduceLoadAction(DataState state, FetchDataAction action) =>
            new DataState() { Devices = null, IsLoading = true, IsInitialized = false };

        [ReducerMethod]
        public static DataState ReduceLoadFinishAction(DataState state, FetchDataFinishAction action) =>
            new DataState() { IsLoading = false, Devices = action.Devices, IsInitialized = true };
    }

    public class DataEffects
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IServiceProvider _serviceProvider;

        public DataEffects(Blazored.LocalStorage.ILocalStorageService localStorage, IServiceProvider serviceProvider)
        {
            _localStorage = localStorage;
            this._serviceProvider = serviceProvider;
        }

        [EffectMethod]
        public async Task HandleFetchData(FetchDataAction action, IDispatcher dispatcher)
        {
            var client = _serviceProvider.GetService<Client>();
            if (client == null)
            {
                return;
            }

            var devicesResult = await client.DevicesGetAsync(null, null, null, null);
            // get last position for every device
            List<TraccarDevice> devices = new();
            foreach (var device in devicesResult.Result.ToList())
            {
                if (!device.Id.HasValue)
                    continue;
                var position = await client.PositionsAsync(device.Id, null, null, device.PositionId);
                var pos = position.Result.FirstOrDefault();
                if (position != null && position.StatusCode == (int) HttpStatusCode.OK && pos != null)
                {
                    TraccarDevice traccarDevice = new TraccarDevice()
                    {
                        Id = device.Id.Value,
                        Name = device.Name,
                        Latitude = pos.Latitude,
                        Longitude = pos.Longitude,
                        Altitude = pos.Altitude
                    };
                    devices.Add(traccarDevice);
                }
            }

            Console.WriteLine("HandleFetchData");
            dispatcher.Dispatch(new FetchDataFinishAction() { Devices = devices }) ;
        }
    }

}
