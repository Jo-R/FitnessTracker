using AutoMapper;
using FitnessTracker.Data.Models.Requests.RunActivities;
using FitnessTracker.Data.Models.Responses.RunActivities;


namespace FitnessTracker.Data.Models.Mappings
{
    public class RunActivityProfile: Profile
    {
        public RunActivityProfile()
        {
            CreateMap<AddRunActivityRequest, RunActivity>();
            CreateMap<RunActivity, RunActivityResponse>();
        }
    }
}
