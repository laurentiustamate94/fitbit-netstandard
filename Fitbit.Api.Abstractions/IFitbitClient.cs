using Fitbit.Api.Abstractions.Endpoints;
using System;

namespace Fitbit.Api.Abstractions
{
    public interface IFitbitClient
    {
        IAuthentication Authentication { get; }

        //IActivity Activity { get; }

        //IBodyAndWeight BodyAndWeight { get; }

        //IDevices Devices { get; }

        //IFoodLogging FoodLogging { get; }

        //IFriends Friends { get; }

        IHeartRate HeartRate { get; }

        //ISleep Sleep { get; }

        //ISubscriptions Subscriptions { get; }

        //IUser User { get; }
    }
}