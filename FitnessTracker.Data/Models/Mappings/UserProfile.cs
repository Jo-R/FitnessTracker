using AutoMapper;
using FitnessTracker.Data.Models.Requests.Users;
using FitnessTracker.Data.Models.Responses.Users;

namespace FitnessTracker.Data.Models.Mappings
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
