using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

namespace Web.Repositories {

    public interface IBaseRepository {

        void Dispose();
        List<DbEntityValidationResult> RepositoryDatabaseErrors { get; set; }
    }

    public class BaseRepository : IBaseRepository, IDisposable {

        #region Constructor
        public BaseRepository() {
            this._databaseErrors = new List<DbEntityValidationResult>();
        }
        #endregion Constructor

        #region Properties
        private List<DbEntityValidationResult> _databaseErrors;
        public List<DbEntityValidationResult> RepositoryDatabaseErrors {
            get {
                if (_databaseErrors == null) {
                    _databaseErrors = new List<DbEntityValidationResult>();
                }
                return _databaseErrors;
            }
            set {
                this._databaseErrors = value;
            }
        }

        private Web.Entity.NorthwindContext _dbContext = null;
        public Web.Entity.NorthwindContext DBContext {
            get {
                if (this._dbContext == null) {
                    this._dbContext = new Entity.NorthwindContext();
                }

                return this._dbContext;
            }
        }
        #endregion Properties

        #region Dispose Methods

        ~BaseRepository() {
            Dispose(false);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (this._dbContext != null) {
                this._dbContext.Dispose();
                this._dbContext = null;
            }
        }

        #endregion Dispose Methods
    }
}