using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Continuations;
using FubuPersistence;
using FubuPersistenceHarness.Domain;

namespace FubuPersistenceHarness
{
    public class HomeEndpoint
    {
        private readonly ITransaction _transaction;

        public HomeEndpoint(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public FubuContinuation post_new_user(NewUserInputModel input)
        {
            _transaction.WithRepository(x => x.Update(new User
            {
                Id = Guid.NewGuid(),
                FirstName = input.FirstName,
                LastName = input.LastName
            }));

            return FubuContinuation.RedirectTo<HomeEndpoint>(x => x.Index());
        }

        public FubuContinuation post_clear(ClearInputModel input)
        {
            _transaction.WithRepository(x => x.DeleteAll<User>());
            return FubuContinuation.RedirectTo<HomeEndpoint>(x => x.Index());
        }

        public ShowUsersViewModel Index()
        {
            ShowUsersViewModel view = null;

            _transaction.WithRepository(x => view = new ShowUsersViewModel
            {
                Users = x.All<User>().ToList()
            });

            return view;
        }
    }

    public class ShowUsersViewModel
    {
        public IList<User> Users { get; set; }
    }

    public class NewUserInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ClearInputModel
    {
    }
}