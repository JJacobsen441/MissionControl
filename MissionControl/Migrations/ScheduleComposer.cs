using MissionControl.Models;
using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web.Scheduling;

namespace MissionControl.Migrations
{
    public class ScheduleComposer : ComponentComposer<ScheduleComponent>
    {
    }

    public class ScheduleComponent : IComponent
    {
        private IProfilingLogger _logger;
        private IRuntimeState _runtime;
        private IContentService _contentService;
        private BackgroundTaskRunner<IBackgroundTask> _scheduleRunner;

        public ScheduleComponent(IProfilingLogger logger, IRuntimeState runtime, IContentService contentService)
        {
            _logger = logger;
            _runtime = runtime;
            _contentService = contentService;
            _scheduleRunner = new BackgroundTaskRunner<IBackgroundTask>("Schedule", _logger);
        }

        public void Initialize()
        {
            int delayBeforeWeStart = 10000; // 60000ms = 1min
            int howOftenWeRepeat = 300000; //300000ms = 5mins

            var task = new Schedule(_scheduleRunner, delayBeforeWeStart, howOftenWeRepeat, _runtime, _logger, _contentService);

            //As soon as we add our task to the runner it will start to run (after its delay period)
            _scheduleRunner.TryAdd(task);
        }

        public void Terminate()
        {
        }
    }

    // Now we get to define the recurring task
    public class Schedule : RecurringTaskBase
    {
        private IRuntimeState _runtime;
        private IProfilingLogger _logger;
        private IContentService _contentService;

        public Schedule(IBackgroundTaskRunner<RecurringTaskBase> runner, int delayBeforeWeStart, int howOftenWeRepeat, IRuntimeState runtime, IProfilingLogger logger, IContentService contentService)
            : base(runner, delayBeforeWeStart, howOftenWeRepeat)
        {
            _runtime = runtime;
            _logger = logger;
            _contentService = contentService;
        }

        public override bool PerformRun()
        {
            try
            {

                //var numberOfThingsInBin = _contentService.CountChildren(Constants.System.RecycleBinContent);

                //_logger.Info<Schedule>("Go clean your room - {ServerRole}", _runtime.ServerRole);
                //_logger.Info<Schedule>("You have {NumberOfThingsInTheBin}", numberOfThingsInBin);

                //if (_contentService.RecycleBinSmells())
                //{
                // Take out the trash
                //    using (_logger.TraceDuration<Schedule>("Mum, I am emptying out the bin", "Its all clean now!"))
                //    {
                //        _contentService.EmptyRecycleBin(userId: -1);
                //    }
                //}

                List<FacilityDTO> locations = GeneralHelper.Locations();

                long unix = DateTimeOffset.Now.ToUnixTimeSeconds();
                List<ISS> iss = RestHelper.ISSGET(unix);
                
                double lat = iss[0].latitude;
                double lng = iss[0].longitude;
                long time = iss[0].timestamp;

                var coord = new GeoCoordinate(lat, lng);
                var nearest = locations.Select(x => new {
                    loc = x.Location,
                    lat = x.Latitude,
                    lng = x.Longitude,
                    time = time,
                    dist = new GeoCoordinate(x.Latitude, x.Longitude).GetDistanceTo(coord)
                });
                var near = nearest.OrderBy(x => x.dist).FirstOrDefault();

                //dont know if this is nessesary
                if (near.IsNull())
                    return true;

                FacilityDTO res = new FacilityDTO() { Location = near.loc, Latitude = near.lat, Longitude = near.lng, Timestamp = near.time, Distance = (int)near.dist };
                
                DAL dal = new DAL();
                dal.CreateClosestFacilityByTimeStamp(res);
                
                return true;
            }
            catch (Exception _e)
            {
                // If we want to keep repeating - we need to return true
                // But if we run into a problem/error & want to stop repeating - return false
                return true;
            }
        }

        public override bool IsAsync => false;
    }
}