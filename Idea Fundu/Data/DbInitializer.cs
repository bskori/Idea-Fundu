using Idea_Fundu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Idea_Fundu.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context =
                serviceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager =
                serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await context.Database.MigrateAsync();

            // =========================================================
            // STOP DUPLICATE DATA
            // =========================================================

            if (context.Ideas.Any())
            {
                return;
            }

            // =========================================================
            // USERS
            // =========================================================

            var founders = new List<ApplicationUser>
{
    new ApplicationUser
    {
        FullName = "Rahul Mehta",
        UserName = "rahul@gmail.com",
        Email = "rahul@gmail.com",
        RoleType = "Founder",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Priya Sharma",
        UserName = "priya@gmail.com",
        Email = "priya@gmail.com",
        RoleType = "Founder",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Aarav Kapoor",
        UserName = "aarav@gmail.com",
        Email = "aarav@gmail.com",
        RoleType = "Founder",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Sneha Verma",
        UserName = "sneha@gmail.com",
        Email = "sneha@gmail.com",
        RoleType = "Founder",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Rohan Patel",
        UserName = "rohan@gmail.com",
        Email = "rohan@gmail.com",
        RoleType = "Founder",
        EmailConfirmed = true
    }
};

            foreach (var founder in founders)
            {
                var existingUser =
                    await userManager.FindByEmailAsync(founder.Email);

                if (existingUser == null)
                {
                    await userManager.CreateAsync(
                        founder,
                        "Founder@123");
                }
            }



            var investors = new List<ApplicationUser>
{
    new ApplicationUser
    {
        FullName = "Aman Verma",
        UserName = "aman@gmail.com",
        Email = "aman@gmail.com",
        RoleType = "Investor",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Neha Joshi",
        UserName = "neha@gmail.com",
        Email = "neha@gmail.com",
        RoleType = "Investor",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Karan Malhotra",
        UserName = "karan@gmail.com",
        Email = "karan@gmail.com",
        RoleType = "Investor",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Simran Kaur",
        UserName = "simran@gmail.com",
        Email = "simran@gmail.com",
        RoleType = "Investor",
        EmailConfirmed = true
    },

    new ApplicationUser
    {
        FullName = "Vikram Singh",
        UserName = "vikram@gmail.com",
        Email = "vikram@gmail.com",
        RoleType = "Investor",
        EmailConfirmed = true
    }
};

            foreach (var investor in investors)
            {
                var existingUser =
                    await userManager.FindByEmailAsync(investor.Email);

                if (existingUser == null)
                {
                    await userManager.CreateAsync(
                        investor,
                        "Investor@123");
                }
            }

            // =========================================================
            // IDEAS
            // =========================================================

            if (!context.Ideas.Any())
            {
                var allFounders = userManager.Users
                    .Where(x => x.RoleType == "Founder")
                    .ToList();

                var ideas = new List<Idea>
{
    new Idea{
        Title="AI Resume Screening Platform",
        Description="AI-based hiring platform that automatically screens resumes and ranks candidates for HR teams.",
        Category="AI & Recruitment",
        RequiredFund=1200000,
        RiskLevel="Medium",
        Restrictions="Only SaaS investors",
        Status="Pending",
        ImageUrl="startup1.jpg",
        CreatedDate=DateTime.Now.AddDays(-50),
        UserId=allFounders[0].Id
    },

    new Idea{
        Title="Smart Farming Drone",
        Description="Drones for monitoring crop health, irrigation and fertilizer usage using AI technology.",
        Category="AgriTech",
        RequiredFund=2500000,
        RiskLevel="High",
        Restrictions="Long-term investors preferred",
        Status="Pending",
        ImageUrl="startup2.jpg",
        CreatedDate=DateTime.Now.AddDays(-45),
        UserId=allFounders[1].Id
    },

    new Idea{
        Title="Online Doctor Consultation App",
        Description="Healthcare mobile application for online consultations with doctors and digital prescriptions.",
        Category="HealthTech",
        RequiredFund=1800000,
        RiskLevel="Medium",
        Restrictions="Healthcare domain investors only",
        Status="Funded",
        ImageUrl="startup3.jpg",
        CreatedDate=DateTime.Now.AddDays(-40),
        UserId=allFounders[2].Id
    },

    new Idea{
        Title="Solar Powered EV Charging",
        Description="Green energy startup focused on solar-powered electric vehicle charging stations.",
        Category="Green Energy",
        RequiredFund=4500000,
        RiskLevel="High",
        Restrictions="Infrastructure investment required",
        Status="Pending",
        ImageUrl="startup4.jpg",
        CreatedDate=DateTime.Now.AddDays(-38),
        UserId=allFounders[3].Id
    },

    new Idea{
        Title="AI Based Learning App",
        Description="Personalized education platform that adapts courses based on student performance.",
        Category="EdTech",
        RequiredFund=1400000,
        RiskLevel="Low",
        Restrictions="EdTech investors only",
        Status="Pending",
        ImageUrl="startup5.jpg",
        CreatedDate=DateTime.Now.AddDays(-35),
        UserId=allFounders[4].Id
    },

    new Idea{
        Title="Smart Inventory Management",
        Description="Cloud-based inventory management software for small and medium businesses.",
        Category="AI & Recruitment",
        RequiredFund=900000,
        RiskLevel="Low",
        Restrictions="None",
        Status="Pending",
        ImageUrl="startup6.jpg",
        CreatedDate=DateTime.Now.AddDays(-33),
        UserId=allFounders[0].Id
    },

    new Idea{
        Title="Organic Food Marketplace",
        Description="Marketplace platform connecting organic farmers directly with urban consumers.",
        Category="AgriTech",
        RequiredFund=2200000,
        RiskLevel="Medium",
        Restrictions="Food-tech investors preferred",
        Status="Funded",
        ImageUrl="startup7.jpg",
        CreatedDate=DateTime.Now.AddDays(-31),
        UserId=allFounders[1].Id
    },

    new Idea{
        Title="AI Medical Diagnosis Tool",
        Description="AI-powered software to assist doctors in identifying diseases from reports.",
        Category="HealthTech",
        RequiredFund=3000000,
        RiskLevel="High",
        Restrictions="Medical compliance required",
        Status="Pending",
        ImageUrl="startup8.jpg",
        CreatedDate=DateTime.Now.AddDays(-29),
        UserId=allFounders[2].Id
    },

    new Idea{
        Title="Hydrogen Fuel Storage",
        Description="Startup focused on safe and efficient hydrogen fuel storage systems.",
        Category="Green Energy",
        RequiredFund=5200000,
        RiskLevel="High",
        Restrictions="Energy investors only",
        Status="Closed",
        ImageUrl="startup9.jpg",
        CreatedDate=DateTime.Now.AddDays(-28),
        UserId=allFounders[3].Id
    },

    new Idea{
        Title="Virtual Coding Academy",
        Description="Interactive coding education platform with live mentorship and AI practice.",
        Category="EdTech",
        RequiredFund=1600000,
        RiskLevel="Medium",
        Restrictions="None",
        Status="Pending",
        ImageUrl="startup10.jpg",
        CreatedDate=DateTime.Now.AddDays(-27),
        UserId=allFounders[4].Id
    },

    // =========================
    // MORE REALISTIC STARTUPS
    // =========================

    new Idea{
        Title="AI Interview Assistant",
        Description="AI assistant helping recruiters conduct technical interviews faster.",
        Category="AI & Recruitment",
        RequiredFund=1750000,
        RiskLevel="Medium",
        Restrictions="SaaS investors preferred",
        Status="Pending",
        ImageUrl="startup11.jpg",
        CreatedDate=DateTime.Now.AddDays(-25),
        UserId=allFounders[0].Id
    },

    new Idea{
        Title="Hydroponic Farming System",
        Description="Automated hydroponic farming setup for urban indoor agriculture.",
        Category="AgriTech",
        RequiredFund=2100000,
        RiskLevel="Medium",
        Restrictions="Agri investors preferred",
        Status="Pending",
        ImageUrl="startup12.jpg",
        CreatedDate=DateTime.Now.AddDays(-24),
        UserId=allFounders[1].Id
    },

    new Idea{
        Title="Mental Wellness Platform",
        Description="Mental health support application with AI-powered counseling tools.",
        Category="HealthTech",
        RequiredFund=2800000,
        RiskLevel="Medium",
        Restrictions="Healthcare investors only",
        Status="Pending",
        ImageUrl="startup13.jpg",
        CreatedDate=DateTime.Now.AddDays(-23),
        UserId=allFounders[2].Id
    },

    new Idea{
        Title="Biogas Energy Plant",
        Description="Renewable energy production through agricultural waste recycling.",
        Category="Green Energy",
        RequiredFund=6000000,
        RiskLevel="High",
        Restrictions="Infrastructure support needed",
        Status="Pending",
        ImageUrl="startup14.jpg",
        CreatedDate=DateTime.Now.AddDays(-22),
        UserId=allFounders[3].Id
    },

    new Idea{
        Title="AI Homework Evaluator",
        Description="Education platform that automatically evaluates assignments using AI.",
        Category="EdTech",
        RequiredFund=1350000,
        RiskLevel="Low",
        Restrictions="None",
        Status="Funded",
        ImageUrl="startup15.jpg",
        CreatedDate=DateTime.Now.AddDays(-21),
        UserId=allFounders[4].Id
    },

    // =========================
    // AUTO GENERATED 45 MORE
    // =========================
};

                // GENERATE MANY MORE IDEAS
                for (int i = 16; i <= 60; i++)
                {
                    ideas.Add(new Idea
                    {
                        Title = $"Innovative Startup Idea {i}",
                        Description = $"This is a realistic startup idea description for startup number {i}. It focuses on solving modern business and technology challenges with scalable solutions.",
                        Category = i % 5 == 0 ? "EdTech" :
                                   i % 4 == 0 ? "Green Energy" :
                                   i % 3 == 0 ? "HealthTech" :
                                   i % 2 == 0 ? "AgriTech" :
                                   "AI & Recruitment",

                        RequiredFund = 800000 + (i * 120000),

                        RiskLevel = i % 3 == 0 ? "High" :
                                    i % 2 == 0 ? "Medium" :
                                    "Low",

                        Restrictions = "None",

                        Status = i % 4 == 0 ? "Funded" :
                                 i % 5 == 0 ? "Closed" :
                                 "Pending",

                        ImageUrl = $"startup{i}.jpg",

                        CreatedDate = DateTime.Now.AddDays(-i),

                        UserId = allFounders[i % allFounders.Count].Id
                    });
                }

                context.Ideas.AddRange(ideas);
                await context.SaveChangesAsync();
            }

            // =========================================================
            // INVESTMENTS
            // =========================================================

            // ===============================
            // HUGE INVESTMENT SEED DATA
            // ===============================

            if (!context.Investments.Any())
            {
                var investorsList = userManager.Users
                    .Where(x => x.RoleType == "Investor")
                    .ToList();

                var ideasList = context.Ideas.ToList();

                var random = new Random();

                var suggestionList = new List<string>
    {
        "Great potential for future scaling.",
        "Focus more on customer acquisition.",
        "Impressive business model.",
        "Need stronger marketing strategy.",
        "Excellent startup concept.",
        "Consider expanding to tier-2 cities.",
        "Revenue model looks promising.",
        "Improve mobile experience.",
        "Strong long-term growth opportunity.",
        "Team execution seems solid."
    };

                var investments = new List<Investment>();

                for (int i = 1; i <= 220; i++)
                {
                    var randomIdea =
                        ideasList[random.Next(ideasList.Count)];

                    var randomInvestor =
                        investorsList[random.Next(investorsList.Count)];

                    investments.Add(new Investment
                    {
                        IdeaId = randomIdea.Id,

                        InvestorId = randomInvestor.Id,

                        Amount = random.Next(50000, 1000000),

                        Suggestions =
                            suggestionList[random.Next(suggestionList.Count)],

                        InvestmentDate =
                            DateTime.Now.AddDays(-random.Next(1, 120))
                    });
                }

                context.Investments.AddRange(investments);

                await context.SaveChangesAsync();
            }

            // ===============================
            // HUGE COMMENT SEED DATA
            // ===============================

            // =========================================================
            // COMMENTS
            // =========================================================

            if (!context.Comments.Any())
            {
                var investorsList = userManager.Users
                    .Where(x => x.RoleType == "Investor")
                    .ToList();

                var ideasList = context.Ideas.ToList();

                var random = new Random();

                var commentMessages = new List<string>
    {
        "This startup idea has strong market potential.",
        "Very innovative concept.",
        "I think this can scale rapidly in India.",
        "The business model looks sustainable.",
        "Interesting idea with great execution possibilities.",
        "Would love to know more about the revenue strategy.",
        "Strong possibility for long-term growth.",
        "Great startup for future expansion.",
        "This has potential to become a successful company.",
        "Amazing idea for digital transformation."
    };

                var comments = new List<Comment>();

                for (int i = 1; i <= 320; i++)
                {
                    var randomIdea =
                        ideasList[random.Next(ideasList.Count)];

                    var randomInvestor =
                        investorsList[random.Next(investorsList.Count)];

                    comments.Add(new Comment
                    {
                        IdeaId = randomIdea.Id,

                        UserId = randomInvestor.Id,

                        Message =
                            commentMessages[random.Next(commentMessages.Count)],

                        CreatedDate =
                            DateTime.Now.AddDays(-random.Next(1, 150))
                    });
                }

                context.Comments.AddRange(comments);

                await context.SaveChangesAsync();
            }


            // =========================================================
            // STARTUP UPDATES
            // =========================================================

            // ===============================
            // HUGE STARTUP UPDATE SEED DATA
            // ===============================

            if (!context.StartupUpdates.Any())
            {
                var ideas = context.Ideas.ToList();

                var random = new Random();

                var updateTitles = new List<string>
{
    "Prototype Completed",
    "New Investor Meeting",
    "Beta Launch Successful",
    "Product Development Update",
    "Reached First 100 Users",
    "Team Expansion",
    "Mobile App Released",
    "Partnership Announcement",
    "Funding Milestone Achieved",
    "Market Research Completed",
    "AI Feature Added",
    "Website Redesign Launched",
    "New Customer Onboarding",
    "Revenue Growth Update",
    "Business Expansion Plan",
    "Testing Phase Completed",
    "New Technology Integration",
    "Product Performance Improvement",
    "Startup Growth Report",
    "Operational Expansion"
};

                var updateDescriptions = new List<string>
{
    "We successfully completed the initial prototype and started user testing.",

    "Our startup recently connected with multiple investors for future funding discussions.",

    "The beta launch received positive feedback from early users.",

    "We improved overall platform performance and scalability.",

    "The startup reached an important customer growth milestone this month.",

    "Our development team is expanding to accelerate product delivery.",

    "We launched new features based on investor and customer feedback.",

    "The company entered partnership discussions with industry leaders.",

    "Operational efficiency has significantly improved over the last quarter.",

    "The startup is preparing for expansion into new markets.",

    "We enhanced security, UI experience, and application speed.",

    "Customer engagement and platform activity continue to grow rapidly.",

    "The startup roadmap is progressing according to plan.",

    "Our platform now supports advanced AI-driven automation features.",

    "We are receiving strong traction from startup communities and investors.",

    "The startup successfully completed another product development milestone.",

    "New integrations were added to improve platform capabilities.",

    "The business is seeing strong demand from target customers.",

    "We are continuously improving the platform based on market research.",

    "The startup is preparing for the next phase of scaling and growth."
};

                var startupUpdates = new List<StartupUpdate>();

                // CREATE 180+ STARTUP UPDATES

                for (int i = 1; i <= 180; i++)
                {
                    var randomIdea =
                        ideas[random.Next(ideas.Count)];

                    startupUpdates.Add(new StartupUpdate
                    {
                        IdeaId = randomIdea.Id,

                        Title =
                            updateTitles[random.Next(updateTitles.Count)],

                        Description =
                            updateDescriptions[random.Next(updateDescriptions.Count)],

                        CreatedDate =
                            DateTime.Now.AddDays(-random.Next(1, 180))
                    });
                }

                context.StartupUpdates.AddRange(startupUpdates);

                await context.SaveChangesAsync();
            }


            // =========================================================
            // SAVE DATA
            // =========================================================

            await context.SaveChangesAsync();
        }
    }
}