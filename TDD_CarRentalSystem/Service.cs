using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_CarRentalSystem
{
    class Service
    {
        // Constant to indicate that the vehicle needs to be serviced every 10,000km
        public static int SERVICE_KILOMETER_LIMIT = 10000;

        private int lastServiceOdometerKm = 0;
        private int serviceCount = 0;
        private bool requiredService = false;

        //PK id - field
        //FK vehicle id

        public Service(bool contentLoaded, int lastServiceOdometerKm, int serviceCount, bool requiredService)
        {
            //_contentLoaded = contentLoaded;
            this.lastServiceOdometerKm = lastServiceOdometerKm;
            this.serviceCount = serviceCount;
            this.requiredService = requiredService;
        }


        // TODO add lastServiceDate

        // get and set methods

        //methods- service odo reading, service at, created at, updated at
        public int ServiceCount
        {
            get { return serviceCount; }
        }

        public int LastServiceOdometerKm
        {
            get { return lastServiceOdometerKm; }
        }

        public bool RequiredService
        {
            get { return requiredService; }
        }

        // return the last service
        public int getLastServiceOdometerKm()
        {
            return this.lastServiceOdometerKm;
        }

        /**
         * The function recordService expects the total distance traveled by the car, 
         * saves it and increase serviceCount.
         * @param distance 
         */
        public void recordService(int distance)
        {
            this.lastServiceOdometerKm = distance;
            this.serviceCount++;
        }

        // return how many services the car has had
        public int getServiceCount()
        {
            return this.serviceCount;
        }

        /**
         * Calculates the total services by dividing kilometers by
         * {@link #SERVICE_KILOMETER_LIMIT} and floors the value. 
         * 
         * @return the number of services needed per SERVICE_KILOMETER_LIMIT
         */
        public int getTotalScheduledServices()
        {
            double totalScheduledService = lastServiceOdometerKm / SERVICE_KILOMETER_LIMIT;
            return (int)Math.Floor(totalScheduledService);
        }
    }
}
