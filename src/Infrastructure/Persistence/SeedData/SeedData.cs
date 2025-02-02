namespace Persistence.SeedData;

public static class SeedData
{
    //public static void SeedUsers(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager)
    //{
    //    if (!roleManager.RoleExistsAsync(UserRoles.Admin).Result)
    //        _ = roleManager.CreateAsync(new MyRole
    //        {
    //            Name = UserRoles.Admin,
    //            NormalizedName = UserRoles.Admin.ToUpper()
    //        }).Result;

    //    if (!roleManager.RoleExistsAsync(UserRoles.Customer).Result)
    //        _ = roleManager.CreateAsync(new MyRole
    //        {
    //            Name = UserRoles.Customer,
    //            NormalizedName = UserRoles.Customer.ToUpper()
    //        }).Result;

    //    if (!roleManager.RoleExistsAsync(UserRoles.Manager).Result)
    //        _ = roleManager.CreateAsync(new MyRole
    //        {
    //            Name = UserRoles.Manager,
    //            NormalizedName = UserRoles.Manager.ToUpper()
    //        }).Result;

    //    if (userManager.FindByNameAsync("abolfazl").Result != null)
    //        return;

    //    MyUser user = new()
    //    {
    //        Name = "delveloper",
    //        UserName = "abolfazl",
    //        NormalizedUserName = "abolfazl".ToUpper(),
    //        PhoneNumber = "09366178192"
    //    };

    //    var result = userManager.CreateAsync(user, "a123456789").Result;

    //    if (result.Succeeded)
    //        _ = userManager.AddToRoleAsync(user, UserRoles.Admin).Result;
    //}
}