using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.DAL;
using sp23Team33FinalProject.Migrations;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.Utilities;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using Microsoft.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.ComponentModel.DataAnnotations;
using System;

namespace sp23Team33FinalProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly PasswordValidator<AppUser> _passwordValidator;
        private readonly AppDbContext _context;
        //private String _userPassword;

        public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = appDbContext;
            _userManager = userManager;
            _signInManager = signIn;
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            ViewBag.AllMajors = GetAllMajorsSelectList();
            return View();
        }

        private SelectList GetAllMajorsSelectList()
        {
            //Get the list of months from the database
            List<Major> majorList = _context.Majors.ToList();

            //convert the list to a SelectList by calling SelectList constructor
            SelectList majorSelectList = new SelectList(majorList.OrderBy(m => m.MajorId), "MajorId", "MajorName");

            //return the electList
            return majorSelectList;
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel rvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(rvm);
            }

            var query = from m in _context.Majors select m;
            query = query.Where(m => m.MajorId == rvm.MajorID);

            //this code maps the RegisterViewModel to the AppUser domain model
            AppUser newUser = new AppUser
            {
                UserName = rvm.Email,
                Email = rvm.Email,
                FirstName = rvm.FirstName,
                LastName = rvm.LastName,
                MiddleInitial = rvm.MiddleInitial,
                PositionType = rvm.PositionType,
                Major = query.First(),
                GraduationYear = rvm.GraduationYear.ToString().Substring(1),
                GPA = rvm.GPA
            };

            //create AddUserModel
            AddUserModel aum = new AddUserModel()
            {
                User = newUser,
                Password = rvm.Password,

                //TODO: You will need to change this value if you want to 
                //add the user to a different role - just specify the role name.
                RoleName = "Student"
            };

            //This code uses the AddUser utility to create a new user with the specified password
            IdentityResult result = await Utilities.AddUser.AddUserWithRoleAsync(aum, _userManager, _context);


            if (result.Succeeded && User.IsInRole("CSO")) //everything is okay
            {
                return View("Confirm");
            }
            else if (result.Succeeded)
            {
                //NOTE: This code logs the user into the account that they just created
                //You may or may not want to log a user in directly after they register - check
                //the business rules!
                Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(rvm.Email, rvm.Password, false, lockoutOnFailure: false);
                //_userPassword = rvm.Password;
                //Send the user to the home page
                return RedirectToAction("Index", "Home");
            }
            else  //the add user operation didn't work, and we need to show an error message
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send user back to page with errors
                return View(rvm);
            }
        }

        
        [Authorize(Roles = ("CSO"))]
        public IActionResult RegisterRecruiter()
        {
            ViewBag.AllCompanies = GetAllCompaniesWithNoneSelectList();
            return View();
        }

        private SelectList GetAllCompaniesWithNoneSelectList()
        {
            //Get the list of months from the database
            List<Company> companyList = _context.Companies.ToList();

            //add a dummy entry so the user can select all months
            Company SelectNone = new Company() { CompanyID = 0, CompanyName = "New" };
            companyList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor
            SelectList companySelectList = new SelectList(companyList.OrderBy(m => m.CompanyID), "CompanyID", "CompanyName");

            //return the electList
            return companySelectList;
        }

        private SelectList GetAllCompaniesSelectList()
        {
            //Get the list of months from the database
            List<Company> companyList = _context.Companies.ToList();

            //convert the list to a SelectList by calling SelectList constructor
            SelectList companySelectList = new SelectList(companyList.OrderBy(m => m.CompanyID), "CompanyID", "CompanyName");

            //return the electList
            return companySelectList;
        }

        // POST: /Account/RegisterRecruiter
        [HttpPost]
        [Authorize(Roles = ("CSO"))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRecruiter(RecruiterRegisterViewModel rrvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                ViewBag.AllCompanies = GetAllCompaniesWithNoneSelectList();
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(rrvm);
            }

            var query = from c in _context.Companies select c;
            query = query.Where(c => c.CompanyID == rrvm.CompanyID);

            //this code maps the RegisterViewModel to the AppUser domain model
            AppUser newUser = new AppUser
            {
                UserName = rrvm.Email,
                Email = rrvm.Email,
                FirstName = rrvm.FirstName,
                LastName = rrvm.LastName
            };

            if (query != null && query.Any()==true)
            {
                newUser.Company = query.First();
            }

            //create AddUserModel
            AddUserModel aum = new AddUserModel()
            {
                User = newUser,
                Password = rrvm.Password,

                //TODO: You will need to change this value if you want to 
                //add the user to a different role - just specify the role name.
                RoleName = "Recruiter"
            };

            //This code uses the AddUser utility to create a new user with the specified password
            IdentityResult result = await Utilities.AddUser.AddUserWithRoleAsync(aum, _userManager, _context);

            if (result.Succeeded) //everything is okay
            {
                //NOTE: This code logs the user into the account that they just created
                //You may or may not want to log a user in directly after they register - check
                //the business rules!
                //Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(rrvm.Email, rrvm.Password, false, lockoutOnFailure: false);

                //Send the user to the home page
                return View("Confirm");
            }
            else  //the add user operation didn't work, and we need to show an error message
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send user back to page with errors
                return View(rrvm);
            }
        }

        [Authorize(Roles = ("CSO"))]
        public IActionResult RegisterCSO()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = ("CSO"))]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCSO(CSORegisterViewModel crvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(crvm);
            }

            //this code maps the RegisterViewModel to the AppUser domain model
            AppUser newUser = new AppUser
            {
                UserName = crvm.Email,
                Email = crvm.Email,
                FirstName = crvm.FirstName,
                LastName = crvm.LastName,
            };

            //create AddUserModel
            AddUserModel aum = new AddUserModel()
            {
                User = newUser,
                Password = crvm.Password,

                //TODO: You will need to change this value if you want to 
                //add the user to a different role - just specify the role name.
                RoleName = "CSO"
            };

            //This code uses the AddUser utility to create a new user with the specified password
            IdentityResult result = await Utilities.AddUser.AddUserWithRoleAsync(aum, _userManager, _context);

            if (result.Succeeded) //everything is okay
            {
                //NOTE: This code logs the user into the account that they just created
                //You may or may not want to log a user in directly after they register - check
                //the business rules!
                //Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(crvm.Email, crvm.Password, false, lockoutOnFailure: false);

                //Send the user to the home page
                return View("Confirm");
            }
            else  //the add user operation didn't work, and we need to show an error message
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send user back to page with errors
                return View(crvm);
            }
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl; //pass along the page the user should go back to
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel lvm, string returnUrl)
        {
            //if user forgot to include user name or password,
            //send them back to the login page to try again
            if (ModelState.IsValid == false)
            {
                return View(lvm);
            }

            //attempt to sign the user in using the SignInManager
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.Password, lvm.RememberMe, lockoutOnFailure: false);

            //if the login worked, take the user to either the url
            //they requested OR the homepage if there isn't a specific url
            if (result.Succeeded)
            {
                //return ?? "/" means if returnUrl is null, substitute "/" (home)
                return Redirect(returnUrl ?? "/");
            }
            else //log in was not successful
            {
                //add an error to the model to show invalid attempt
                ModelState.AddModelError("", "Invalid login attempt.");
                //send user back to login page to try again
                return View(lvm);
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View("Error", new string[] { "You are not authorized for this resource" });
        }

        //GET: Account/Index
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            //String id = User.Identity.Name;
            //AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);
            
            //TODO
            AppUser user = await _context.Users.Include(u => u.Major)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var MajorID = _context.Majors.Where(m => m.Students.Contains(user)).ToList().First().MajorId;

            //String GradYearStrConvert = "Y" + user.GraduationYear;
            //GraduationYearEnum GradYear = (GraduationYearEnum)Enum.Parse(typeof(GraduationYearEnum), GradYearStrConvert); ;

            //populate the view model
            //(i.e. map the domain model to the view model)
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.UserName = user.UserName;
            ivm.FirstName = user.FirstName;
            ivm.LastName = user.LastName;
            ivm.MajorName = user.Major.MajorName;
            ivm.GraduationYear = user.GraduationYear;
            ivm.PositionType = user.PositionType;
            ivm.GPA = user.GPA;
            
            //send data to the view
            return View(ivm);
        }

        //GET: Account/RecruiterIndex
        [Authorize(Roles = ("Recruiter"))]
        public async Task<IActionResult> RecruiterIndex()
        {
            RecruiterIndexViewModel rivm = new RecruiterIndexViewModel();

            //get user info
            //String id = User.Identity.Name;
            //AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);

            AppUser user = await _context.Users.Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var MajorID = _context.Majors.Where(m => m.Students.Contains(user)).ToList().First().MajorId;

            //String GradYearStrConvert = "Y" + user.GraduationYear;
            //GraduationYearEnum GradYear = (GraduationYearEnum)Enum.Parse(typeof(GraduationYearEnum), GradYearStrConvert); ;

            //populate the view model
            //(i.e. map the domain model to the view model)
            rivm.Email = user.Email;
            rivm.HasPassword = true;
            rivm.UserID = user.Id;
            rivm.UserName = user.UserName;
            rivm.FirstName = user.FirstName;
            rivm.LastName = user.LastName;
            rivm.CompanyName = user.Company.CompanyName;

            //send data to the view
            return View(rivm);
        }

        //GET: Account/CSOIndex
        [Authorize(Roles ="CSO")]
        public async Task<IActionResult> CSOIndex()
        {
            CSOIndexViewModel cvm = new CSOIndexViewModel();

            //get user info
            //String id = User.Identity.Name;
            //AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);

            AppUser user = await _context.Users.Include(u => u.Company)
                .FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var MajorID = _context.Majors.Where(m => m.Students.Contains(user)).ToList().First().MajorId;

            //String GradYearStrConvert = "Y" + user.GraduationYear;
            //GraduationYearEnum GradYear = (GraduationYearEnum)Enum.Parse(typeof(GraduationYearEnum), GradYearStrConvert); ;

            //populate the view model
            //(i.e. map the domain model to the view model)
            cvm.Email = user.Email;
            cvm.HasPassword = true;
            cvm.UserID = user.Id;
            cvm.UserName = user.UserName;
            cvm.FirstName = user.FirstName;
            cvm.LastName = user.LastName;

            //send data to the view
            return View(cvm);
        }

        //Logic for change password
        // GET: /Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel cpvm)
        {
            //if user forgot a field, send them back to 
            //change password page to try again
            if (ModelState.IsValid == false)
            {
                return View(cpvm);
            }

            //Find the logged in user using the UserManager
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);

            //Attempt to change the password using the UserManager
            var result = await _userManager.ChangePasswordAsync(userLoggedIn, cpvm.OldPassword, cpvm.NewPassword);

            //if the attempt to change the password worked
            if (result.Succeeded)
            {
                //sign in the user with the new password
                await _signInManager.SignInAsync(userLoggedIn, isPersistent: false);
                //_userPassword = cpvm.NewPassword;
                //send the user back to the home page
                return RedirectToAction("Index", "Home");
            }
            else //attempt to change the password didn't work
            {
                //Add all the errors from the result to the model state
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                //send the user back to the change password page to try again
                return View(cpvm);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOff()
        {
            //sign the user out of the application
            _signInManager.SignOutAsync();

            //send the user back to the home page
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/EditProfile
        //TODO: only edit profile of user you are logged into
        [Authorize(Roles ="Student, CSO")]
        public async Task<ActionResult> EditProfile(String? studentID)
        {
            if (User.IsInRole("CSO") == true)
            {
                if (studentID == null)
                {
                    return View("Error", new String[] { "Please specify a student to edit!" });
                }

                AppUser student = await _context.Users.Include(u => u.Major)
                                                            .FirstOrDefaultAsync(u => u.Id == studentID);

                if (student == null)
                {
                    return View("Error", new String[] { "This student does not exist in the database!" });
                }

                String GradYearStr = "Y" + student.GraduationYear;
                GraduationYearEnum GradYearEnum = (GraduationYearEnum)Enum.Parse(typeof(GraduationYearEnum), GradYearStr); ;

                EditProfileViewModel epvm = new EditProfileViewModel();
                epvm.UserID = student.Id;
                epvm.FirstName = student.FirstName;
                epvm.MiddleInitial = student.MiddleInitial;
                epvm.LastName = student.LastName;
                epvm.Email = student.Email;
                epvm.MajorID = student.Major.MajorId;
                epvm.GraduationYear = GradYearEnum;
                epvm.GPA = student.GPA;
                epvm.PositionType = student.PositionType;
                if (student.LockoutEnd != null && student.LockoutEnd > DateTime.Now)
                {
                    epvm.Deactivate = true;
                }

                ViewBag.AllMajors = GetAllMajorsSelectList();
                return View(epvm);
            }

            //get user info
            AppUser user = await _context.Users.Include(u => u.Major).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var MajorID = _context.Majors.Where(m => m.Students.Contains(user)).ToList().First().MajorId;

            String GradYearStrConvert = "Y" + user.GraduationYear;
            GraduationYearEnum GradYear = (GraduationYearEnum)Enum.Parse(typeof(GraduationYearEnum), GradYearStrConvert); ;

            EditProfileViewModel vm = new EditProfileViewModel();
            vm.UserID = user.Id;
            vm.FirstName = user.FirstName;
            vm.MiddleInitial = user.MiddleInitial;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.MajorID = user.Major.MajorId;
            vm.GraduationYear = GradYear;
            vm.GPA = user.GPA;
            vm.PositionType = user.PositionType;

            ViewBag.AllMajors = GetAllMajorsSelectList();
            return View(vm);
        }


        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student, CSO")]
        public  async Task<ActionResult> EditProfile(EditProfileViewModel epvm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                ViewBag.AllMajors = GetAllMajorsSelectList();
                return View(epvm);
            }

            var query = from m in _context.Majors select m;
            query = query.Where(m => m.MajorId == epvm.MajorID);

            AppUser user = _context.Users.FirstOrDefault(u => u.Id == epvm.UserID);

            user.FirstName = epvm.FirstName;
            user.LastName = epvm.LastName;
            user.UserName = epvm.Email;
            user.NormalizedEmail = epvm.Email.ToUpper();
            user.NormalizedUserName = epvm.Email.ToUpper();
            user.Email = epvm.Email;
            user.MiddleInitial = epvm.MiddleInitial;
            user.Major = query.First();
            user.GraduationYear = epvm.GraduationYear.ToString().Substring(1);
            user.GPA = epvm.GPA;
            user.PositionType = epvm.PositionType;

            if (User.IsInRole("CSO"))
            {
                if (epvm.Deactivate == true)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTimeOffset.Now.AddYears(99);
                }

                if (epvm.Deactivate == false)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now;
                }
            }

            _context.SaveChanges();

            if (User.IsInRole("CSO") == false)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Recruiter, CSO")]
        public async Task<ActionResult> EditRecruiterProfile(String? recruiterID)
        {
            if (User.IsInRole("CSO") == true)
            {
                if (recruiterID == null)
                {
                    return View("Error", new String[] { "Please specify a recruiter to edit!" });
                }

                AppUser recruiter = await _context.Users.Include(u => u.Company)
                                                            .FirstOrDefaultAsync(u => u.Id == recruiterID);

                if (recruiter == null)
                {
                    return View("Error", new String[] { "This recruiter does not exist in the database!" });
                }

                RecruiterEditViewModel revm = new RecruiterEditViewModel();
                revm.UserID = recruiter.Id;
                revm.FirstName = recruiter.FirstName;
                revm.LastName = recruiter.LastName;
                revm.Email = recruiter.Email;
                revm.CompanyID = recruiter.Company.CompanyID;
                if(recruiter.LockoutEnd != null && recruiter.LockoutEnd > DateTime.Now)
                {
                    revm.Deactivate = true;
                }

                ViewBag.AllCompanies = GetAllCompaniesSelectList();
                return View(revm);
            }

            //get user info
            AppUser user = await _context.Users.Include(u => u.Company).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            //var CompanyID = _context.Companies.Where(c => c.Recruiters.Contains(user)).ToList().First().CompanyID;

            RecruiterEditViewModel vm = new RecruiterEditViewModel();
            vm.UserID = user.Id;
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.CompanyName = user.Company.CompanyName;

            ViewBag.AllCompanies = GetAllCompaniesSelectList();
            return View(vm);
        }


        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles = "Recruiter, CSO")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRecruiterProfile(RecruiterEditViewModel revm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                ViewBag.AllCompanies = GetAllCompaniesSelectList();
                return View(revm);
            }

            var query = from c in _context.Companies select c;
            query = query.Where(c => c.CompanyID == revm.CompanyID);

            AppUser user = _context.Users.FirstOrDefault(u => u.Id == revm.UserID);

            user.FirstName = revm.FirstName;
            user.LastName = revm.LastName;
            user.UserName = revm.Email;
            user.Email = revm.Email;
            user.NormalizedEmail = revm.Email.ToUpper();
            user.NormalizedUserName = revm.Email.ToUpper();
            if(User.IsInRole("CSO"))
            {
                user.Company = query.First();
                if(revm.Deactivate == true)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTimeOffset.Now.AddYears(99);
                }

                if (revm.Deactivate == false)
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = DateTime.Now;
                }
            }

            _context.SaveChanges();

            if (User.IsInRole("CSO") == false)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "CSO")]
        public async Task<ActionResult> EditCSOProfile(String? csoID)
        {
            //get user info
            if (csoID == null)
            {
                return View("Error", new String[] { "Please specify a CSO to edit!" });
            }

            AppUser cso = await _context.Users.FirstOrDefaultAsync(u => u.Id == csoID);

            if (cso == null)
            {
                return View("Error", new String[] { "This CSO does not exist in the database!" });
            }

            CSOEditiewModel vm = new CSOEditiewModel();
            vm.FirstName = cso.FirstName;
            vm.LastName = cso.LastName;
            vm.Email = cso.Email;
            vm.UserID = cso.Id;

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "CSO")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCSOProfile(CSOEditiewModel cevm)
        {
            //if registration data is valid, create a new user on the database
            if (ModelState.IsValid == false)
            {
                //this is the sad path - something went wrong, 
                //return the user to the register page to try again
                return View(cevm);
            }

            AppUser user = _context.Users.FirstOrDefault(u => u.Id == cevm.UserID);

            user.FirstName = cevm.FirstName;
            user.LastName = cevm.LastName;
            user.UserName = cevm.Email;
            user.Email = cevm.Email;
            user.NormalizedEmail = cevm.Email.ToUpper();
            user.NormalizedUserName = cevm.Email.ToUpper();

            _context.SaveChanges();

            if(user.UserName == User.Identity.Name)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("CSOIndex", "Home");
        }

        [Authorize(Roles = "Student")]
        public async Task<ActionResult>ApplyPosition(int? positionID)
        {
            if (positionID == null)
            {
                return View("Error", new String[] { "Please specify a position to apply to!" });
            }

            Position position = await _context.Positions
                .Include(p => p.Company)
                .Include(p => p.Majors)
                .FirstOrDefaultAsync(m => m.PositionID == positionID);

            if (position == null)
            {
                return View("Error", new String[] { "This position was not found in the database!" });
            }

            AppUser student = await _context.Users.Include(u => u.Major).FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            Application application = await _context.Applications
                .Include(a => a.Position)
                .Include(a => a.Student)
                .FirstOrDefaultAsync(a => a.Position.PositionID == position.PositionID && a.Student.Id == student.Id);

            if (application != null)
            {
                return View("Error", new String[] { "You have already applied to this position!" });
            }
            ISession session = HttpContext.Session;
            string dateString = session.GetString("AppDate");
            DateTime AppDate = DateTime.Parse(dateString);
            if (position.Deadline <= AppDate)
            {
                return View("Error", new String[] { "The application deadline has passed!" });
            }
            if (!position.Majors.Contains(student.Major))
            {
                return View("Error", new String[] { "This position does not accept this major!" });
            }
            if (position.PositionType != student.PositionType)
            {
                return View("Error", new String[] { "This position does not accept your position type!" });
            }

            Application newApp = new Application();
            newApp.AppStatus = Status.Pending;
            newApp.Position = position;
            newApp.Position.Company = position.Company;
            newApp.Student = student;

            _context.Add(newApp);
            await _context.SaveChangesAsync();
            return RedirectToAction("Confirm", "Applications");
        }
    }
}
    