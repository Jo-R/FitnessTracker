using FitnessTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FitnessTracker.Data
{
    public static class DbInitialiser
    {
        public static void Initialize(FitnessTrackerContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Activities.Any())
            {
                return;   // DB has been seeded
            }
            Guid userId = Guid.NewGuid();
            var users = new User[]
            {
            new User{Id=userId, FirstName="jo", LastName="ba", DateOfBirth=DateTime.Parse("1976-09-01")}

            };
            foreach (User u in users)
            {
                context.Users.Add(u);
            }
            context.SaveChanges();

            var activities = new Activity[]
            {
            new Activity{UserId=userId,Type=ActivityType.Running,Comments="Amazing",Date=DateTime.Parse("2005-09-01"),Id=Guid.NewGuid()},
            new Activity{UserId=userId,Type=ActivityType.Running,Comments="Amazing",Date=DateTime.Parse("2005-09-01"),Id=Guid.NewGuid()},
            new Activity{UserId=userId,Type=ActivityType.Running,Comments="Amazing",Date=DateTime.Parse("2005-09-01"),Id=Guid.NewGuid()},
            new Activity{UserId=userId,Type=ActivityType.Running,Comments="Amazing",Date=DateTime.Parse("2005-09-01"),Id=Guid.NewGuid()},
            new Activity{UserId=userId,Type=ActivityType.Running,Comments="Amazing",Date=DateTime.Parse("2005-09-01"),Id=Guid.NewGuid()},
            };
            foreach (Activity a in activities)
            {
                context.Activities.Add(a);
            }
            context.SaveChanges();

            
      

        }
    }
}
