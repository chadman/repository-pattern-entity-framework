using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Repositories;
using Web.Entity;
using System.Data.Entity.Validation;

namespace Web.Controllers {

    #region Repository
    public interface IHomeRepository : IBaseRepository {
        List<Employee> FindEmployees();
        bool Save();
    }

    public class HomeRepository : BaseRepository, IHomeRepository {
        public List<Employee> FindEmployees() {
            return (from e in base.DBContext.Employees
                    select e).ToList();
        }

        public bool Save() {
            try {
                base.DBContext.SaveChanges();
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex) {
                foreach (DbEntityValidationResult result in ex.EntityValidationErrors) {
                    base.RepositoryDatabaseErrors.Add(result);
                }
                return false;
            }
        }
    }
    #endregion Repository

    public class HomeController : BaseController {

        #region Properties
        public IHomeRepository Repository { get; set; }
        #endregion Properties

        #region Constructor
        public HomeController() {
            this.Repository = new HomeRepository();
        }
        #endregion Constructor

        public ActionResult Index() {

            List<Employee> employees = this.Repository.FindEmployees();

            // Updates the employees name, this will show that the change tracker of the DBContext 
            // picks up the changes so there is no need to attach them to the context
            employees.ForEach(delegate(Employee currentEmployee) {
                currentEmployee.FirstName = "Repository";
                currentEmployee.LastName = "EntityFramework";
            });

            if (!this.Repository.Save()) {
                foreach (DbEntityValidationResult result in this.Repository.RepositoryDatabaseErrors) {
                    foreach (DbValidationError error in result.ValidationErrors) {
                        this.ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
            }

            return View();
        }

        #region Overrides
        protected override void Dispose(bool disposing) {
            // This is important. We must dispose the repository here in each controller
            this.Repository.Dispose();

            base.Dispose(disposing);
        }
        #endregion Overrides

    }
}
