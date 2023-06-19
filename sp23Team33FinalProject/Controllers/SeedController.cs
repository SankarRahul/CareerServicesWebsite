using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.DAL;

namespace sp23Team33FinalProject.Controllers
{
    public class SeedController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedController(AppDbContext db, UserManager<AppUser> um, RoleManager<IdentityRole> rm)
        {
            _context = db;
            _userManager = um;
            _roleManager = rm;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SeedRoles()
        {
            try
            {
                //call the method to seed the roles
                await Seeding.SeedRoles.AddAllRoles(_roleManager);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                //Add the message from the inner exception
                errorList.Add(ex.InnerException.Message);

                //Add additional inner exception messages, if there are any
                if (ex.InnerException.InnerException != null)
                {
                    errorList.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", errorList);
            }

            //this is the happy path - seeding worked!
            return View("Confirm");
        }

        public async Task<IActionResult> SeedPeople()
        {
            try
            {
                //call the method to seed the users
                await Seeding.SeedUsers.SeedAllUsers(_userManager, _context);
            }
            catch (Exception ex)
            {
                //add the error messages to a list of strings
                List<String> errorList = new List<String>();

                //Add the outer message
                errorList.Add(ex.Message);

                if (ex.InnerException != null)
                {
                    //Add the message from the inner exception
                    errorList.Add(ex.InnerException.Message);

                    //Add additional inner exception messages, if there are any
                    if (ex.InnerException.InnerException != null)
                    {
                        errorList.Add(ex.InnerException.InnerException.Message);
                    }

                }


                return View("Error", errorList);
            }

            //this is the happy path - seeding worked!
            return View("Confirm");
        }
        //public async Task<IActionResult> SeedMajors()
        //{
        //    try
        //    {
        //        //call the method to seed the roles
        //        await Seeding.SeedMajors.SeedAllMajors(_context);
        //    }
        //    catch (Exception ex)
        //    {
        //        //add the error messages to a list of strings
        //        List<String> errorList = new List<String>();

        //        //Add the outer message
        //        errorList.Add(ex.Message);

        //        //Add the message from the inner exception
        //        errorList.Add(ex.InnerException.Message);

        //        //Add additional inner exception messages, if there are any
        //        if (ex.InnerException.InnerException != null)
        //        {
        //            errorList.Add(ex.InnerException.InnerException.Message);
        //        }

        //        return View("Error", errorList);
        //    }

        //    //this is the happy path - seeding worked!
        //    return View("Confirm");
        //}
        //public async Task<IActionResult> SeedPositions()
        //{
        //    try
        //    {
        //        //call the method to seed the roles
        //        await Seeding.SeedPositions.SeedAllPositions(_context);
        //    }
        //    catch (Exception ex)
        //    {
        //        //add the error messages to a list of strings
        //        List<String> errorList = new List<String>();

        //        //Add the outer message
        //        errorList.Add(ex.Message);

        //        //Add the message from the inner exception
        //        errorList.Add(ex.InnerException.Message);

        //        //Add additional inner exception messages, if there are any
        //        if (ex.InnerException.InnerException != null)
        //        {
        //            errorList.Add(ex.InnerException.InnerException.Message);
        //        }

        //        return View("Error", errorList);
        //    }

        //    //this is the happy path - seeding worked!
        //    return View("Confirm");
        //}

        //public async Task<IActionResult> SeedApplications()
        //{
        //    try
        //    {
        //        //call the method to seed the roles
        //        await Seeding.SeedApplications.SeedAllApplications(_userManager, _context);
        //    }
        //    catch (Exception ex)
        //    {
        //        //add the error messages to a list of strings
        //        List<String> errorList = new List<String>();

        //        //Add the outer message
        //        errorList.Add(ex.Message);

        //        //Add the message from the inner exception
        //        errorList.Add(ex.InnerException.Message);

        //        //Add additional inner exception messages, if there are any
        //        if (ex.InnerException.InnerException != null)
        //        {
        //            errorList.Add(ex.InnerException.InnerException.Message);
        //        }

        //        return View("Error", errorList);
        //    }

        //    //this is the happy path - seeding worked!
        //    return View("Confirm");
        //}

        //public async Task<IActionResult> SeedInterviews()
        //{
        //    try
        //    {
        //        //call the method to seed the roles
        //        await Seeding.SeedInterviews.SeedAllInterviews(_userManager, _context);
        //    }
        //    catch (Exception ex)
        //    {
        //        //create a new list for the error messages
        //        List<String> errors = new List<String>();
        //        //add a generic error message
        //        errors.Add("There was a problem adding repositories to the database");
        //        //add message from the exception
        //        errors.Add(ex.Message);
        //        //add messages from inner exceptions, if there are any
        //        if (ex.InnerException != null)
        //        {
        //            errors.Add(ex.InnerException.Message);
        //            if (ex.InnerException.InnerException != null)
        //            {
        //                errors.Add(ex.InnerException.InnerException.Message);
        //                if (ex.InnerException.InnerException.InnerException != null)
        //                {

        //                    errors.Add(ex.InnerException.InnerException.InnerException.Message);
        //                }
        //            }
        //        }

        //        //return the error view with the errors
        //        return View("Error", errors);
        //    }
        //    //everything is okay - return the confirmation page
        //    return View("Confirm");
        //}

        public IActionResult SeedCompanies()
        {
            try
            {
                Seeding.SeedCompanies.SeedAllCompanies(_context);
            }
            catch (Exception ex)
            {
                //create a new list for the error messages
                List<String> errors = new List<String>();
                //add a generic error message
                errors.Add("There was a problem adding repositories to the database");
                //add message from the exception
                errors.Add(ex.Message);
                //add messages from inner exceptions, if there are any
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errors.Add(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {

                            errors.Add(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
                //return the error view with the errors
                return View("Error", errors);
            }
            //everything is okay - return the confirmation page
            return View("Confirm");
        }

        public IActionResult SeedMajors()
        {
            try
            {
                Seeding.SeedMajors.SeedAllMajors(_context);
            }
            catch (Exception ex)
            {
                //create a new list for the error messages
                List<String> errors = new List<String>();
                //add a generic error message
                errors.Add("There was a problem adding repositories to the database");
                //add message from the exception
                errors.Add(ex.Message);
                //add messages from inner exceptions, if there are any
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errors.Add(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {

                            errors.Add(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
                //return the error view with the errors
                return View("Error", errors);
            }
            //everything is okay - return the confirmation page
            return View("Confirm");
        }

        public IActionResult SeedPositions()
        {
            try
            {
                Seeding.SeedPositions.SeedAllPositions(_context);
            }
            catch (Exception ex)
            {
                //create a new list for the error messages
                List<String> errors = new List<String>();
                //add a generic error message
                errors.Add("There was a problem adding repositories to the database");
                //add message from the exception
                errors.Add(ex.Message);
                //add messages from inner exceptions, if there are any
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errors.Add(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {

                            errors.Add(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
                //return the error view with the errors
                return View("Error", errors);
            }
            //everything is okay - return the confirmation page
            return View("Confirm");
        }

        public IActionResult SeedApplications()
        {
            try
            {
                Seeding.SeedApplications.SeedAllApplications(_userManager, _context);
            }
            catch (Exception ex)
            {
                //create a new list for the error messages
                List<String> errors = new List<String>();
                //add a generic error message
                errors.Add("There was a problem adding repositories to the database");
                //add message from the exception
                errors.Add(ex.Message);
                //add messages from inner exceptions, if there are any
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errors.Add(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {

                            errors.Add(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
                //return the error view with the errors
                return View("Error", errors);
            }
            //everything is okay - return the confirmation page
            return View("Confirm");
        }

        public IActionResult SeedInterviews()
        {
            try
            {
               Seeding.SeedInterviews.SeedAllInterviews(_context);
            }
            catch (Exception ex)
            {
                //create a new list for the error messages
                List<String> errors = new List<String>();
                //add a generic error message
                errors.Add("There was a problem adding repositories to the database");
                //add message from the exception
                errors.Add(ex.Message);
                //add messages from inner exceptions, if there are any
                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                    if (ex.InnerException.InnerException != null)
                    {
                        errors.Add(ex.InnerException.InnerException.Message);
                        if (ex.InnerException.InnerException.InnerException != null)
                        {

                            errors.Add(ex.InnerException.InnerException.InnerException.Message);
                        }
                    }
                }
                //return the error view with the errors
                return View("Error", errors);
            }
            //everything is okay - return the confirmation page
            return View("Confirm");
        }
    }
}
